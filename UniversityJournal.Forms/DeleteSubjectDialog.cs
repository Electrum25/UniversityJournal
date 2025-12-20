using Microsoft.Extensions.DependencyInjection;
using UniversityJournal.Core.Entities;
using UniversityJournal.Core.Repositories;
using UniversityJournal.Core.UseCases;

namespace UniversityJournal.Forms
{
    public partial class DeleteSubjectDialog : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private List<Subject> _subjects;

        public DeleteSubjectDialog(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            LoadSubjects();
        }

        private async void LoadSubjects()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var subjectRepo = scope.ServiceProvider.GetRequiredService<ISubjectRepository>();
                _subjects = await subjectRepo.GetAll() ?? new List<Subject>();

                cmbSubjects.DataSource = _subjects.Select(s => new
                {
                    Display = s.SubjectName,
                    Value = s.SubjectId
                }).ToList();
                cmbSubjects.DisplayMember = "Display";
                cmbSubjects.ValueMember = "Value";
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (cmbSubjects.SelectedValue == null)
            {
                MessageBox.Show("Выберите предмет для удаления.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var subjectId = (Guid)cmbSubjects.SelectedValue;
            bool deleteWithRelated = chkDeleteWithRelated.Checked;  

            string confirmMessage = deleteWithRelated
                ? "Вы уверены, что хотите удалить этот предмет вместе со всеми связанными оценками, посещаемостью и связями со студентами? Это действие необратимо."
                : "Вы уверены, что хотите удалить этот предмет? Убедитесь, что у него нет связанных данных.";

            var confirmResult = MessageBox.Show(
                confirmMessage,
                "Подтверждение удаления",
                MessageBoxButtons.YesNo,
                deleteWithRelated ? MessageBoxIcon.Warning : MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var useCase = scope.ServiceProvider.GetRequiredService<DeleteSubjectUseCase>();
                        await useCase.Handle(subjectId, deleteWithRelated);
                        MessageBox.Show(deleteWithRelated
                            ? "Предмет и все связанные данные успешно удалены."
                            : "Предмет успешно удален.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();  
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка удаления: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
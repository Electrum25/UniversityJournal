using Microsoft.Extensions.DependencyInjection;
using UniversityJournal.Core.Entities;
using UniversityJournal.Core.Repositories;
using UniversityJournal.Core.UseCases;

namespace UniversityJournal.Forms
{
    public partial class UpdateSubjectDialog : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private List<Subject> _subjects;
        private List<Teacher> _teachers;

        public UpdateSubjectDialog(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            LoadSubjects();
            LoadTeachers();
        }

        private async void LoadSubjects()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<ISubjectRepository>();
                _subjects = await repo.GetAll() ?? new List<Subject>();

                cmbSubjects.DataSource = _subjects.Select(s => new
                {
                    Display = s.SubjectName,
                    Value = s.SubjectId
                }).ToList();
                cmbSubjects.DisplayMember = "Display";
                cmbSubjects.ValueMember = "Value";
            }
        }

        private async void LoadTeachers()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<ITeacherRepository>();
                _teachers = await repo.GetAll() ?? new List<Teacher>();

                cmbTeachers.DataSource = _teachers.Select(t => new
                {
                    Display = $"{t.FirstName} {t.LastName}",
                    Value = t.TeacherId
                }).ToList();
                cmbTeachers.DisplayMember = "Display";
                cmbTeachers.ValueMember = "Value";
            }
        }

        private void cmbSubjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSubjects.SelectedValue is Guid subjectId)
            {
                var subject = _subjects.FirstOrDefault(s => s.SubjectId == subjectId);
                if (subject != null)
                {
                    txtSubjectName.Text = subject.SubjectName;
                    cmbTeachers.SelectedValue = subject.TeacherId;
                }
            }
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (cmbSubjects.SelectedValue == null || cmbTeachers.SelectedValue == null)
            {
                MessageBox.Show("Выберите предмет и преподавателя.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmResult = MessageBox.Show("Обновить информацию о предмете?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var useCase = scope.ServiceProvider.GetRequiredService<UpdateSubjectUseCase>();
                        await useCase.Handle(new UpdateSubjectUseCase.UpdateSubjectRequest
                        {
                            SubjectId = (Guid)cmbSubjects.SelectedValue,
                            SubjectName = txtSubjectName.Text,
                            TeacherId = (Guid)cmbTeachers.SelectedValue
                        });
                        MessageBox.Show("Информация обновлена.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadSubjects();  
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
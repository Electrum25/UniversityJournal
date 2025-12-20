using Microsoft.Extensions.DependencyInjection;
using UniversityJournal.Core.Entities;
using UniversityJournal.Core.Repositories;
using UniversityJournal.Core.UseCases;

namespace UniversityJournal.Forms
{
    public partial class DeleteGroupDialog : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private List<Group> _groups;

        public DeleteGroupDialog(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            LoadGroups();
        }

        private async void LoadGroups()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var groupRepo = scope.ServiceProvider.GetRequiredService<IGroupRepository>();
                _groups = await groupRepo.GetAll() ?? new List<Group>();

                cmbGroups.DataSource = _groups.Select(g => new
                {
                    Display = $"{g.GroupName} ({g.Specialization}, {g.Year})",
                    Value = g.GroupId
                }).ToList();
                cmbGroups.DisplayMember = "Display";
                cmbGroups.ValueMember = "Value";
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (cmbGroups.SelectedValue == null)
            {
                MessageBox.Show("Выберите группу для удаления.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var groupId = (Guid)cmbGroups.SelectedValue;
            bool deleteWithStudents = chkDeleteWithStudents.Checked;  

            string confirmMessage = deleteWithStudents
                ? "Вы уверены, что хотите удалить эту группу вместе со всеми студентами и их данными (оценки, посещаемость и т.д.)? Это действие необратимо."
                : "Вы уверены, что хотите удалить эту группу? Убедитесь, что в ней нет студентов.";

            var confirmResult = MessageBox.Show(
                confirmMessage,
                "Подтверждение удаления",
                MessageBoxButtons.YesNo,
                deleteWithStudents ? MessageBoxIcon.Warning : MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var useCase = scope.ServiceProvider.GetRequiredService<DeleteGroupUseCase>();
                        await useCase.Handle(groupId, deleteWithStudents);
                        MessageBox.Show(deleteWithStudents
                            ? "Группа и все связанные студенты успешно удалены."
                            : "Группа успешно удалена.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

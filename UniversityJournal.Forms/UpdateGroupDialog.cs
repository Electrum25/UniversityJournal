using Microsoft.Extensions.DependencyInjection;
using UniversityJournal.Core.Entities;
using UniversityJournal.Core.Repositories;
using UniversityJournal.Core.UseCases;

namespace UniversityJournal.Forms
{
    public partial class UpdateGroupDialog : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private List<Group> _groups;

        public UpdateGroupDialog(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            LoadGroups();
        }

        private async void LoadGroups()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<IGroupRepository>();
                _groups = await repo.GetAll() ?? new List<Group>();

                cmbGroups.DataSource = _groups.Select(g => new
                {
                    Display = $"{g.GroupName} ({g.Specialization}, {g.Year})",
                    Value = g.GroupId
                }).ToList();
                cmbGroups.DisplayMember = "Display";
                cmbGroups.ValueMember = "Value";
            }
        }

        private void cmbGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbGroups.SelectedValue is Guid groupId)
            {
                var group = _groups.FirstOrDefault(g => g.GroupId == groupId);
                if (group != null)
                {
                    txtGroupName.Text = group.GroupName;
                    txtSpecialization.Text = group.Specialization;
                    nudYear.Value = group.Year;
                }
            }
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (cmbGroups.SelectedValue == null)
            {
                MessageBox.Show("Выберите группу.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmResult = MessageBox.Show("Обновить информацию о группе?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var useCase = scope.ServiceProvider.GetRequiredService<UpdateGroupUseCase>();
                        await useCase.Handle(new UpdateGroupUseCase.UpdateGroupRequest
                        {
                            GroupId = (Guid)cmbGroups.SelectedValue,
                            GroupName = txtGroupName.Text,
                            Specialization = txtSpecialization.Text,
                            Year = (int)nudYear.Value
                        });
                        MessageBox.Show("Информация обновлена.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadGroups(); 
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
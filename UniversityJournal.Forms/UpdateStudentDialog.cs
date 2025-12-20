using Microsoft.Extensions.DependencyInjection;
using UniversityJournal.Core.Entities;
using UniversityJournal.Core.Repositories;
using UniversityJournal.Core.UseCases;

namespace UniversityJournal.Forms
{
    public partial class UpdateStudentDialog : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private List<Group> _groups;
        private List<Student> _students;

        public UpdateStudentDialog(IServiceProvider serviceProvider)
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

                cmbNewGroup.DataSource = _groups.Select(g => new
                {
                    Display = $"{g.GroupName} ({g.Specialization}, {g.Year})",
                    Value = g.GroupId
                }).ToList();
                cmbNewGroup.DisplayMember = "Display";
                cmbNewGroup.ValueMember = "Value";
            }
        }

        private async void LoadStudents(Guid groupId)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<IStudentRepository>();
                _students = await repo.GetByGroup(groupId) ?? new List<Student>();

                cmbStudents.DataSource = _students.Select(s => new
                {
                    Display = $"{s.FirstName} {s.LastName}",
                    Value = s.StudentId
                }).ToList();
                cmbStudents.DisplayMember = "Display";
                cmbStudents.ValueMember = "Value";
            }
        }

        private void cmbGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbGroups.SelectedValue is Guid groupId)
            {
                LoadStudents(groupId);
            }
            else
            {
                cmbStudents.DataSource = null;
            }
        }

        private void cmbStudents_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbStudents.SelectedValue is Guid studentId)
            {
                var student = _students.FirstOrDefault(s => s.StudentId == studentId);
                if (student != null)
                {
                    txtFirstName.Text = student.FirstName;
                    txtLastName.Text = student.LastName;
                    cmbNewGroup.SelectedValue = student.GroupId;  
                }
            }
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (cmbStudents.SelectedValue == null || cmbNewGroup.SelectedValue == null)
            {
                MessageBox.Show("Выберите студента и новую группу.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmResult = MessageBox.Show("Обновить информацию о студенте?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var useCase = scope.ServiceProvider.GetRequiredService<UpdateStudentUseCase>();
                        await useCase.Handle(new UpdateStudentUseCase.UpdateStudentRequest
                        {
                            StudentId = (Guid)cmbStudents.SelectedValue,
                            FirstName = txtFirstName.Text,
                            LastName = txtLastName.Text,
                            GroupId = (Guid)cmbNewGroup.SelectedValue  
                        });
                        MessageBox.Show("Информация обновлена.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadStudents((Guid)cmbGroups.SelectedValue);  
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
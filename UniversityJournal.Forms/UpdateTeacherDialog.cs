using Microsoft.Extensions.DependencyInjection;
using UniversityJournal.Core.Entities;
using UniversityJournal.Core.Repositories;
using UniversityJournal.Core.UseCases;

namespace UniversityJournal.Forms
{
    public partial class UpdateTeacherDialog : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private List<Teacher> _teachers;

        public UpdateTeacherDialog(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            LoadTeachers();
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

        private void cmbTeachers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTeachers.SelectedValue is Guid teacherId)
            {
                var teacher = _teachers.FirstOrDefault(t => t.TeacherId == teacherId);
                if (teacher != null)
                {
                    txtFirstName.Text = teacher.FirstName;
                    txtLastName.Text = teacher.LastName;
                    txtPatronymic.Text = teacher.Patronymic;
                }
            }
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (cmbTeachers.SelectedValue == null)
            {
                MessageBox.Show("Выберите преподавателя.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmResult = MessageBox.Show("Обновить информацию о преподавателе?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var useCase = scope.ServiceProvider.GetRequiredService<UpdateTeacherUseCase>();
                        await useCase.Handle(new UpdateTeacherUseCase.UpdateTeacherRequest
                        {
                            TeacherId = (Guid)cmbTeachers.SelectedValue,
                            FirstName = txtFirstName.Text,
                            LastName = txtLastName.Text,
                            Patronymic = txtPatronymic.Text
                        });
                        MessageBox.Show("Информация обновлена.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadTeachers();  
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
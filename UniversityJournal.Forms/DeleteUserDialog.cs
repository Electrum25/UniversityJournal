using Microsoft.Extensions.DependencyInjection;
using System.Data;
using UniversityJournal.Core.Entities;
using UniversityJournal.Core.Repositories;
using UniversityJournal.Core.UseCases;

namespace UniversityJournal.Forms
{
    public partial class DeleteUserDialog : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private List<User> _users;

        public DeleteUserDialog(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            LoadUsers();
        }

        private async void LoadUsers()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var userRepo = scope.ServiceProvider.GetRequiredService<IUserRepository>();
                _users = await userRepo.GetAll() ?? new List<User>();

                cmbUsers.DataSource = _users.Select(u => new
                {
                    Display = $"{u.Login} ({u.Role})",
                    Value = u.UserId
                }).ToList();
                cmbUsers.DisplayMember = "Display";
                cmbUsers.ValueMember = "Value";
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (cmbUsers.SelectedValue == null)
            {
                MessageBox.Show("Выберите пользователя для удаления.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var userId = (Guid)cmbUsers.SelectedValue;
            var confirmResult = MessageBox.Show(
                "Вы уверены, что хотите удалить этого пользователя? Это действие необратимо.",
                "Подтверждение удаления",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var useCase = scope.ServiceProvider.GetRequiredService<DeleteUserUseCase>();
                        await useCase.Handle(userId);
                        MessageBox.Show("Пользователь успешно удален.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
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
            LoadUsersAsync();  // Вызов без await, так как конструктор не async
        }

        private async Task LoadUsersAsync()  // Изменено на async Task
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var userRepo = scope.ServiceProvider.GetRequiredService<IUserRepository>();
                _users = await userRepo.GetAll() ?? new List<User>();

                // Фильтруем админов, если их <= 1
                var adminCount = _users.Count(u => u.Role == UserRole.Admin);
                var filteredUsers = adminCount <= 1 ? _users.Where(u => u.Role != UserRole.Admin) : _users;

                cmbUsers.DataSource = filteredUsers.Select(u => new
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
                        await LoadUsersAsync();  // Теперь корректно с await
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
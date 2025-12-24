using Microsoft.Extensions.DependencyInjection;
using UniversityJournal.Core.UseCases;
using UniversityJournal.Core.Entities;

namespace UniversityJournal.Forms
{
    public partial class AddAdminDialog : Form
    {
        private readonly IServiceProvider _serviceProvider;

        public AddAdminDialog(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLogin.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Заполните все поля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var scope = _serviceProvider.CreateScope())
            {
                var useCase = scope.ServiceProvider.GetRequiredService<CreateUserUseCase>();
                try
                {
                    var request = new CreateUserUseCase.CreateUserRequest
                    {
                        Login = txtLogin.Text,
                        Password = txtPassword.Text,
                        Role = UserRole.Admin
                    };
                    await useCase.Handle(request);
                    MessageBox.Show("Админ добавлен!");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}\nInner: {ex.InnerException?.Message}");
                }
            }
        }
    }
}
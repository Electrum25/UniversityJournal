using Microsoft.Extensions.DependencyInjection;
using UniversityJournal.Core.UseCases;

namespace UniversityJournal.Forms
{
    public partial class AddTeacherDialog : Form
    {
        private readonly IServiceProvider _serviceProvider;

        public AddTeacherDialog(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            var useCase = _serviceProvider.GetRequiredService<CreateTeacherUseCase>();
            try
            {
                var request = new CreateTeacherUseCase.CreateTeacherRequest
                {
                    Login = txtLogin.Text,
                    Password = txtPassword.Text,
                    FirstName = txtFirstName.Text,
                    LastName = txtLastName.Text,
                    Patronymic = txtPatronymic.Text
                };
                await useCase.Handle(request);
                MessageBox.Show("Преподаватель добавлен!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }
    }
}
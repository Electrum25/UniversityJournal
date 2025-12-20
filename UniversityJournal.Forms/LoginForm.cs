using Microsoft.Extensions.DependencyInjection;
using UniversityJournal.Core.UseCases;
using UniversityJournal.Core.Entities;
using UniversityJournal.Core.Repositories;

namespace UniversityJournal.Forms
{
    public partial class LoginForm : Form
    {
        private readonly IServiceProvider _serviceProvider;

        public LoginForm(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            txtLogin.Focus();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            var useCase = _serviceProvider.GetRequiredService<AuthenticateUserUseCase>();
            try
            {
                var request = new AuthenticateUserUseCase.AuthenticateUserRequest
                {
                    Login = txtLogin.Text,
                    Password = txtPassword.Text
                };
                var user = await useCase.Handle(request);

                Form nextForm = null;
                if (user.Role == UserRole.Admin)
                {
                    nextForm = new AdminForm(_serviceProvider);
                }
                else if (user.Role == UserRole.Teacher)
                {
                    nextForm = new TeacherForm(_serviceProvider, user.UserId);
                }
                else if (user.Role == UserRole.Student)
                {
                    var studentId = await GetStudentId(user.UserId); 
                    nextForm = new StudentForm(_serviceProvider, studentId);
                }

                if (nextForm != null)
                {
                    nextForm.Show();
                    this.Owner?.Hide();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка входа: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task<Guid> GetStudentId(Guid userId)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var studentRepo = scope.ServiceProvider.GetRequiredService<IStudentRepository>();
                var students = await studentRepo.GetAll();
                var student = students.FirstOrDefault(s => s.UserId == userId);
                return student?.StudentId ?? Guid.Empty;
            }
        }
    }
}
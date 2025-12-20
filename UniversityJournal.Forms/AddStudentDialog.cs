using Microsoft.Extensions.DependencyInjection;
using UniversityJournal.Core.UseCases;
using UniversityJournal.Core.Entities;

namespace UniversityJournal.Forms
{
    public partial class AddStudentDialog : Form
    {
        private readonly IServiceProvider _serviceProvider;

        public AddStudentDialog(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            LoadGroups(); 
        }

        private async void LoadGroups()
        {
            var useCase = _serviceProvider.GetRequiredService<GetGroupsUseCase>();
            var groups = await useCase.Handle();
            cmbGroup.DataSource = groups;
            cmbGroup.DisplayMember = "GroupName"; 
            cmbGroup.ValueMember = "GroupId"; 
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            var useCase = _serviceProvider.GetRequiredService<CreateStudentUseCase>();
            try
            {
                var request = new CreateStudentUseCase.CreateStudentRequest
                {
                    Login = txtLogin.Text,
                    Password = txtPassword.Text,
                    FirstName = txtFirstName.Text,
                    LastName = txtLastName.Text,
                    GroupId = (Guid)cmbGroup.SelectedValue 
                };
                await useCase.Handle(request);
                MessageBox.Show("Студент добавлен!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }
    }
}

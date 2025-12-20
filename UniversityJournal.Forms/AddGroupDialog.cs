using Microsoft.Extensions.DependencyInjection;
using UniversityJournal.Core.UseCases;

namespace UniversityJournal.Forms
{
    public partial class AddGroupDialog : Form
    {
        private readonly IServiceProvider _serviceProvider;

        public AddGroupDialog(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            var useCase = _serviceProvider.GetRequiredService<CreateGroupUseCase>();
            try
            {
                var request = new CreateGroupUseCase.CreateGroupRequest
                {
                    GroupName = txtGroupName.Text,
                    Specialization = txtSpecialization.Text,
                    Year = int.Parse(txtYear.Text)
                };
                await useCase.Handle(request);
                MessageBox.Show("Группа добавлена!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using UniversityJournal.Core.Entities;
using UniversityJournal.Core.Repositories;
using UniversityJournal.Core.UseCases;

namespace UniversityJournal.Forms
{
    public partial class AddSubjectDialog : Form
    {
        private readonly IServiceProvider _serviceProvider;

        public AddSubjectDialog(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            LoadTeachers();
        }
        private void AddSubjectDialog_Load(object sender, EventArgs e)
        {

        }

        private async void LoadTeachers()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var useCase = scope.ServiceProvider.GetRequiredService<ITeacherRepository>();
                var teachers = await useCase.GetAll();
                cmbTeacher.DataSource = teachers;
                cmbTeacher.DisplayMember = "LastName"; 
                cmbTeacher.ValueMember = "TeacherId";
            }
        }


        private async void btnSave_Click(object sender, EventArgs e)
        {
            var useCase = _serviceProvider.GetRequiredService<CreateSubjectUseCase>();
            try
            {
                var selectedTeacher = (Teacher)cmbTeacher.SelectedItem; 
                var request = new CreateSubjectUseCase.CreateSubjectRequest
                {
                    SubjectName = txtSubjectName.Text,
                    TeacherId = selectedTeacher.TeacherId 
                };
                await useCase.Handle(request);
                MessageBox.Show("Предмет создан!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

    }
}
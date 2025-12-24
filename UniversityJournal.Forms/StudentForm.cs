using Microsoft.Extensions.DependencyInjection;
using UniversityJournal.Core.UseCases;
using UniversityJournal.Core.Entities;

namespace UniversityJournal.Forms
{
    public partial class StudentForm : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Guid _studentId;

        public StudentForm(IServiceProvider serviceProvider, Guid studentId)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _studentId = studentId;
            LoadData();
        }

        private async void LoadData()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var useCase = scope.ServiceProvider.GetRequiredService<GetStudentDataUseCase>();
                var data = await useCase.Handle(_studentId);

                var subjectDict = data.Subjects?.ToDictionary(s => s.SubjectId, s => s.SubjectName) ?? new Dictionary<Guid, string>();

                if (data.Grades != null && data.Grades.Any())
                {
                    dgvGrades.AutoGenerateColumns = true;
                    dgvGrades.DataSource = data.Grades.Select(g => new
                    {
                        Предмет = subjectDict.ContainsKey(g.SubjectId) ? subjectDict[g.SubjectId] : "Unknown",
                        Лаба = g.LabNumber,
                        Оценка = g.Score,
                        Дата = g.Date,
                        Комментарий = g.Comment
                    }).ToList();
                }
                else
                {
                    MessageBox.Show("Нет оценок.");
                }

                if (data.Attendances != null && data.Attendances.Any())
                {
                    dgvAttendances.AutoGenerateColumns = true;
                    dgvAttendances.DataSource = data.Attendances.Select(a => new
                    {
                        Предмет = subjectDict.ContainsKey(a.SubjectId) ? subjectDict[a.SubjectId] : "Unknown",
                        Дата = a.Date,
                        Статус = a.Status.ToString()
                    }).ToList();
                }
                else
                {
                    MessageBox.Show("Нет посещаемостей.");
                }
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            var loginForm = new LoginForm(_serviceProvider);
            loginForm.Show();
            this.Hide();
        }
    }
}
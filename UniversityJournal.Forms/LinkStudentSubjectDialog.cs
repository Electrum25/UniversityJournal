using Microsoft.Extensions.DependencyInjection;
using UniversityJournal.Core.Entities;
using UniversityJournal.Core.Repositories;
using UniversityJournal.Core.UseCases;

namespace UniversityJournal.Forms
{
    public partial class LinkStudentSubjectDialog : Form
    {
        private readonly IServiceProvider _serviceProvider;

        public LinkStudentSubjectDialog(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            LoadData();
        }

        private async void LoadData()
        {
            await LoadStudents();
            await LoadSubjects();
        }

        private async Task LoadStudents()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<IStudentRepository>();
                var students = await repo.GetAll();
                cmbStudent.DataSource = students;
                cmbStudent.DisplayMember = "LastName";
                cmbStudent.ValueMember = "StudentId";
            }
        }

        private async Task LoadSubjects()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<ISubjectRepository>();
                var subjects = await repo.GetAll();
                cmbSubject.DataSource = subjects;
                cmbSubject.DisplayMember = "SubjectName";
                cmbSubject.ValueMember = "SubjectId";
            }
        }

        private async Task LoadExistingLinks(Guid studentId)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var studentSubjectRepo = scope.ServiceProvider.GetRequiredService<IStudentSubjectRepository>();
                var subjectRepo = scope.ServiceProvider.GetRequiredService<ISubjectRepository>();

                var links = await studentSubjectRepo.GetByStudent(studentId) ?? new List<StudentSubject>();
                var subjects = await subjectRepo.GetAll() ?? new List<Subject>();

                var subjectDict = subjects.ToDictionary(s => s.SubjectId, s => s.SubjectName);

                cmbExistingLinks.DataSource = links.Select(l => new
                {
                    Display = subjectDict.GetValueOrDefault(l.SubjectId, "Unknown"),
                    Value = new { l.StudentId, l.SubjectId }
                }).ToList();
                cmbExistingLinks.DisplayMember = "Display";
                cmbExistingLinks.ValueMember = "Value";
            }
        }

        private async void cmbStudent_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbStudent.SelectedValue is Guid studentId)
            {
                await LoadExistingLinks(studentId);
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var useCase = scope.ServiceProvider.GetRequiredService<CreateStudentSubjectUseCase>();
                try
                {
                    var request = new CreateStudentSubjectUseCase.CreateStudentSubjectRequest
                    {
                        StudentId = (Guid)cmbStudent.SelectedValue,
                        SubjectId = (Guid)cmbSubject.SelectedValue
                    };
                    await useCase.Handle(request);
                    MessageBox.Show("Студент привязан к предмету!");
                    await LoadExistingLinks((Guid)cmbStudent.SelectedValue);  
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}\nInner: {ex.InnerException?.Message}");
                }
            }
        }

        private async void btnUnlink_Click(object sender, EventArgs e)
        {
            if (cmbExistingLinks.SelectedValue == null)
            {
                MessageBox.Show("Выберите связь для разрыва.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selected = (dynamic)cmbExistingLinks.SelectedValue;
            Guid studentId = selected.StudentId;
            Guid subjectId = selected.SubjectId;

            var confirmResult = MessageBox.Show("Разорвать связь между студентом и предметом?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var useCase = scope.ServiceProvider.GetRequiredService<UnlinkStudentSubjectUseCase>();
                        await useCase.Handle(new UnlinkStudentSubjectUseCase.UnlinkStudentSubjectRequest
                        {
                            StudentId = studentId,
                            SubjectId = subjectId
                        });
                        MessageBox.Show("Связь разорвана.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await LoadExistingLinks(studentId);  
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
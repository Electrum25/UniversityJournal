using Microsoft.Extensions.DependencyInjection;
using UniversityJournal.Core.Entities;
using UniversityJournal.Core.Repositories;
using UniversityJournal.Core.UseCases;

namespace UniversityJournal.Forms
{
    public partial class TeacherForm : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Guid _userId;
        private Guid _teacherId;

        public TeacherForm(IServiceProvider serviceProvider, Guid userId)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _userId = userId;
            LoadTeacherIdAndSubjects();
        }

        private async void LoadTeacherIdAndSubjects()
        {
            _teacherId = await GetTeacherId(_userId);
            LoadSubjects();
            LoadStatusOptions();
        }

        private async Task<Guid> GetTeacherId(Guid userId)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var teacherRepo = scope.ServiceProvider.GetRequiredService<ITeacherRepository>();
                var teachers = await teacherRepo.GetAll();
                var teacher = teachers.FirstOrDefault(t => t.UserId == userId);
                return teacher?.TeacherId ?? Guid.Empty;
            }
        }

        private async void LoadSubjects()
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var useCase = scope.ServiceProvider.GetRequiredService<GetSubjectsByTeacherUseCase>();
                    var subjects = await useCase.Handle(_teacherId);
                    cmbSubject.DataSource = subjects;
                    cmbSubject.DisplayMember = "SubjectName";
                    cmbSubject.ValueMember = "SubjectId";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки предметов: {ex.Message}");
            }
        }

        private void LoadStatusOptions()
        {
            cmbStatus.DataSource = Enum.GetValues(typeof(AttendanceStatus));
        }

        private void cmbSubject_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadStudentsForSubject();
        }

        private async void LoadStudentsForSubject()
        {
            if (cmbSubject.SelectedItem == null) return;

            using (var scope = _serviceProvider.CreateScope())
            {
                var studentSubjectRepo = scope.ServiceProvider.GetRequiredService<IStudentSubjectRepository>();
                var selectedSubject = (Subject)cmbSubject.SelectedItem;
                var subjectId = selectedSubject.SubjectId;
                var studentSubjects = await studentSubjectRepo.GetBySubject(subjectId);
                var studentIds = studentSubjects.Select(ss => ss.StudentId).ToList();
                var studentRepo = scope.ServiceProvider.GetRequiredService<IStudentRepository>();
                var students = await studentRepo.GetAll();
                var filteredStudents = students.Where(s => studentIds.Contains(s.StudentId)).ToList();
                cmbStudent.DataSource = filteredStudents;
                cmbStudent.DisplayMember = "LastName";
                cmbStudent.ValueMember = "StudentId";
            }
        }

        private async void btnSetGrade_Click(object sender, EventArgs e)
        {
            if (cmbSubject.SelectedItem == null || cmbStudent.SelectedItem == null) return;

            using (var scope = _serviceProvider.CreateScope())
            {
                var useCase = scope.ServiceProvider.GetRequiredService<CreateGradeUseCase>();
                try
                {
                    var selectedSubject = (Subject)cmbSubject.SelectedItem;
                    var selectedStudent = (Student)cmbStudent.SelectedItem;
                    var request = new CreateGradeUseCase.CreateGradeRequest
                    {
                        StudentId = selectedStudent.StudentId,
                        SubjectId = selectedSubject.SubjectId,
                        LabNumber = int.Parse(txtLabNumber.Text),
                        Score = int.Parse(txtScore.Text),
                        Comment = txtComment.Text
                    };
                    await useCase.Handle(request, _teacherId);
                    MessageBox.Show("Оценка поставлена!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}");
                }
            }
        }

        private async void btnMarkAttendance_Click(object sender, EventArgs e)
        {
            if (cmbSubject.SelectedItem == null || cmbStudent.SelectedItem == null) return;

            using (var scope = _serviceProvider.CreateScope())
            {
                var useCase = scope.ServiceProvider.GetRequiredService<CreateAttendanceUseCase>();
                try
                {
                    var selectedSubject = (Subject)cmbSubject.SelectedItem;
                    var selectedStudent = (Student)cmbStudent.SelectedItem;
                    var request = new CreateAttendanceUseCase.CreateAttendanceRequest
                    {
                        StudentId = selectedStudent.StudentId,
                        SubjectId = selectedSubject.SubjectId,
                        Date = dtpDate.Value,
                        Status = (AttendanceStatus)cmbStatus.SelectedItem
                    };
                    await useCase.Handle(request, _teacherId);
                    MessageBox.Show("Посещаемость отмечена!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}\nInner: {ex.InnerException?.Message}");
                }
            }
        }

        private async void btnUpdateFinalGrade_Click(object sender, EventArgs e)
        {
            if (cmbSubject.SelectedItem == null || cmbStudent.SelectedItem == null) return;

            using (var scope = _serviceProvider.CreateScope())
            {
                var useCase = scope.ServiceProvider.GetRequiredService<UpdateStudentSubjectUseCase>();
                try
                {
                    var selectedSubject = (Subject)cmbSubject.SelectedItem;
                    var selectedStudent = (Student)cmbStudent.SelectedItem;
                    var request = new UpdateStudentSubjectUseCase.UpdateStudentSubjectRequest
                    {
                        StudentId = selectedStudent.StudentId,
                        SubjectId = selectedSubject.SubjectId,
                        FinalGrade = txtFinalGrade.Text
                    };
                    await useCase.Handle(request);
                    MessageBox.Show("Итоговая оценка обновлена!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}");
                }
            }
        }
        private void btnViewResults_Click(object sender, EventArgs e)
        {
            try
            {
                var form = new TeacherResultsForm(_serviceProvider, _teacherId);  
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка открытия формы: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

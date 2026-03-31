using UniversityJournal.Core.Entities;
using UniversityJournal.Core.Repositories;
using ClosedXML.Excel;
using System.IO;

namespace UniversityJournal.Core.UseCases
{
    public class ExportDatabaseToExcelUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IStudentSubjectRepository _studentSubjectRepository;
        private readonly IGradeRepository _gradeRepository;
        private readonly IAttendanceRepository _attendanceRepository;

        public ExportDatabaseToExcelUseCase(
            IUserRepository userRepository,
            IStudentRepository studentRepository,
            ITeacherRepository teacherRepository,
            IGroupRepository groupRepository,
            ISubjectRepository subjectRepository,
            IStudentSubjectRepository studentSubjectRepository,
            IGradeRepository gradeRepository,
            IAttendanceRepository attendanceRepository)
        {
            _userRepository = userRepository;
            _studentRepository = studentRepository;
            _teacherRepository = teacherRepository;
            _groupRepository = groupRepository;
            _subjectRepository = subjectRepository;
            _studentSubjectRepository = studentSubjectRepository;
            _gradeRepository = gradeRepository;
            _attendanceRepository = attendanceRepository;
        }

        public async Task ExportToExcel(string filePath)
        {
            using (var workbook = new XLWorkbook())
            {
                // Лист Users
                var users = await _userRepository.GetAll() ?? new List<User>();
                var usersSheet = workbook.Worksheets.Add("Users");
                usersSheet.Cell(1, 1).Value = "UserId";
                usersSheet.Cell(1, 2).Value = "Login";
                usersSheet.Cell(1, 3).Value = "Role";
                usersSheet.Cell(1, 4).Value = "CreatedAt";
                for (int i = 0; i < users.Count; i++)
                {
                    usersSheet.Cell(i + 2, 1).Value = users[i].UserId.ToString();
                    usersSheet.Cell(i + 2, 2).Value = users[i].Login;
                    usersSheet.Cell(i + 2, 3).Value = users[i].Role.ToString();
                    usersSheet.Cell(i + 2, 4).Value = users[i].CreatedAt.ToString();
                }

                // Лист Students
                var students = await _studentRepository.GetAll() ?? new List<Student>();
                var studentsSheet = workbook.Worksheets.Add("Students");
                studentsSheet.Cell(1, 1).Value = "StudentId";
                studentsSheet.Cell(1, 2).Value = "UserId";
                studentsSheet.Cell(1, 3).Value = "FirstName";
                studentsSheet.Cell(1, 4).Value = "LastName";
                studentsSheet.Cell(1, 5).Value = "GroupId";
                for (int i = 0; i < students.Count; i++)
                {
                    studentsSheet.Cell(i + 2, 1).Value = students[i].StudentId.ToString();
                    studentsSheet.Cell(i + 2, 2).Value = students[i].UserId.ToString();
                    studentsSheet.Cell(i + 2, 3).Value = students[i].FirstName;
                    studentsSheet.Cell(i + 2, 4).Value = students[i].LastName;
                    studentsSheet.Cell(i + 2, 5).Value = students[i].GroupId.ToString();
                }

                // Лист Teachers
                var teachers = await _teacherRepository.GetAll() ?? new List<Teacher>();
                var teachersSheet = workbook.Worksheets.Add("Teachers");
                teachersSheet.Cell(1, 1).Value = "TeacherId";
                teachersSheet.Cell(1, 2).Value = "UserId";
                teachersSheet.Cell(1, 3).Value = "FirstName";
                teachersSheet.Cell(1, 4).Value = "LastName";
                teachersSheet.Cell(1, 5).Value = "Patronymic";
                for (int i = 0; i < teachers.Count; i++)
                {
                    teachersSheet.Cell(i + 2, 1).Value = teachers[i].TeacherId.ToString();
                    teachersSheet.Cell(i + 2, 2).Value = teachers[i].UserId.ToString();
                    teachersSheet.Cell(i + 2, 3).Value = teachers[i].FirstName;
                    teachersSheet.Cell(i + 2, 4).Value = teachers[i].LastName;
                    teachersSheet.Cell(i + 2, 5).Value = teachers[i].Patronymic;
                }

                // Лист Groups
                var groups = await _groupRepository.GetAll() ?? new List<Group>();
                var groupsSheet = workbook.Worksheets.Add("Groups");
                groupsSheet.Cell(1, 1).Value = "GroupId";
                groupsSheet.Cell(1, 2).Value = "GroupName";
                groupsSheet.Cell(1, 3).Value = "Specialization";
                groupsSheet.Cell(1, 4).Value = "Year";
                for (int i = 0; i < groups.Count; i++)
                {
                    groupsSheet.Cell(i + 2, 1).Value = groups[i].GroupId.ToString();
                    groupsSheet.Cell(i + 2, 2).Value = groups[i].GroupName;
                    groupsSheet.Cell(i + 2, 3).Value = groups[i].Specialization;
                    groupsSheet.Cell(i + 2, 4).Value = groups[i].Year;
                }

                // Лист Subjects
                var subjects = await _subjectRepository.GetAll() ?? new List<Subject>();
                var subjectsSheet = workbook.Worksheets.Add("Subjects");
                subjectsSheet.Cell(1, 1).Value = "SubjectId";
                subjectsSheet.Cell(1, 2).Value = "SubjectName";
                subjectsSheet.Cell(1, 3).Value = "TeacherId";
                for (int i = 0; i < subjects.Count; i++)
                {
                    subjectsSheet.Cell(i + 2, 1).Value = subjects[i].SubjectId.ToString();
                    subjectsSheet.Cell(i + 2, 2).Value = subjects[i].SubjectName;
                    subjectsSheet.Cell(i + 2, 3).Value = subjects[i].TeacherId.ToString();
                }

                // Лист StudentSubjects
                var studentSubjects = await _studentSubjectRepository.GetAll() ?? new List<StudentSubject>();
                var studentSubjectsSheet = workbook.Worksheets.Add("StudentSubjects");
                studentSubjectsSheet.Cell(1, 1).Value = "StudentId";
                studentSubjectsSheet.Cell(1, 2).Value = "SubjectId";
                studentSubjectsSheet.Cell(1, 3).Value = "FinalScore";
                studentSubjectsSheet.Cell(1, 4).Value = "FinalGrade";
                for (int i = 0; i < studentSubjects.Count; i++)
                {
                    studentSubjectsSheet.Cell(i + 2, 1).Value = studentSubjects[i].StudentId.ToString();
                    studentSubjectsSheet.Cell(i + 2, 2).Value = studentSubjects[i].SubjectId.ToString();
                    studentSubjectsSheet.Cell(i + 2, 3).Value = studentSubjects[i].FinalScore?.ToString();
                    studentSubjectsSheet.Cell(i + 2, 4).Value = studentSubjects[i].FinalGrade;
                }

                // Лист Grades
                var grades = await _gradeRepository.GetAll() ?? new List<Grade>();
                var gradesSheet = workbook.Worksheets.Add("Grades");
                gradesSheet.Cell(1, 1).Value = "GradeId";
                gradesSheet.Cell(1, 2).Value = "StudentId";
                gradesSheet.Cell(1, 3).Value = "SubjectId";
                gradesSheet.Cell(1, 4).Value = "LabNumber";
                gradesSheet.Cell(1, 5).Value = "Score";
                gradesSheet.Cell(1, 6).Value = "Date";
                gradesSheet.Cell(1, 7).Value = "Comment";
                for (int i = 0; i < grades.Count; i++)
                {
                    gradesSheet.Cell(i + 2, 1).Value = grades[i].GradeId.ToString();
                    gradesSheet.Cell(i + 2, 2).Value = grades[i].StudentId.ToString();
                    gradesSheet.Cell(i + 2, 3).Value = grades[i].SubjectId.ToString();
                    gradesSheet.Cell(i + 2, 4).Value = grades[i].LabNumber;
                    gradesSheet.Cell(i + 2, 5).Value = grades[i].Score;
                    gradesSheet.Cell(i + 2, 6).Value = grades[i].Date.ToString();
                    gradesSheet.Cell(i + 2, 7).Value = grades[i].Comment;
                }

                // Лист Attendances
                var attendances = await _attendanceRepository.GetAll() ?? new List<Attendance>();
                var attendancesSheet = workbook.Worksheets.Add("Attendances");
                attendancesSheet.Cell(1, 1).Value = "AttendanceId";
                attendancesSheet.Cell(1, 2).Value = "StudentId";
                attendancesSheet.Cell(1, 3).Value = "SubjectId";
                attendancesSheet.Cell(1, 4).Value = "Date";
                attendancesSheet.Cell(1, 5).Value = "Status";
                for (int i = 0; i < attendances.Count; i++)
                {
                    attendancesSheet.Cell(i + 2, 1).Value = attendances[i].AttendanceId.ToString();
                    attendancesSheet.Cell(i + 2, 2).Value = attendances[i].StudentId.ToString();
                    attendancesSheet.Cell(i + 2, 3).Value = attendances[i].SubjectId.ToString();
                    attendancesSheet.Cell(i + 2, 4).Value = attendances[i].Date.ToString();
                    attendancesSheet.Cell(i + 2, 5).Value = attendances[i].Status.ToString();
                }

                workbook.SaveAs(filePath);
            }
        }
    }
}

using UniversityJournal.Core.Entities;
using UniversityJournal.Core.Repositories;

namespace UniversityJournal.Core.UseCases
{
    public class GetTeacherResultsUseCase
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IGradeRepository _gradeRepository;
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IStudentSubjectRepository _studentSubjectRepository;

        public GetTeacherResultsUseCase(
            ISubjectRepository subjectRepository,
            IStudentRepository studentRepository,
            IGradeRepository gradeRepository,
            IAttendanceRepository attendanceRepository,
            IStudentSubjectRepository studentSubjectRepository)
        {
            _subjectRepository = subjectRepository;
            _studentRepository = studentRepository;
            _gradeRepository = gradeRepository;
            _attendanceRepository = attendanceRepository;
            _studentSubjectRepository = studentSubjectRepository;
        }

        public async Task<List<dynamic>> Handle(Guid teacherId, Guid groupId, string dataType)
        {
            var subjects = await _subjectRepository.GetAll() ?? new List<Subject>();
            var teacherSubjects = subjects.Where(s => s.TeacherId == teacherId).Select(s => s.SubjectId).ToList();

            var students = await _studentRepository.GetByGroup(groupId) ?? new List<Student>();
            var studentIds = students.Select(s => s.StudentId).ToList();

            var results = new List<dynamic>();

            if (dataType == "Посещаемость")
            {
                var attendances = await _attendanceRepository.GetAll() ?? new List<Attendance>();
                var filtered = attendances.Where(a => teacherSubjects.Contains(a.SubjectId) && studentIds.Contains(a.StudentId)).ToList();
                results = filtered.Select(a => new
                {
                    StudentName = students.FirstOrDefault(s => s.StudentId == a.StudentId)?.FirstName + " " + students.FirstOrDefault(s => s.StudentId == a.StudentId)?.LastName,
                    SubjectName = subjects.FirstOrDefault(s => s.SubjectId == a.SubjectId)?.SubjectName,
                    Date = a.Date,
                    Status = a.Status.ToString()
                }).Cast<dynamic>().ToList();
            }
            else if (dataType == "Оценки")
            {
                var grades = await _gradeRepository.GetAll() ?? new List<Grade>();
                var filtered = grades.Where(g => teacherSubjects.Contains(g.SubjectId) && studentIds.Contains(g.StudentId)).ToList();
                results = filtered.Select(g => new
                {
                    StudentName = students.FirstOrDefault(s => s.StudentId == g.StudentId)?.FirstName + " " + students.FirstOrDefault(s => s.StudentId == g.StudentId)?.LastName,
                    SubjectName = subjects.FirstOrDefault(s => s.SubjectId == g.SubjectId)?.SubjectName,
                    LabNumber = g.LabNumber,
                    Score = g.Score,
                    Date = g.Date,
                    Comment = g.Comment
                }).Cast<dynamic>().ToList();
            }
            else if (dataType == "Финальные оценки")
            {
                var studentSubjects = await _studentSubjectRepository.GetAll() ?? new List<StudentSubject>();
                var filtered = studentSubjects.Where(ss => teacherSubjects.Contains(ss.SubjectId) && studentIds.Contains(ss.StudentId)).ToList();
                results = filtered.Select(ss => new
                {
                    StudentName = students.FirstOrDefault(s => s.StudentId == ss.StudentId)?.FirstName + " " + students.FirstOrDefault(s => s.StudentId == ss.StudentId)?.LastName,
                    SubjectName = subjects.FirstOrDefault(s => s.SubjectId == ss.SubjectId)?.SubjectName,
                    FinalScore = ss.FinalScore,
                    FinalGrade = ss.FinalGrade
                }).Cast<dynamic>().ToList();
            }

            return results;
        }
    }
}
using UniversityJournal.Core.Entities;
using UniversityJournal.Core.Repositories;

namespace UniversityJournal.Core.UseCases
{
    public class GetStudentDataUseCase
    {
        private readonly IGradeRepository _gradeRepository;
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly ISubjectRepository _subjectRepository; 

        public GetStudentDataUseCase(
            IGradeRepository gradeRepository,
            IAttendanceRepository attendanceRepository,
            ISubjectRepository subjectRepository)  
        {
            _gradeRepository = gradeRepository;
            _attendanceRepository = attendanceRepository;
            _subjectRepository = subjectRepository;  
        }

        public async Task<StudentData> Handle(Guid studentId)
        {
            var grades = await _gradeRepository.GetByStudent(studentId);
            var attendances = await _attendanceRepository.GetByStudent(studentId);
            var subjects = await _subjectRepository.GetAll();  
            return new StudentData { Grades = grades, Attendances = attendances, Subjects = subjects };
        }

        public class StudentData
        {
            public List<Grade>? Grades { get; set; }
            public List<Attendance>? Attendances { get; set; }
            public List<Subject>? Subjects { get; set; } 
        }
    }
}
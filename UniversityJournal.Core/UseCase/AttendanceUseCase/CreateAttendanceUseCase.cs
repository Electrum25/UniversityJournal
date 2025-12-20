using UniversityJournal.Core.Entities;
using UniversityJournal.Core.Repositories;

namespace UniversityJournal.Core.UseCases
{
    public class CreateAttendanceUseCase
    {
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ISubjectRepository _subjectRepository;

        public CreateAttendanceUseCase(IAttendanceRepository attendanceRepository, IStudentRepository studentRepository, ISubjectRepository subjectRepository)
        {
            _attendanceRepository = attendanceRepository;
            _studentRepository = studentRepository;
            _subjectRepository = subjectRepository;
        }

        public async Task Handle(CreateAttendanceRequest request, Guid teacherId)
        {
            var student = await _studentRepository.Get(request.StudentId);
            if (student == null)
            {
                throw new ArgumentException("Студент не найден.");
            }

            var subject = await _subjectRepository.Get(request.SubjectId);
            if (subject == null)
            {
                throw new ArgumentException("Предмет не найден.");
            }

            if (request.Status == null)
            {
                throw new ArgumentException("Статус обязателен.");
            }

            var attendance = new Attendance
            {
                AttendanceId = Guid.NewGuid(),
                StudentId = request.StudentId,
                SubjectId = request.SubjectId,
                Date = request.Date.ToUniversalTime(), 
                Status = request.Status
            };
            await _attendanceRepository.Create(attendance);
        }

        public class CreateAttendanceRequest
        {
            public Guid StudentId { get; set; }
            public Guid SubjectId { get; set; }
            public DateTime Date { get; set; }
            public AttendanceStatus Status { get; set; }
        }
    }
}

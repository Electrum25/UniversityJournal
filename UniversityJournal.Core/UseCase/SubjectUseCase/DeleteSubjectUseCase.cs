using UniversityJournal.Core.Entities;
using UniversityJournal.Core.Repositories;

namespace UniversityJournal.Core.UseCases
{
    public class DeleteSubjectUseCase
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly IGradeRepository _gradeRepository;
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IStudentSubjectRepository _studentSubjectRepository;  

        public DeleteSubjectUseCase(
            ISubjectRepository subjectRepository,
            IGradeRepository gradeRepository,
            IAttendanceRepository attendanceRepository,
            IStudentSubjectRepository studentSubjectRepository)
        {
            _subjectRepository = subjectRepository;
            _gradeRepository = gradeRepository;
            _attendanceRepository = attendanceRepository;
            _studentSubjectRepository = studentSubjectRepository;
        }

        public async Task<bool> Handle(Guid subjectId, bool deleteWithRelated = false)
        {
            var subject = await _subjectRepository.Get(subjectId);
            if (subject == null)
            {
                throw new ArgumentException("Предмет не найден.", nameof(subjectId));
            }

            // Проверяем связанные записи
            var grades = await _gradeRepository.GetBySubject(subjectId);
            var attendances = await _attendanceRepository.GetBySubject(subjectId);
            var studentSubjects = await _studentSubjectRepository.GetBySubject(subjectId);  

            bool hasRelated = (grades?.Any() == true) || (attendances?.Any() == true) || (studentSubjects?.Any() == true);

            if (deleteWithRelated)
            {
                // Удаляем связанные записи
                if (grades != null)
                    foreach (var grade in grades)
                        await _gradeRepository.Delete(grade.GradeId);

                if (attendances != null)
                    foreach (var attendance in attendances)
                        await _attendanceRepository.Delete(attendance.AttendanceId);

                if (studentSubjects != null)
                    foreach (var ss in studentSubjects)
                        await _studentSubjectRepository.Delete(ss.StudentId, ss.SubjectId);  
            }
            else
            {
                if (hasRelated)
                {
                    throw new InvalidOperationException("Нельзя удалить предмет, так как у него есть связанные оценки, посещаемость или связи со студентами. Отметьте опцию 'Удалить вместе со связанными данными' для каскадного удаления.");
                }
            }

            await _subjectRepository.Delete(subjectId);
            return true;
        }
    }
}
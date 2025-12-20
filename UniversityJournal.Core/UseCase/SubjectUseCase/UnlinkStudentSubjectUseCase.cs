using UniversityJournal.Core.Entities;
using UniversityJournal.Core.Repositories;

namespace UniversityJournal.Core.UseCases
{
    public class UnlinkStudentSubjectUseCase
    {
        private readonly IStudentSubjectRepository _studentSubjectRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ISubjectRepository _subjectRepository;

        public UnlinkStudentSubjectUseCase(
            IStudentSubjectRepository studentSubjectRepository,
            IStudentRepository studentRepository,
            ISubjectRepository subjectRepository)
        {
            _studentSubjectRepository = studentSubjectRepository;
            _studentRepository = studentRepository;
            _subjectRepository = subjectRepository;
        }

        public async Task Handle(UnlinkStudentSubjectRequest request)
        {
            var student = await _studentRepository.Get(request.StudentId);
            if (student == null)
            {
                throw new ArgumentException("Студент не найден.", nameof(request.StudentId));
            }

            var subject = await _subjectRepository.Get(request.SubjectId);
            if (subject == null)
            {
                throw new ArgumentException("Предмет не найден.", nameof(request.SubjectId));
            }

            var studentSubject = await _studentSubjectRepository.Get(request.StudentId, request.SubjectId);
            if (studentSubject == null)
            {
                throw new ArgumentException("Связь между студентом и предметом не найдена.");
            }

            await _studentSubjectRepository.Delete(request.StudentId, request.SubjectId);
        }

        public class UnlinkStudentSubjectRequest
        {
            public Guid StudentId { get; set; }
            public Guid SubjectId { get; set; }
        }
    }
}
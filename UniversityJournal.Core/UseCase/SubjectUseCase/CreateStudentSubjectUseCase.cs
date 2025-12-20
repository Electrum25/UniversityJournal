using UniversityJournal.Core.Entities;
using UniversityJournal.Core.Repositories;

namespace UniversityJournal.Core.UseCases
{
    public class CreateStudentSubjectUseCase
    {
        private readonly IStudentSubjectRepository _studentSubjectRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ISubjectRepository _subjectRepository;

        public CreateStudentSubjectUseCase(IStudentSubjectRepository studentSubjectRepository, IStudentRepository studentRepository, ISubjectRepository subjectRepository)
        {
            _studentSubjectRepository = studentSubjectRepository;
            _studentRepository = studentRepository;
            _subjectRepository = subjectRepository;
        }

        public async Task Handle(CreateStudentSubjectRequest request)
        {
            var student = await _studentRepository.Get(request.StudentId);
            if (student == null) throw new ArgumentException("Студент не найден.");

            var subject = await _subjectRepository.Get(request.SubjectId);
            if (subject == null) throw new ArgumentException("Предмет не найден.");

            var existing = await _studentSubjectRepository.Get(request.StudentId, request.SubjectId);
            if (existing != null) throw new ArgumentException("Студент уже привязан к этому предмету.");

            var studentSubject = new StudentSubject
            {
                StudentId = request.StudentId,
                SubjectId = request.SubjectId,
                FinalScore = null,
                FinalGrade = "" 
            };
            await _studentSubjectRepository.Create(studentSubject);
        }

        public class CreateStudentSubjectRequest
        {
            public Guid StudentId { get; set; }
            public Guid SubjectId { get; set; }
        }
    }
}
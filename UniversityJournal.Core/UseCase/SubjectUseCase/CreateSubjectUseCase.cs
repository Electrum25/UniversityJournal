using UniversityJournal.Core.Entities;
using UniversityJournal.Core.Repositories;

namespace UniversityJournal.Core.UseCases
{
    public class CreateSubjectUseCase
    {
        private readonly ISubjectRepository _subjectRepository;

        public CreateSubjectUseCase(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        public async Task<Guid> Handle(CreateSubjectRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.SubjectName) || request.TeacherId == Guid.Empty)
            {
                throw new ArgumentException("SubjectName and TeacherId are required.");
            }

            var subject = new Subject
            {
                SubjectId = Guid.NewGuid(),
                SubjectName = request.SubjectName,
                TeacherId = request.TeacherId
            };
            return await _subjectRepository.Create(subject);
        }

        public class CreateSubjectRequest
        {
            public string SubjectName { get; set; } = string.Empty;
            public Guid TeacherId { get; set; }
        }
    }
}
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

            // Простая проверка: часов не может быть 0 или меньше
            if (request.TotalHours <= 0)
            {
                throw new ArgumentException("TotalHours must be greater than zero.");
            }

            var subject = new Subject
            {
                SubjectId = Guid.NewGuid(),
                SubjectName = request.SubjectName,
                TeacherId = request.TeacherId,
                TotalHours = request.TotalHours // Передаем часы в базу
            };

            return await _subjectRepository.Create(subject);
        }

        public class CreateSubjectRequest
        {
            public string SubjectName { get; set; } = string.Empty;
            public Guid TeacherId { get; set; }
            public int TotalHours { get; set; }
        }
    }
}
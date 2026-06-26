using EasyCaching.Core;
using UniversityJournal.Core.Entities;
using UniversityJournal.Core.Repositories;

namespace UniversityJournal.Core.UseCases
{
    public class CreateSubjectUseCase
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly IEasyCachingProvider _cache;

        public CreateSubjectUseCase(ISubjectRepository subjectRepository,
            IEasyCachingProviderFactory cacheFactory)
        {
            _subjectRepository = subjectRepository;
            _cache = cacheFactory.GetCachingProvider("default");
        }

        public async Task<Guid> Handle(CreateSubjectRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.SubjectName) || request.TeacherId == Guid.Empty)
                throw new ArgumentException("SubjectName and TeacherId are required.");

            if (request.TotalHours <= 0)
                throw new ArgumentException("TotalHours must be greater than zero.");

            var subject = new Subject
            {
                SubjectId = Guid.NewGuid(),
                SubjectName = request.SubjectName,
                TeacherId = request.TeacherId,
                TotalHours = request.TotalHours
            };

            var createdId = await _subjectRepository.Create(subject);

            await _cache.RemoveByPrefixAsync("subjects_teacher_");

            return createdId;
        }

        public class CreateSubjectRequest
        {
            public string SubjectName { get; set; } = string.Empty;
            public Guid TeacherId { get; set; }
            public int TotalHours { get; set; }
        }
    }
}
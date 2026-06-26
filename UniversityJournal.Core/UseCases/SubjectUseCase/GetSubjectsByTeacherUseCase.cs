using EasyCaching.Core;
using Microsoft.Extensions.Logging;
using UniversityJournal.Core.DTOs;
using UniversityJournal.Core.Repositories;

namespace UniversityJournal.Core.UseCases
{
    public class GetSubjectsByTeacherUseCase
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly IEasyCachingProvider _cache;
        private readonly ILogger<GetSubjectsByTeacherUseCase> _logger;

        public GetSubjectsByTeacherUseCase(
            ISubjectRepository subjectRepository,
            IEasyCachingProviderFactory cacheFactory,
            ILogger<GetSubjectsByTeacherUseCase> logger)
        {
            _subjectRepository = subjectRepository;
            _cache = cacheFactory.GetCachingProvider("default");
            _logger = logger;
        }

        public async Task<IEnumerable<SubjectDTO>> Handle(Guid teacherId)
        {
            string cacheKey = $"subjects_teacher_{teacherId}";

            var cached = await _cache.GetAsync<IEnumerable<SubjectDTO>>(cacheKey);
            if (!cached.IsNull)
                return cached.Value;

            var subjects = await _subjectRepository.GetByTeacher(teacherId);
            var dtos = subjects?.Select(s => new SubjectDTO(s)).ToList()
                       ?? new List<SubjectDTO>();

            await _cache.SetAsync(cacheKey, dtos, TimeSpan.FromMinutes(10), default);

            _logger.LogInformation("Subjects for teacher {TeacherId} cached (EasyCaching)", teacherId);

            return dtos;
        }
    }
}
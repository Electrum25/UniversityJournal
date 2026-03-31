using UniversityJournal.Core.Entities;
using UniversityJournal.Core.Repositories;

namespace UniversityJournal.Core.UseCases
{
    public class CreateGradeUseCase
    {
        private readonly IGradeRepository _gradeRepository;

        public CreateGradeUseCase(IGradeRepository gradeRepository)
        {
            _gradeRepository = gradeRepository;
        }

        public async Task<Guid> Handle(CreateGradeRequest request, Guid teacherId)
        {
            if (request.StudentId == Guid.Empty || request.SubjectId == Guid.Empty ||
                request.Score < 0 || request.Score > 100 || request.LabNumber <= 0)
            {
                throw new ArgumentException("Invalid data: check StudentId, SubjectId, Score (0-100), LabNumber.");
            }

            var grade = new Grade
            {
                GradeId = Guid.NewGuid(),
                StudentId = request.StudentId,
                SubjectId = request.SubjectId,
                LabNumber = request.LabNumber,
                Score = request.Score,
                Date = DateTime.UtcNow,
                Comment = request.Comment,
            };
            return await _gradeRepository.Create(grade);
        }

        public class CreateGradeRequest
        {
            public Guid StudentId { get; set; }
            public Guid SubjectId { get; set; }
            public int LabNumber { get; set; }
            public int Score { get; set; }
            public string? Comment { get; set; }
        }
    }
}

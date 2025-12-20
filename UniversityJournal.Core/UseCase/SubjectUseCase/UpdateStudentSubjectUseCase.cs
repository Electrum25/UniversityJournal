using UniversityJournal.Core.Entities;
using UniversityJournal.Core.Repositories;

namespace UniversityJournal.Core.UseCases
{
    public class UpdateStudentSubjectUseCase
    {
        private readonly IStudentSubjectRepository _studentSubjectRepository;

        public UpdateStudentSubjectUseCase(IStudentSubjectRepository studentSubjectRepository)
        {
            _studentSubjectRepository = studentSubjectRepository;
        }

        public async Task Handle(UpdateStudentSubjectRequest request)
        {
            var studentSubject = await _studentSubjectRepository.Get(request.StudentId, request.SubjectId);
            if (studentSubject == null) throw new ArgumentException("Not found.");

            studentSubject.FinalScore = request.FinalScore;
            studentSubject.FinalGrade = request.FinalGrade;
            await _studentSubjectRepository.Update(studentSubject);
        }

        public class UpdateStudentSubjectRequest
        {
            public Guid StudentId { get; set; }
            public Guid SubjectId { get; set; }
            public decimal? FinalScore { get; set; }
            public string FinalGrade { get; set; } = string.Empty;
        }
    }
}

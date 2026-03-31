using UniversityJournal.Core.Entities;
using UniversityJournal.Core.Repositories;

namespace UniversityJournal.Core.UseCases
{
    public class UpdateSubjectUseCase
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly ITeacherRepository _teacherRepository;

        public UpdateSubjectUseCase(ISubjectRepository subjectRepository, ITeacherRepository teacherRepository)
        {
            _subjectRepository = subjectRepository;
            _teacherRepository = teacherRepository;
        }

        public async Task Handle(UpdateSubjectRequest request)
        {
            var subject = await _subjectRepository.Get(request.SubjectId);
            if (subject == null)
            {
                throw new ArgumentException("Предмет не найден.", nameof(request.SubjectId));
            }

            var teacher = await _teacherRepository.Get(request.TeacherId);
            if (teacher == null)
            {
                throw new ArgumentException("Преподаватель не найден.", nameof(request.TeacherId));
            }

            subject.SubjectName = request.SubjectName;
            subject.TeacherId = request.TeacherId;

            var success = await _subjectRepository.Update(subject);
            if (!success)
            {
                throw new Exception("Не удалось обновить предмет.");
            }
        }

        public class UpdateSubjectRequest
        {
            public Guid SubjectId { get; set; }
            public string SubjectName { get; set; }
            public Guid TeacherId { get; set; }
        }
    }
}
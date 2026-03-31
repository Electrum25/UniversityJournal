using UniversityJournal.Core.Entities;
using UniversityJournal.Core.Repositories;

namespace UniversityJournal.Core.UseCases
{
    public class GetSubjectsByTeacherUseCase
    {
        private readonly ISubjectRepository _subjectRepository;

        public GetSubjectsByTeacherUseCase(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        public async Task<List<Subject>?> Handle(Guid teacherId)
        {
            return await _subjectRepository.GetByTeacher(teacherId);
        }
    }
}
using UniversityJournal.Core.Entities;

namespace UniversityJournal.Core.Repositories
{
    public interface ISubjectRepository
    {
        public Task<Guid> Create(Subject subject);
        public Task<bool> Update(Subject subject);
        public Task Delete(Guid subjectId);
        public Task<Subject?> Get(Guid subjectId);
        public Task<List<Subject>?> GetByTeacher(Guid teacherId);
        public Task<List<Subject>?> GetAll();
    }
}
using UniversityJournal.Core.Entities;

namespace UniversityJournal.Core.Repositories
{
    public interface ITeacherRepository
    {
        public Task<Guid> Create(Teacher teacher);
        public Task<bool> Update(Teacher teacher);
        public Task Delete(Guid teacherId);
        public Task<Teacher?> Get(Guid teacherId);
        public Task<List<Teacher>?> GetAll();
    }
}


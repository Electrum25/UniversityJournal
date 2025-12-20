using UniversityJournal.Core.Entities;

namespace UniversityJournal.Core.Repositories
{
    public interface IStudentRepository
    {
        public Task<Guid> Create(Student student);
        public Task<bool> Update(Student student);
        public Task Delete(Guid studentId);
        public Task<Student?> Get(Guid studentId);
        public Task<List<Student>?> GetAll();
        public Task<List<Student>?> GetByGroup(Guid groupId);
    }
}

using UniversityJournal.Core.Entities;

namespace UniversityJournal.Core.Repositories
{
    public interface IGroupRepository
    {
        public Task<Guid> Create(Group group);
        public Task<bool> Update(Group group);
        public Task Delete(Guid groupId);
        public Task<Group?> Get(Guid groupId);
        public Task<List<Group>?> GetAll();
    }
}

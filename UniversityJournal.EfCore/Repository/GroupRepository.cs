using Microsoft.EntityFrameworkCore;
using UniversityJournal.Core.Entities;
using UniversityJournal.Core.Repositories;
using UniversityJournal.EfCore;

namespace UniversityJournal.Storage.EfCore.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly UniversityJournalDbContext _context;

        public GroupRepository(UniversityJournalDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Guid> Create(Group group)
        {
            if (group == null) throw new ArgumentNullException(nameof(group));
            _context.Add(group);
            await _context.SaveChangesAsync();
            return group.GroupId;
        }

        public async Task Delete(Guid groupId)
        {
            var group = await _context.Groups.FindAsync(groupId);
            if (group != null)
            {
                _context.Groups.Remove(group);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("Group not found.", nameof(groupId));
            }
        }

        public async Task<Group?> Get(Guid groupId)
        {
            return await _context.Groups.FindAsync(groupId);
        }

        public async Task<List<Group>?> GetAll()
        {
            return await _context.Groups.ToListAsync();
        }

        public async Task<bool> Update(Group group)
        {
            if (group == null) throw new ArgumentNullException(nameof(group));
            _context.Entry(group).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }
    }
}

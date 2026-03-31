using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityJournal.Core.Entities;
using UniversityJournal.Core.Storage.Repositories;

namespace UniversityJournal.EfCore.Repository
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly UniversityJournalDbContext _context;
        public ScheduleRepository(UniversityJournalDbContext context) => _context = context;

        public async Task<List<ScheduleItem>> GetByGroupIdAsync(Guid groupId)
        {
            return await _context.ScheduleItems
                .Include(s => s.Subject) // Чтобы подтянулось название предмета
                .Include(s => s.Teacher) // ЧТОБЫ ПОДТЯНУЛОСЬ ФИО ПРЕПОДАВАТЕЛЯ
                .Where(s => s.GroupId == groupId)
                .ToListAsync();
        }
        public async Task<bool> AddAsync(ScheduleItem item)
        {
            _context.ScheduleItems.Add(item);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<int> CountAsync(Guid subjectId, Guid groupId) =>
            await _context.ScheduleItems.CountAsync(s => s.SubjectId == subjectId && s.GroupId == groupId);

        public async Task<bool> IsBusyAsync(Guid groupId, DayOfWeek day, int pair) =>
            await _context.ScheduleItems.AnyAsync(s => s.GroupId == groupId && s.DayOfWeek == day && s.PairNumber == pair);
    }
}

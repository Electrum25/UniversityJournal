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
                .Include(s => s.Subject)
                .Include(s => s.Teacher) 
                .Where(s => s.GroupId == groupId)
                .ToListAsync();
        }
        public async Task<bool> AddAsync(ScheduleItem item)
        {
            _context.ScheduleItems.Add(item);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<ScheduleItem>> GetByDateRangeAsync(Guid groupId, DateTime start, DateTime end)
        {
            return await _context.ScheduleItems
                .Include(s => s.Subject)
                .Include(s => s.Teacher)
                .Include(s => s.Group) 
                .Where(s => s.GroupId == groupId && s.Date >= start && s.Date <= end)
                .OrderBy(s => s.Date)
                .ThenBy(s => s.PairNumber)
                .ToListAsync();
        }

        public async Task<bool> IsBusyAsync(Guid groupId, DateTime date, int pair) =>
            await _context.ScheduleItems.AnyAsync(s => s.GroupId == groupId && s.Date.Date == date.Date && s.PairNumber == pair);

        public async Task<int> CountAsync(Guid subjectId, Guid groupId) =>
            await _context.ScheduleItems.CountAsync(s => s.SubjectId == subjectId && s.GroupId == groupId);

        public async Task<List<ScheduleItem>> GetByTeacherDateRangeAsync(Guid teacherId, DateTime start, DateTime end)
        {
            return await _context.ScheduleItems
                .Include(s => s.Subject)
                .Include(s => s.Group) 
                .Include(s => s.Teacher)
                .Where(s => s.TeacherId == teacherId &&
                            s.Date >= start &&
                            s.Date <= end)
                .ToListAsync();
        }
    }
}

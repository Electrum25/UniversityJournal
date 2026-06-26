using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityJournal.Core.Entities;

namespace UniversityJournal.Core.Storage.Repositories
{
    public interface IScheduleRepository
    {
        Task<List<ScheduleItem>> GetByGroupIdAsync(Guid groupId);
        Task<List<ScheduleItem>> GetByDateRangeAsync(Guid groupId, DateTime start, DateTime end);
        Task<bool> AddAsync(ScheduleItem item);
        Task<int> CountAsync(Guid subjectId, Guid groupId);
        Task<bool> IsBusyAsync(Guid groupId, DateTime date, int pair);
        Task<List<ScheduleItem>> GetByTeacherDateRangeAsync(Guid teacherId, DateTime start, DateTime end);
    }
}

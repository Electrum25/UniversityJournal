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
        Task<bool> AddAsync(ScheduleItem item);
        Task<int> CountAsync(Guid subjectId, Guid groupId);
        Task<bool> IsBusyAsync(Guid groupId, DayOfWeek day, int pair);
    }
}

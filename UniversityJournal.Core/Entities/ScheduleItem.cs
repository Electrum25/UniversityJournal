using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityJournal.Core.Entities
{
    public class ScheduleItem
    {
        public Guid ScheduleItemId { get; set; }
        public Guid SubjectId { get; set; }
        public Guid GroupId { get; set; }
        public Guid TeacherId { get; set; }

        public DayOfWeek DayOfWeek { get; set; } // Понедельник, Вторник...
        public int PairNumber { get; set; } // 1, 2, 3, 4 пара

        // Для связи
        public Subject Subject { get; set; }

        public Teacher Teacher { get; set; }
    }
}

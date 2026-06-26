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

        public DateTime Date { get; set; } 
        public int PairNumber { get; set; } 

        public Subject? Subject { get; set; }

        public Teacher? Teacher { get; set; }
        public Group? Group { get; set; }
    }
}

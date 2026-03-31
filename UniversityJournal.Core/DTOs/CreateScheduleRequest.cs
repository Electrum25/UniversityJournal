using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityJournal.Core.DTOs
{
    public class CreateScheduleRequest
    {
        public Guid SubjectId { get; set; }
        public Guid GroupId { get; set; }
        public Guid TeacherId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public int PairNumber { get; set; }
    }
}

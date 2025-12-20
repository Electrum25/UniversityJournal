using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityJournal.Core.Entities
{
    public enum AttendanceStatus
    {
        Present,
        Absent,
        ValidReason
    }
    public class Attendance
    {
        public Guid AttendanceId { get; set; } 
        public Guid StudentId { get; set; }    
        public Guid SubjectId { get; set; }    
        public DateTime Date { get; set; }    
        public AttendanceStatus Status { get; set; }
    }
}

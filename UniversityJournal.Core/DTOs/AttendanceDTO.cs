using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityJournal.Core.Entities;

namespace UniversityJournal.Core.DTOs
{
    public class AttendanceDTO
    {
        public Guid AttendanceId { get; set; } 
        public Guid StudentId { get; set; }    
        public Guid SubjectId { get; set; }    
        public DateTime Date { get; set; }     
        public AttendanceStatus Status { get; set; } 
        public AttendanceDTO(Attendance attendance)
        {
            AttendanceId = attendance.AttendanceId;
            StudentId = attendance.StudentId;
            SubjectId = attendance.SubjectId;
            Date = attendance.Date;
            Status = attendance.Status;
        }
    }
}

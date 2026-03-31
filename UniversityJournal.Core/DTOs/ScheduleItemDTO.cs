using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityJournal.Core.Entities;

namespace UniversityJournal.Core.DTOs
{
    public class ScheduleItemDTO
    {
        public Guid ScheduleItemId { get; set; }
        public Guid SubjectId { get; set; }
        public string SubjectName { get; set; }
        public Guid GroupId { get; set; }
        public Guid TeacherId { get; set; }
        public string TeacherFullName { get; set; } // "Иванов И.И."

        public DayOfWeek DayOfWeek { get; set; }
        public int PairNumber { get; set; }

        // Время начала и конца (рассчитаем на бэкенде по твоей сетке)
        public string TimeRange { get; set; }

        public Teacher Teacher { get; set; }
    }
}

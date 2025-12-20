using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityJournal.Core.Entities;

namespace UniversityJournal.Core.DTOs
{
    public class TeacherDTO
    {
        public Guid TeacherId { get; set; }
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }

        public TeacherDTO(Teacher teacher)
        {
            TeacherId = teacher.TeacherId;
            UserId = teacher.UserId;
            FirstName = teacher.FirstName;
            LastName = teacher.LastName;
            Patronymic = teacher.Patronymic;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityJournal.Core.Entities;

namespace UniversityJournal.Core.DTOs
{
    public class StudentDTO
    {
        public Guid StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid GroupId { get; set; }

        public StudentDTO(Student student) 
        {
            StudentId = student.StudentId;
            FirstName = student.FirstName;
            LastName = student.LastName;
            GroupId = student.GroupId;
        }
    }
}

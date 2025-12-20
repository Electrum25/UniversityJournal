using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityJournal.Core.Entities;

namespace UniversityJournal.Core.DTOs
{
    public class SubjectDTO
    {
        public Guid SubjectId { get; set; }    
        public string SubjectName { get; set; } 
        public Guid TeacherId { get; set; }   
        public SubjectDTO(Subject subject) 
        {
            SubjectId = subject.SubjectId;
            SubjectName = subject.SubjectName;
            TeacherId = subject.TeacherId;
        }
    }
}

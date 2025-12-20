using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityJournal.Core.Entities
{
    public class Subject
    {
        public Guid SubjectId { get; set; }    
        public string SubjectName { get; set; } 
        public Guid TeacherId { get; set; }    
    }
}

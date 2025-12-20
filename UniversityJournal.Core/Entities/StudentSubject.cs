using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityJournal.Core.Entities
{
    public class StudentSubject
    {
        public Guid StudentId { get; set; }  
        public Guid SubjectId { get; set; }    
        public decimal? FinalScore { get; set; } 
        public string FinalGrade { get; set; } 
    }
}

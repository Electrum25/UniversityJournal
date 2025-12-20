using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityJournal.Core.Entities
{
    public class Grade
    {
        public Guid GradeId { get; set; }     
        public Guid StudentId { get; set; }   
        public Guid SubjectId { get; set; }    
        public int LabNumber { get; set; }    
        public int Score { get; set; }      
        public DateTime Date { get; set; }    
        public string Comment { get; set; }  
    }
}

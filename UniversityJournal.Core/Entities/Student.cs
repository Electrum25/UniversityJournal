using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityJournal.Core.Entities
{
    public class Student
    {
        public Guid StudentId { get; set; }   
        public Guid UserId { get; set; }       
        public string FirstName { get; set; }  
        public string LastName { get; set; }   
        public Guid GroupId { get; set; }    
    }
}

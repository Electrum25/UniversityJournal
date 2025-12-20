using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityJournal.Core.Entities
{
    public class Teacher
    {
        public Guid TeacherId { get; set; }    
        public Guid UserId { get; set; }       
        public string FirstName { get; set; }  
        public string LastName { get; set; }  
        public string Patronymic { get; set; } 
    }
}

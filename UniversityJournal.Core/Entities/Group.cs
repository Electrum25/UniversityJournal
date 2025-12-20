using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityJournal.Core.Entities
{
    public class Group
    {
        public Guid GroupId { get; set; }    
        public string GroupName { get; set; }  
        public string Specialization { get; set; }
        public int Year { get; set; }        
    }
}

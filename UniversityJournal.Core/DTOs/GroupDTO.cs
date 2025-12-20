using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityJournal.Core.Entities;

namespace UniversityJournal.Core.DTOs
{
    public class GroupDTO
    {
        public Guid GroupId { get; set; }      
        public string GroupName { get; set; }  
        public string Specialization { get; set; } 
        public int Year { get; set; }         
        public GroupDTO(Group group) 
        {
            GroupId = group.GroupId;
            GroupName = group.GroupName;
            Specialization = group.Specialization;
            Year = group.Year;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityJournal.Core.Entities
{
    public enum UserRole
    {
        Admin,
        Teacher,
        Student
    }
    public class User
    {
        public Guid UserId { get; set; }      
        public string Login { get; set; }      
        public string PasswordHash { get; set; } 
        public UserRole Role { get; set; }    
        public DateTime CreatedAt { get; set; }
    }
}

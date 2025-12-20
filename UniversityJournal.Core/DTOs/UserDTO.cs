using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityJournal.Core.Entities;

namespace UniversityJournal.Core.DTOs
{
    public class UserDTO
    {
        public Guid UserId { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public UserRole Role { get; set; }
        public DateTime CreatedAt { get; set; }
        public UserDTO(User user) 
        {
            UserId = user.UserId;
            Login = user.Login;
            PasswordHash = user.PasswordHash;
            Role = user.Role;
            CreatedAt = user.CreatedAt;
        }
    }
}

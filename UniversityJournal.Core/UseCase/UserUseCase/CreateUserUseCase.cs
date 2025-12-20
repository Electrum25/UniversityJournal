using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityJournal.Core.Entities;
using UniversityJournal.Core.Repositories;
using BCrypt.Net; // Добавь NuGet пакет BCrypt.Net для хэширования

namespace UniversityJournal.Core.UseCases
{
    public class CreateUserUseCase
    {
        private readonly IUserRepository _userRepository;

        public CreateUserUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Guid> Handle(CreateUserRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Login) || string.IsNullOrWhiteSpace(request.Password))
            {
                throw new ArgumentException("Login and password cannot be empty.");
            }

            var existingUser = await _userRepository.GetByLogin(request.Login);
            if (existingUser != null)
            {
                throw new ArgumentException("Login already exists.");
            }

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var user = new User()
            {
                UserId = Guid.NewGuid(),
                Login = request.Login,
                PasswordHash = passwordHash,
                Role = request.Role,
                CreatedAt = DateTime.UtcNow,
            };

            return await _userRepository.Create(user);
        }

        public class CreateUserRequest
        {
            public string Login { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
            public UserRole Role { get; set; } 
        }
    }
}

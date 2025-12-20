using UniversityJournal.Core.Entities;
using UniversityJournal.Core.Repositories;
using BCrypt.Net;

namespace UniversityJournal.Core.UseCases
{
    public class AuthenticateUserUseCase
    {
        private readonly IUserRepository _userRepository;

        public AuthenticateUserUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User?> Handle(AuthenticateUserRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Login) || string.IsNullOrWhiteSpace(request.Password))
            {
                throw new ArgumentException("Login and password cannot be empty.");
            }

            var user = await _userRepository.GetByLogin(request.Login);
            if (user == null)
            {
                throw new ArgumentException("Invalid login or password.");
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                throw new ArgumentException("Invalid login or password.");
            }

            return user; 
        }

        public class AuthenticateUserRequest
        {
            public string Login { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
        }
    }
}

using Microsoft.AspNetCore.Identity;
using UniversityJournal.Core.Entities;
using UniversityJournal.Core.Identity;
using UniversityJournal.Core.Repositories;

namespace UniversityJournal.Core.UseCases
{
    public class CreateUserUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<UniversityJournalIdentityUser> _userManager;

        public CreateUserUseCase(IUserRepository userRepository, UserManager<UniversityJournalIdentityUser> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public async Task<Guid> Handle(CreateUserRequest request)
        {
            var identityUser = new UniversityJournalIdentityUser
            {
                UserName = request.Login,
                Email = request.Login
            };

            var result = await _userManager.CreateAsync(identityUser, request.Password);

            if (!result.Succeeded)
                throw new Exception($"Ошибка Identity: {string.Join(", ", result.Errors.Select(e => e.Description))}");

            await _userManager.AddToRoleAsync(identityUser, request.Role.ToString());

            var user = new User
            {
                UserId = identityUser.Id,
                Login = request.Login,
                PasswordHash = "PROTECTED",
                Role = request.Role,
                CreatedAt = DateTime.UtcNow,
                IdentityUserId = identityUser.Id
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
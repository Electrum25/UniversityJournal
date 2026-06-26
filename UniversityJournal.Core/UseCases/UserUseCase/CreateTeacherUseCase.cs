using Microsoft.AspNetCore.Identity;
using UniversityJournal.Core.Entities;
using UniversityJournal.Core.Identity;
using UniversityJournal.Core.Repositories;
using UniversityJournal.Core.Common;

namespace UniversityJournal.Core.UseCases
{
    public class CreateTeacherUseCase
    {
        private readonly UserManager<UniversityJournalIdentityUser> _userManager;
        private readonly IUserRepository _userRepository;
        private readonly ITeacherRepository _teacherRepository;

        public CreateTeacherUseCase(UserManager<UniversityJournalIdentityUser> userManager,
            IUserRepository userRepository, ITeacherRepository teacherRepository)
        {
            _userManager = userManager;
            _userRepository = userRepository;
            _teacherRepository = teacherRepository;
        }

        public async Task<Result<Guid>> Handle(CreateTeacherRequest request)
        {
            var existingIdentity = await _userManager.FindByNameAsync(request.Login);
            if (existingIdentity != null)
                return Result<Guid>.Failure("Пользователь с таким логином уже существует.");

            var identityUser = new UniversityJournalIdentityUser
            {
                UserName = request.Login,
                Email = request.Login
            };
            var createResult = await _userManager.CreateAsync(identityUser, request.Password);
            if (!createResult.Succeeded)
                return Result<Guid>.Failure(string.Join(", ", createResult.Errors.Select(e => e.Description)));

            await _userManager.AddToRoleAsync(identityUser, UserRole.Teacher.ToString());

            var user = new User
            {
                UserId = identityUser.Id,
                Login = request.Login,
                PasswordHash = "PROTECTED",
                Role = UserRole.Teacher,
                CreatedAt = DateTime.UtcNow,
                IdentityUserId = identityUser.Id
            };
            await _userRepository.Create(user);

            var teacher = new Teacher
            {
                TeacherId = Guid.NewGuid(),
                UserId = user.UserId,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Patronymic = request.Patronymic
            };
            var teacherId = await _teacherRepository.Create(teacher);
            return Result<Guid>.Success(teacherId);
        }
        public class CreateTeacherRequest
        {
            public string Login { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
            public string FirstName { get; set; } = string.Empty;
            public string LastName { get; set; } = string.Empty;
            public string? Patronymic { get; set; }
        }
    }
    
}
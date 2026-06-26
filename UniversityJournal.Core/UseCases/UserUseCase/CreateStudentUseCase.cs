using Microsoft.AspNetCore.Identity;
using UniversityJournal.Core.Entities;
using UniversityJournal.Core.Repositories;
using UniversityJournal.Core.Identity;


namespace UniversityJournal.Core.UseCases
{
    public class CreateStudentUseCase
    {
        private readonly UserManager<UniversityJournalIdentityUser> _userManager;
        private readonly IUserRepository _userRepository;
        private readonly IStudentRepository _studentRepository;

        public CreateStudentUseCase(UserManager<UniversityJournalIdentityUser> userManager, IUserRepository userRepository, IStudentRepository studentRepository)
        {
            _userManager = userManager;
            _userRepository = userRepository;
            _studentRepository = studentRepository;
        }

        public async Task<Guid> Handle(CreateStudentRequest request)
        {
            var email = !string.IsNullOrEmpty(request.Email)
        ? request.Email
        : $"{request.Login}@university.local";

            var identityUser = new UniversityJournalIdentityUser
            {
                UserName = request.Login,
                Email = request.Login
            };

            var result = await _userManager.CreateAsync(identityUser, request.Password);

            if (!result.Succeeded)
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));

            await _userManager.AddToRoleAsync(identityUser, UserRole.Student.ToString());

            var identityId = identityUser.Id;

            var user = new User
            {
                UserId = identityId,
                Login = request.Login,
                PasswordHash = "PROTECTED", 
                Role = UserRole.Student,
                CreatedAt = DateTime.UtcNow,
                IdentityUserId = identityId 
            };
            await _userRepository.Create(user);

            var student = new Student
            {
                StudentId = Guid.NewGuid(),
                UserId = user.UserId,
                FirstName = request.FirstName,
                LastName = request.LastName,
                GroupId = request.GroupId
            };

            return await _studentRepository.Create(student);
        }

        public class CreateStudentRequest
        {
            public string Login { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
            public string?  Email { get; set; } = string.Empty;
            public string FirstName { get; set; } = string.Empty;
            public string LastName { get; set; } = string.Empty;
            public Guid GroupId { get; set; }
        }
    }
}
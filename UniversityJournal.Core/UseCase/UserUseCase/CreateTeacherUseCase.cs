using UniversityJournal.Core.Entities;
using UniversityJournal.Core.Repositories;
using BCrypt.Net;

namespace UniversityJournal.Core.UseCases
{
    public class CreateTeacherUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly ITeacherRepository _teacherRepository;

        public CreateTeacherUseCase(IUserRepository userRepository, ITeacherRepository teacherRepository)
        {
            _userRepository = userRepository;
            _teacherRepository = teacherRepository;
        }

        public async Task<Guid> Handle(CreateTeacherRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Login) || string.IsNullOrWhiteSpace(request.Password) ||
                string.IsNullOrWhiteSpace(request.FirstName) || string.IsNullOrWhiteSpace(request.LastName))
            {
                throw new ArgumentException("All fields are required.");
            }

            var existingUser = await _userRepository.GetByLogin(request.Login);
            if (existingUser != null)
            {
                throw new ArgumentException("Login already exists.");
            }

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            var user = new User
            {
                UserId = Guid.NewGuid(),
                Login = request.Login,
                PasswordHash = passwordHash,
                Role = UserRole.Teacher,
                CreatedAt = DateTime.UtcNow,
            };
            await _userRepository.Create(user);

            var teacher = new Teacher
            {
                TeacherId = Guid.NewGuid(),
                UserId = user.UserId,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Patronymic = request.Patronymic,
            };
            return await _teacherRepository.Create(teacher);
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

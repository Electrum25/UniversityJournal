using UniversityJournal.Core.Entities;
using UniversityJournal.Core.Repositories;
using BCrypt.Net;

namespace UniversityJournal.Core.UseCases
{
    public class CreateStudentUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IStudentRepository _studentRepository;

        public CreateStudentUseCase(IUserRepository userRepository, IStudentRepository studentRepository)
        {
            _userRepository = userRepository;
            _studentRepository = studentRepository;
        }

        public async Task<Guid> Handle(CreateStudentRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Login) || string.IsNullOrWhiteSpace(request.Password) ||
                string.IsNullOrWhiteSpace(request.FirstName) || string.IsNullOrWhiteSpace(request.LastName) ||
                request.GroupId == Guid.Empty)
            {
                throw new ArgumentException("All fields are required, including GroupId.");
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
                Role = UserRole.Student,
                CreatedAt = DateTime.UtcNow,
            };
            await _userRepository.Create(user);

            var student = new Student
            {
                StudentId = Guid.NewGuid(),
                UserId = user.UserId,
                FirstName = request.FirstName,
                LastName = request.LastName,
                GroupId = request.GroupId,
            };
            return await _studentRepository.Create(student);
        }

        public class CreateStudentRequest
        {
            public string Login { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
            public string FirstName { get; set; } = string.Empty;
            public string LastName { get; set; } = string.Empty;
            public Guid GroupId { get; set; }
        }
    }
}

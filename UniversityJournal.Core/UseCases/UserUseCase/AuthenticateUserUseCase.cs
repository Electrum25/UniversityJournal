using UniversityJournal.Core.Entities;
using UniversityJournal.Core.Repositories;
using BCrypt.Net;

namespace UniversityJournal.Core.UseCases
{
    public class AuthenticateUserUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ITeacherRepository _teacherRepository;

        public AuthenticateUserUseCase(
            IUserRepository userRepository,
            IStudentRepository studentRepository,
            ITeacherRepository teacherRepository)
        {
            _userRepository = userRepository;
            _studentRepository = studentRepository;
            _teacherRepository = teacherRepository;
        }

        public async Task<AuthData?> Handle(AuthenticateUserRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Login) || string.IsNullOrWhiteSpace(request.Password))
            {
                return null;
            }

            var user = await _userRepository.GetByLogin(request.Login);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return null;
            }

            Guid? businessId = null;

            if (user.Role == UserRole.Student) 
            {
                var students = await _studentRepository.GetAll();
                businessId = students?.FirstOrDefault(s => s.UserId == user.UserId)?.StudentId;
            }
            else if (user.Role == UserRole.Teacher)
            {
                var teachers = await _teacherRepository.GetAll();
                businessId = teachers?.FirstOrDefault(t => t.UserId == user.UserId)?.TeacherId;
            }

            return new AuthData
            {
                User = user,
                BusinessId = businessId
            };
        }

        public class AuthenticateUserRequest
        {
            public string Login { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
        }

        public class AuthData
        {
            public User User { get; set; }
            public Guid? BusinessId { get; set; }
        }
    }
}
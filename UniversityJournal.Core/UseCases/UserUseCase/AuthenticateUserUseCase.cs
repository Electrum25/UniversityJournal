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

        // Добавляем репозитории студентов и учителей в конструктор
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
                return null; // Или выбрасывай исключение, если так удобнее
            }

            // Ищем пользователя (по логину или email, смотря как реализовано в репозитории)
            var user = await _userRepository.GetByLogin(request.Login);

            // Проверка пароля (используем PasswordHash, как в твоем исходном коде)
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return null;
            }

            Guid? businessId = null;

            // Логика поиска ID студента или учителя
            if (user.Role == UserRole.Student) // Сравниваем напрямую с типом UserRole
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

        // Вспомогательные классы
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
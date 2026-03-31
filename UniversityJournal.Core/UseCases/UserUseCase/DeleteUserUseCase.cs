using UniversityJournal.Core.Entities;
using UniversityJournal.Core.Repositories;

namespace UniversityJournal.Core.UseCases
{
    public class DeleteUserUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IStudentRepository _studentRepository;

        public DeleteUserUseCase(
            IUserRepository userRepository,
            ITeacherRepository teacherRepository,
            IStudentRepository studentRepository)
        {
            _userRepository = userRepository;
            _teacherRepository = teacherRepository;
            _studentRepository = studentRepository;
        }

        public async Task<bool> Handle(Guid userId)
        {
            var user = await _userRepository.Get(userId);
            if (user == null) throw new ArgumentException("Пользователь не найден.");

            // Логика защиты админа
            if (user.Role == UserRole.Admin)
            {
                var allUsers = await _userRepository.GetAll();
                if (allUsers?.Count(u => u.Role == UserRole.Admin) <= 1)
                    throw new InvalidOperationException("Нельзя удалить последнего админа.");
            }

            // Удаление роли (Teacher или Student)
            // База данных автоматически удалит записи в связанных таблицах (Subjects, Grades, Attendances)
            // благодаря настройке OnDelete(DeleteBehavior.Cascade) в DbContext
            if (user.Role == UserRole.Teacher)
            {
                var teachers = await _teacherRepository.GetAll();
                var teacher = teachers?.FirstOrDefault(t => t.UserId == userId);
                if (teacher != null)
                {
                    await _teacherRepository.Delete(teacher.TeacherId);
                }
            }
            else if (user.Role == UserRole.Student)
            {
                var students = await _studentRepository.GetAll();
                var student = students?.FirstOrDefault(s => s.UserId == userId);
                if (student != null)
                {
                    await _studentRepository.Delete(student.StudentId);
                }
            }

            // Удаление основной записи пользователя
            await _userRepository.Delete(userId);
            return true;
        }
    }
}
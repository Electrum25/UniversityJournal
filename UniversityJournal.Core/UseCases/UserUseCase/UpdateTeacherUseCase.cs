using Microsoft.AspNetCore.Identity;
using UniversityJournal.Core.Entities;
using UniversityJournal.Core.Identity;
using UniversityJournal.Core.Repositories;

namespace UniversityJournal.Core.UseCases
{
    public class UpdateTeacherUseCase
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IUserRepository _userRepository;
        private readonly UserManager<UniversityJournalIdentityUser> _userManager;

        public UpdateTeacherUseCase(
            ITeacherRepository teacherRepository,
            IUserRepository userRepository,
            UserManager<UniversityJournalIdentityUser> userManager)
        {
            _teacherRepository = teacherRepository;
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public async Task Handle(UpdateTeacherRequest request)
        {
            var teacher = await _teacherRepository.Get(request.TeacherId);
            if (teacher == null) throw new ArgumentException("Преподаватель не найден.");

            teacher.FirstName = request.FirstName;
            teacher.LastName = request.LastName;
            teacher.Patronymic = request.Patronymic;

            var success = await _teacherRepository.Update(teacher);
            if (!success) throw new Exception("Не удалось обновить преподавателя.");
        }

        public class UpdateTeacherRequest
        {
            public Guid TeacherId { get; set; }
            public string FirstName { get; set; } = string.Empty;
            public string LastName { get; set; } = string.Empty;
            public string? Patronymic { get; set; }
        }
    }
}
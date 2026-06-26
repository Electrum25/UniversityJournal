using Microsoft.AspNetCore.Identity;
using UniversityJournal.Core.Common;
using UniversityJournal.Core.Entities;
using UniversityJournal.Core.Identity;
using UniversityJournal.Core.Repositories;

namespace UniversityJournal.Core.UseCases
{
    public class DeleteUserUseCase
    {
        private readonly UserManager<UniversityJournalIdentityUser> _userManager;
        private readonly IUserRepository _userRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IStudentRepository _studentRepository;

        public DeleteUserUseCase(UserManager<UniversityJournalIdentityUser> userManager,
            IUserRepository userRepository, ITeacherRepository teacherRepository,
            IStudentRepository studentRepository)
        {
            _userManager = userManager;
            _userRepository = userRepository;
            _teacherRepository = teacherRepository;
            _studentRepository = studentRepository;
        }

        public async Task<Result<bool>> Handle(Guid userId)
        {
            var user = await _userRepository.Get(userId);
            if (user == null)
                return Result<bool>.Failure("Пользователь не найден.");

            if (user.IdentityUserId.HasValue)
            {
                var identityUser = await _userManager.FindByIdAsync(user.IdentityUserId.Value.ToString());
                if (identityUser != null)
                {
                    if (user.Role == UserRole.Admin)
                    {
                        var admins = await _userManager.GetUsersInRoleAsync("Admin");
                        if (admins.Count <= 1)
                            return Result<bool>.Failure("Нельзя удалить последнего администратора.");
                    }
                    var identityDeleteResult = await _userManager.DeleteAsync(identityUser);
                    if (!identityDeleteResult.Succeeded)
                        return Result<bool>.Failure(string.Join(", ", identityDeleteResult.Errors.Select(e => e.Description)));
                }
            }

            if (user.Role == UserRole.Teacher)
            {
                var teachers = await _teacherRepository.GetAll();
                var teacher = teachers?.FirstOrDefault(t => t.UserId == userId);
                if (teacher != null)
                    await _teacherRepository.Delete(teacher.TeacherId);
            }
            else if (user.Role == UserRole.Student)
            {
                var students = await _studentRepository.GetAll();
                var student = students?.FirstOrDefault(s => s.UserId == userId);
                if (student != null)
                    await _studentRepository.Delete(student.StudentId);
            }

            await _userRepository.Delete(userId);
            return Result<bool>.Success(true);
        }
    }
}
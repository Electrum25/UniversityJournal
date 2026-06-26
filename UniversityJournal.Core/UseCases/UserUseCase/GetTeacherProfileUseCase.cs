using UniversityJournal.Core.DTOs;
using UniversityJournal.Core.Repositories;

namespace UniversityJournal.Core.UseCases
{
    public class GetTeacherProfileUseCase
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IUserRepository _userRepository;

        public GetTeacherProfileUseCase(ITeacherRepository teacherRepository, IUserRepository userRepository)
        {
            _teacherRepository = teacherRepository;
            _userRepository = userRepository;
        }

        public async Task<TeacherDTO?> Handle(Guid userId)
        {
            var allTeachers = await _teacherRepository.GetAll();
            var teacher = allTeachers?.FirstOrDefault(t => t.UserId == userId);
            return teacher == null ? null : new TeacherDTO(teacher);
        }

        public async Task<TeacherDTO?> HandleByIdentity(Guid identityId)
        {
            var allUsers = await _userRepository.GetAll();
            var user = allUsers?.FirstOrDefault(u => u.IdentityUserId == identityId);
            if (user == null) return null;
            return await Handle(user.UserId);
        }
    }
}
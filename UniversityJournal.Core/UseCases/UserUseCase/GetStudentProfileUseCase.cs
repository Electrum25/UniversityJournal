using UniversityJournal.Core.DTOs;
using UniversityJournal.Core.Repositories;

namespace UniversityJournal.Core.UseCases
{
    public class GetStudentProfileUseCase
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IUserRepository _userRepository;

        public GetStudentProfileUseCase(IStudentRepository studentRepository, IUserRepository userRepository)
        {
            _studentRepository = studentRepository;
            _userRepository = userRepository;
        }

        public async Task<StudentDTO?> Handle(Guid userId)
        {
            var allStudents = await _studentRepository.GetAll();
            var student = allStudents?.FirstOrDefault(s => s.UserId == userId);
            return student == null ? null : new StudentDTO(student);
        }

        public async Task<StudentDTO?> HandleByIdentity(Guid identityId)
        {
            var allUsers = await _userRepository.GetAll();
            var user = allUsers?.FirstOrDefault(u => u.IdentityUserId == identityId);
            if (user == null) return null;
            return await Handle(user.UserId);
        }
    }
}
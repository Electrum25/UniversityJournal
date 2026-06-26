using Microsoft.AspNetCore.Identity;
using UniversityJournal.Core.Entities;
using UniversityJournal.Core.Identity;
using UniversityJournal.Core.Repositories;

namespace UniversityJournal.Core.UseCases
{
    public class UpdateStudentUseCase
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IUserRepository _userRepository;
        private readonly UserManager<UniversityJournalIdentityUser> _userManager;

        public UpdateStudentUseCase(
            IStudentRepository studentRepository,
            IGroupRepository groupRepository,
            IUserRepository userRepository,
            UserManager<UniversityJournalIdentityUser> userManager)
        {
            _studentRepository = studentRepository;
            _groupRepository = groupRepository;
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public async Task Handle(UpdateStudentRequest request)
        {
            var student = await _studentRepository.Get(request.StudentId);
            if (student == null) throw new ArgumentException("Студент не найден.");

            if (!string.IsNullOrWhiteSpace(request.NewLogin))
            {
                var user = await _userRepository.Get(student.UserId);
                if (user != null && user.IdentityUserId.HasValue)
                {
                    var identityUser = await _userManager.FindByIdAsync(user.IdentityUserId.Value.ToString());
                    if (identityUser != null)
                    {
                        identityUser.UserName = request.NewLogin;
                        await _userManager.UpdateAsync(identityUser);
                        user.Login = request.NewLogin;
                        await _userRepository.Update(user);
                    }
                }
            }

            if (request.GroupId.HasValue)
            {
                var group = await _groupRepository.Get(request.GroupId.Value);
                if (group != null) student.GroupId = request.GroupId.Value;
            }

            student.FirstName = request.FirstName;
            student.LastName = request.LastName;

            await _studentRepository.Update(student);
        }

        public class UpdateStudentRequest
        {
            public Guid StudentId { get; set; }
            public string FirstName { get; set; } = string.Empty;
            public string LastName { get; set; } = string.Empty;
            public Guid? GroupId { get; set; }
            public string? NewLogin { get; set; }
        }
    }
}
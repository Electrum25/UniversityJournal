using UniversityJournal.Core.DTOs;
using UniversityJournal.Core.Repositories;

namespace UniversityJournal.Core.UseCases
{
    public class GetUserByIdentityUseCase
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdentityUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDTO?> Handle(Guid identityId)
        {
            var users = await _userRepository.GetAll();
            var user = users?.FirstOrDefault(u => u.IdentityUserId == identityId);
            return user == null ? null : new UserDTO(user);
        }
    }
}
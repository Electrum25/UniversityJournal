using UniversityJournal.Core.DTOs;
using UniversityJournal.Core.Repositories;

namespace UniversityJournal.Core.UseCases
{
    public class GetUserByIdUseCase
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDTO?> Handle(Guid userId)
        {
            var user = await _userRepository.Get(userId);
            return user == null ? null : new UserDTO(user);
        }
    }
}
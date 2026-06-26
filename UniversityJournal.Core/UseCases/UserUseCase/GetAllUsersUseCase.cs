using UniversityJournal.Core.DTOs;
using UniversityJournal.Core.Repositories;

namespace UniversityJournal.Core.UseCases
{
    public class GetAllUsersUseCase
    {
        private readonly IUserRepository _userRepository;

        public GetAllUsersUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserDTO>> Handle()
        {
            var users = await _userRepository.GetAll();
            return users?.Select(u => new UserDTO(u)).ToList() ?? new List<UserDTO>();
        }
    }
}
using UniversityJournal.Core.Entities;
using UniversityJournal.Core.Repositories;

namespace UniversityJournal.Core.UseCases
{
    public class GetGroupsUseCase
    {
        private readonly IGroupRepository _groupRepository;

        public GetGroupsUseCase(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<List<Group>?> Handle()
        {
            return await _groupRepository.GetAll();
        }
    }
}

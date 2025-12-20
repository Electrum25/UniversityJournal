using UniversityJournal.Core.Entities;
using UniversityJournal.Core.Repositories;

namespace UniversityJournal.Core.UseCases
{
    public class CreateGroupUseCase
    {
        private readonly IGroupRepository _groupRepository;

        public CreateGroupUseCase(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<Guid> Handle(CreateGroupRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.GroupName) || string.IsNullOrWhiteSpace(request.Specialization) || request.Year <= 0)
            {
                throw new ArgumentException("All fields are required.");
            }

            var group = new Group
            {
                GroupId = Guid.NewGuid(),
                GroupName = request.GroupName,
                Specialization = request.Specialization,
                Year = request.Year
            };
            return await _groupRepository.Create(group);
        }

        public class CreateGroupRequest
        {
            public string GroupName { get; set; } = string.Empty;
            public string Specialization { get; set; } = string.Empty;
            public int Year { get; set; }
        }
    }
}
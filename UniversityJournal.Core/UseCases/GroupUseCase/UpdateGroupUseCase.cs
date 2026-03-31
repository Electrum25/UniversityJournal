using UniversityJournal.Core.Entities;
using UniversityJournal.Core.Repositories;

namespace UniversityJournal.Core.UseCases
{
    public class UpdateGroupUseCase
    {
        private readonly IGroupRepository _groupRepository;

        public UpdateGroupUseCase(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task Handle(UpdateGroupRequest request)
        {
            var group = await _groupRepository.Get(request.GroupId);
            if (group == null)
            {
                throw new ArgumentException("Группа не найдена.", nameof(request.GroupId));
            }

            group.GroupName = request.GroupName;
            group.Specialization = request.Specialization;
            group.Year = request.Year;

            var success = await _groupRepository.Update(group);
            if (!success)
            {
                throw new Exception("Не удалось обновить группу.");
            }
        }

        public class UpdateGroupRequest
        {
            public Guid GroupId { get; set; }
            public string GroupName { get; set; }
            public string Specialization { get; set; }
            public int Year { get; set; }
        }
    }
}
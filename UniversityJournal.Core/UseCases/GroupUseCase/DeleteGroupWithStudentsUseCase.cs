using UniversityJournal.Core.Entities;
using UniversityJournal.Core.Repositories;

namespace UniversityJournal.Core.UseCases
{
    public class DeleteGroupWithStudentsUseCase
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IStudentRepository _studentRepository;

        public DeleteGroupWithStudentsUseCase(
            IGroupRepository groupRepository,
            IStudentRepository studentRepository)
        {
            _groupRepository = groupRepository;
            _studentRepository = studentRepository;
        }

        public async Task<bool> Handle(Guid groupId)
        {
            var group = await _groupRepository.Get(groupId);
            if (group == null)
            {
                throw new ArgumentException("Группа не найдена.", nameof(groupId));
            }

            var students = await _studentRepository.GetByGroup(groupId);
            if (students != null && students.Any())
            {
                foreach (var student in students)
                {
                    await _studentRepository.Delete(student.StudentId);
                }
            }

            await _groupRepository.Delete(groupId);
            return true;
        }
    }
}
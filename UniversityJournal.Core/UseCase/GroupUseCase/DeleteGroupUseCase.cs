using UniversityJournal.Core.Entities;
using UniversityJournal.Core.Repositories;

namespace UniversityJournal.Core.UseCases
{
    public class DeleteGroupUseCase
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IStudentRepository _studentRepository;

        public DeleteGroupUseCase(
            IGroupRepository groupRepository,
            IStudentRepository studentRepository)
        {
            _groupRepository = groupRepository;
            _studentRepository = studentRepository;
        }

        public async Task<bool> Handle(Guid groupId, bool deleteWithStudents = false)
        {
            var group = await _groupRepository.Get(groupId);
            if (group == null)
            {
                throw new ArgumentException("Группа не найдена.", nameof(groupId));
            }

            var students = await _studentRepository.GetByGroup(groupId);

            if (deleteWithStudents)
            {
                if (students != null && students.Any())
                {
                    foreach (var student in students)
                    {
                        await _studentRepository.Delete(student.StudentId);
                    }
                }
            }
            else
            {
                if (students != null && students.Any())
                {
                    throw new InvalidOperationException("Нельзя удалить группу, так как в ней есть студенты. Отметьте опцию 'Удалить вместе со студентами' для каскадного удаления.");
                }
            }

            await _groupRepository.Delete(groupId);
            return true;
        }
    }
}

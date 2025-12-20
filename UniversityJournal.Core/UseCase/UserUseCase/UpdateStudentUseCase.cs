using UniversityJournal.Core.Entities;
using UniversityJournal.Core.Repositories;

namespace UniversityJournal.Core.UseCases
{
    public class UpdateStudentUseCase
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IGroupRepository _groupRepository;

        public UpdateStudentUseCase(IStudentRepository studentRepository, IGroupRepository groupRepository)
        {
            _studentRepository = studentRepository;
            _groupRepository = groupRepository;
        }

        public async Task Handle(UpdateStudentRequest request)
        {
            var student = await _studentRepository.Get(request.StudentId);
            if (student == null)
            {
                throw new ArgumentException("Студент не найден.", nameof(request.StudentId));
            }

            var group = await _groupRepository.Get(request.GroupId);
            if (group == null)
            {
                throw new ArgumentException("Группа не найдена.", nameof(request.GroupId));
            }

            student.FirstName = request.FirstName;
            student.LastName = request.LastName;
            student.GroupId = request.GroupId;

            var success = await _studentRepository.Update(student);
            if (!success)
            {
                throw new Exception("Не удалось обновить студента.");
            }
        }

        public class UpdateStudentRequest
        {
            public Guid StudentId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public Guid GroupId { get; set; }
        }
    }
}
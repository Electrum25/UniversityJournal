using UniversityJournal.Core.Entities;
using UniversityJournal.Core.Repositories;

namespace UniversityJournal.Core.UseCases
{
    public class UpdateTeacherUseCase
    {
        private readonly ITeacherRepository _teacherRepository;

        public UpdateTeacherUseCase(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        public async Task Handle(UpdateTeacherRequest request)
        {
            var teacher = await _teacherRepository.Get(request.TeacherId);
            if (teacher == null)
            {
                throw new ArgumentException("Преподаватель не найден.", nameof(request.TeacherId));
            }

            teacher.FirstName = request.FirstName;
            teacher.LastName = request.LastName;
            teacher.Patronymic = request.Patronymic;

            var success = await _teacherRepository.Update(teacher);
            if (!success)
            {
                throw new Exception("Не удалось обновить преподавателя.");
            }
        }

        public class UpdateTeacherRequest
        {
            public Guid TeacherId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Patronymic { get; set; }  
        }
    }
}
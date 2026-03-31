using Microsoft.AspNetCore.Authorization; // Добавляем поддержку атрибутов
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityJournal.Core.DTOs;
using UniversityJournal.Core.Entities;
using UniversityJournal.Core.Repositories;
using UniversityJournal.Core.UseCases;
using UniversityJournal.EfCore;

namespace UniversityJournal.Server.Controllers
{
    [Authorize] // По умолчанию все методы требуют авторизации
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupRepository _groupRepository;

        public GroupsController(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        /// <summary>
        /// Получить все группы. Доступно Студентам, Преподавателям и Админам.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromServices] GetGroupsUseCase getGroupsUseCase)
        {
            var groups = await getGroupsUseCase.Handle();
            return Ok(groups ?? new List<Group>());
        }

        /// <summary>
        /// Получить группу по ID. Доступно всем ролям.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var group = await _groupRepository.Get(id);
            if (group == null) return NotFound("Группа не найдена");
            return Ok(group);
        }

        /// <summary>
        /// Создать группу. Только Администратор.
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(
            [FromBody] CreateGroupUseCase.CreateGroupRequest request,
            [FromServices] CreateGroupUseCase createUseCase)
        {
            try
            {
                var id = await createUseCase.Handle(request);
                return Ok(id);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Обновить данные группы. Только Администратор.
        /// </summary>
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(
            [FromBody] UpdateGroupUseCase.UpdateGroupRequest request,
            [FromServices] UpdateGroupUseCase updateUseCase)
        {
            try
            {
                await updateUseCase.Handle(request);
                return Ok("Данные группы обновлены");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Удалить группу. Только Администратор.
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(
            Guid id,
            [FromServices] DeleteGroupUseCase deleteUseCase,
            [FromQuery] bool deleteWithStudents = false)
        {
            try
            {
                await deleteUseCase.Handle(id, deleteWithStudents);
                return Ok("Группа удалена");
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Получить список студентов конкретной группы.
        /// </summary>
        [HttpGet("{id}/students")]
        [Authorize(Roles = "Admin,Teacher")] // Обычно это нужно админам и учителям
        public async Task<IActionResult> GetStudentsByGroup(
            Guid id,
            [FromServices] IStudentRepository studentRepository)
        {
            var students = await studentRepository.GetByGroup(id);

            if (students == null || !students.Any())
                return Ok(new List<Student>()); // Возвращаем пустой список, если никого нет

            return Ok(students);
        }

        [HttpGet("{id}/students-with-enrollment/{subjectId}")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> GetStudentsWithEnrollment(
    Guid id,
    Guid subjectId,
    [FromServices] IStudentRepository studentRepository,
    [FromServices] UniversityJournalDbContext context) // Для быстрой проверки связи
        {
            var students = await studentRepository.GetByGroup(id);
            if (students == null) return Ok(new List<StudentEnrollmentDTO>());

            // Получаем ID всех студентов, которые уже записаны на этот предмет
            var enrolledIds = await context.StudentSubjects
                .Where(ss => ss.SubjectId == subjectId)
                .Select(ss => ss.StudentId)
                .ToListAsync();

            var result = students.Select(s => new StudentEnrollmentDTO
            {
                StudentId = s.StudentId,
                FirstName = s.FirstName,
                LastName = s.LastName,
                IsEnrolled = enrolledIds.Contains(s.StudentId)
            }).ToList();

            return Ok(result);
        }
    }
}
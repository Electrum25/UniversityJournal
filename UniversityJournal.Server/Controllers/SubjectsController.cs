using Microsoft.AspNetCore.Mvc;
using UniversityJournal.Core.Entities;
using UniversityJournal.Core.UseCases;
using UniversityJournal.Core.Repositories;
using UniversityJournal.Core.DTOs;
using Microsoft.AspNetCore.Authorization; // Подключаем авторизацию

namespace UniversityJournal.Server.Controllers
{
    [Authorize] // Весь контроллер требует авторизации
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly ISubjectRepository _subjectRepository;

        public SubjectsController(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        #region Subject Management (Управление предметами)

        /// <summary>
        /// Создать новый предмет. Только Админ.
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(
            [FromBody] CreateSubjectUseCase.CreateSubjectRequest request,
            [FromServices] CreateSubjectUseCase createUseCase)
        {
            try
            {
                var id = await createUseCase.Handle(request);
                return Ok(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Посмотреть предметы конкретного учителя. Доступно учителям и админам.
        /// </summary>
        [HttpGet("teacher/{teacherId}")]
        [Authorize(Roles = "Teacher,Admin")]
        public async Task<IActionResult> GetByTeacher(
    Guid teacherId,
    [FromServices] GetSubjectsByTeacherUseCase getUseCase)
        {
            var subjects = await getUseCase.Handle(teacherId);

            // ОБЯЗАТЕЛЬНО маппим в DTO, чтобы имена полей совпали с моделью во Flutter
            var dtos = subjects.Select(s => new SubjectDTO(s)).ToList();

            return Ok(dtos);
        }

        /// <summary>
        /// Обновить данные предмета. Только Админ.
        /// </summary>
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(
            [FromBody] UpdateSubjectUseCase.UpdateSubjectRequest request,
            [FromServices] UpdateSubjectUseCase updateUseCase)
        {
            try
            {
                await updateUseCase.Handle(request);
                return Ok("Предмет обновлен");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Удалить предмет. Только Админ.
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(
            Guid id,
            [FromServices] DeleteSubjectUseCase deleteUseCase,
            [FromQuery] bool deleteWithRelated = false)
        {
            try
            {
                await deleteUseCase.Handle(id, deleteWithRelated);
                return Ok("Предмет удален");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion

        #region Student Assignments (Привязка студентов к предметам)

        /// <summary>
        /// Привязать студента к предмету (Зачисление). Только Админ.
        /// </summary>
        [HttpPost("enroll")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EnrollStudent(
            [FromBody] CreateStudentSubjectUseCase.CreateStudentSubjectRequest request,
            [FromServices] CreateStudentSubjectUseCase enrollUseCase)
        {
            try
            {
                await enrollUseCase.Handle(request);
                return Ok("Студент успешно записан на предмет");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Отвязать студента от предмета. Только Админ.
        /// </summary>
        [HttpDelete("unlink")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UnlinkStudent(
            [FromBody] UnlinkStudentSubjectUseCase.UnlinkStudentSubjectRequest request,
            [FromServices] UnlinkStudentSubjectUseCase unlinkUseCase)
        {
            try
            {
                await unlinkUseCase.Handle(request);
                return Ok("Связь удалена");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")] // Только админ может видеть всё
        public async Task<IActionResult> GetAll()
        {
            // Здесь используй свой репозиторий напрямую или через UseCase
            var subjects = await _subjectRepository.GetAll();

            // Маппим в DTO, чтобы Flutter понимал структуру
            var dtos = subjects.Select(s => new SubjectDTO(s)).ToList();

            return Ok(dtos);
        }

        /// <summary>
        /// Выставить финальную оценку. Доступно Учителю и Админу.
        /// </summary>
        [HttpPut("final-grade")]
        [Authorize(Roles = "Teacher,Admin")]
        public async Task<IActionResult> UpdateFinalGrade(
            [FromBody] UpdateStudentSubjectUseCase.UpdateStudentSubjectRequest request,
            [FromServices] UpdateStudentSubjectUseCase updateGradeUseCase)
        {
            try
            {
                await updateGradeUseCase.Handle(request);
                return Ok("Финальная оценка обновлена");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Получить список студентов, зачисленных на предмет.
        /// </summary>
        [HttpGet("enrolled-students/{subjectId}")]
        [Authorize(Roles = "Teacher,Admin")]
        public async Task<IActionResult> GetEnrolledStudents(Guid subjectId, [FromServices] IStudentRepository studentRepository, [FromServices] IStudentSubjectRepository studentSubjectRepository)
        {
            try
            {
                // 1. Получаем все связи "Студент-Предмет" для данного предмета
                var assignments = await studentSubjectRepository.GetBySubject(subjectId);
                if (assignments == null || !assignments.Any())
                    return Ok(new List<StudentDTO>());

                // 2. Получаем ID всех зачисленных студентов
                var studentIds = assignments.Select(a => a.StudentId).ToList();

                // 3. Получаем полные данные студентов из репозитория студентов
                var allStudents = await studentRepository.GetAll();
                var enrolledStudents = allStudents
                    .Where(s => studentIds.Contains(s.StudentId))
                    .Select(s => new StudentDTO(s)) // Маппим в DTO
                    .ToList();

                return Ok(enrolledStudents);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Получить список предметов, на которые зачислен студент.
        /// </summary>
        [HttpGet("student/{studentId}")]
        [Authorize(Roles = "Student,Admin")]
        public async Task<IActionResult> GetByStudent(Guid studentId, [FromServices] IStudentSubjectRepository studentSubjectRepository)
        {
            try
            {
                // 1. Получаем связи студента с предметами
                var assignments = await studentSubjectRepository.GetByStudent(studentId);

                // 2. Получаем сами объекты предметов из репозитория
                var allSubjects = await _subjectRepository.GetAll();
                var studentSubjectIds = assignments.Select(a => a.SubjectId).ToList();

                var studentSubjects = allSubjects
                    .Where(s => studentSubjectIds.Contains(s.SubjectId))
                    .Select(s => new SubjectDTO(s))
                    .ToList();

                return Ok(studentSubjects);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion
    }
}
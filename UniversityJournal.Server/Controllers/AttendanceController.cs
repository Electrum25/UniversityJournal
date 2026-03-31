using Microsoft.AspNetCore.Mvc;
using UniversityJournal.Core.Entities;
using UniversityJournal.Core.UseCases;
using UniversityJournal.Core.Repositories;
using Microsoft.AspNetCore.Authorization; // Обязательно добавляем

namespace UniversityJournal.Server.Controllers
{
    [Authorize] // По умолчанию доступ только залогиненным пользователям
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceRepository _attendanceRepository;

        public AttendanceController(IAttendanceRepository attendanceRepository)
        {
            _attendanceRepository = attendanceRepository;
        }

        /// <summary>
        /// Отметить посещаемость. Доступно учителям и админам.
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Teacher,Admin")]
        public async Task<IActionResult> MarkAttendance(
            [FromBody] CreateAttendanceUseCase.CreateAttendanceRequest request,
            [FromServices] CreateAttendanceUseCase createUseCase,
            [FromHeader] Guid teacherId)
        {
            try
            {
                await createUseCase.Handle(request, teacherId);
                return Ok("Запись о посещаемости создана");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// История по предмету. Только для персонала (Учителя/Админы).
        /// </summary>
        [HttpGet("subject/{subjectId}")]
        [Authorize(Roles = "Teacher,Admin")]
        public async Task<IActionResult> GetBySubject(Guid subjectId)
        {
            var list = await _attendanceRepository.GetBySubject(subjectId);
            return Ok(list ?? new List<Attendance>());
        }

        /// <summary>
        /// Пропуски студента. Видят все, но логика фронтенда должна ограничивать студента его собственным ID.
        /// </summary>
        [HttpGet("student/{studentId}")]
        [Authorize(Roles = "Student,Teacher,Admin")]
        public async Task<IActionResult> GetByStudent(Guid studentId)
        {
            var list = await _attendanceRepository.GetByStudent(studentId);
            return Ok(list ?? new List<Attendance>());
        }

        /// <summary>
        /// Удаление записи. Только для Администратора.
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _attendanceRepository.Delete(id);
                return Ok("Запись удалена");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
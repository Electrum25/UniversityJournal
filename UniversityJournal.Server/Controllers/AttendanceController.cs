using Microsoft.AspNetCore.Authorization; 
using Microsoft.AspNetCore.Mvc;
using UniversityJournal.Core.Entities;
using UniversityJournal.Core.Repositories;
using UniversityJournal.Core.UseCases;
using OpenIddict.Validation.AspNetCore;

namespace UniversityJournal.Server.Controllers
{
    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceRepository _attendanceRepository;

        public AttendanceController(IAttendanceRepository attendanceRepository)
        {
            _attendanceRepository = attendanceRepository;
        }

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

        [HttpGet("subject/{subjectId}")]
        [Authorize(Roles = "Teacher,Admin")]
        public async Task<IActionResult> GetBySubject(Guid subjectId)
        {
            var list = await _attendanceRepository.GetBySubject(subjectId);
            return Ok(list ?? new List<Attendance>());
        }

        [HttpGet("student/{studentId}")]
        [Authorize(Roles = "Student,Teacher,Admin")]
        public async Task<IActionResult> GetByStudent(Guid studentId)
        {
            var list = await _attendanceRepository.GetByStudent(studentId);
            return Ok(list ?? new List<Attendance>());
        }

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
        [HttpPost("batch")]
        [Authorize(Roles = "Teacher,Admin")]
        public async Task<IActionResult> BatchMarkAttendance(
    [FromBody] List<CreateAttendanceUseCase.CreateAttendanceRequest> requests,
    [FromServices] CreateAttendanceUseCase createUseCase,
    [FromHeader] Guid teacherId)
        {
            try
            {
                foreach (var request in requests)
                {
                    await createUseCase.Handle(request, teacherId);
                }
                return Ok($"Сохранено {requests.Count} записей о посещаемости");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
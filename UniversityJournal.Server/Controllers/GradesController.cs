using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Validation.AspNetCore;
using UniversityJournal.Core.DTOs;
using UniversityJournal.Core.Entities;
using UniversityJournal.Core.Repositories;
using UniversityJournal.Core.UseCases;

namespace UniversityJournal.Server.Controllers
{
    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class GradesController : ControllerBase
    {
        private readonly IGradeRepository _gradeRepository;

        public GradesController(IGradeRepository gradeRepository)
        {
            _gradeRepository = gradeRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateGradeUseCase.CreateGradeRequest request,
            [FromServices] CreateGradeUseCase createUseCase,
            [FromHeader] Guid teacherId) 
        {
            try
            {
                var id = await createUseCase.Handle(request, teacherId);
                return Ok(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("subject/{subjectId}")]
        public async Task<IActionResult> GetBySubject(Guid subjectId)
        {
            var grades = await _gradeRepository.GetBySubject(subjectId);
            return Ok(grades ?? new List<Grade>());
        }

        [HttpGet("student-report/{studentId}")]
        public async Task<IActionResult> GetStudentReport(
            Guid studentId,
            [FromServices] GetStudentDataUseCase getStudentDataUseCase)
        {
            try
            {
                var data = await getStudentDataUseCase.Handle(studentId);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _gradeRepository.Delete(id);
                return Ok("Оценка удалена");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateGradeRequest request)
        {
            try
            {
                await _gradeRepository.Update(request);
                return Ok("Оценка обновлена");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
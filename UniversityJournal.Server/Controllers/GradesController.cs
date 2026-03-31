using Microsoft.AspNetCore.Mvc;
using UniversityJournal.Core.Entities;
using UniversityJournal.Core.UseCases;
using UniversityJournal.Core.Repositories;
using UniversityJournal.Core.DTOs;

namespace UniversityJournal.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradesController : ControllerBase
    {
        private readonly IGradeRepository _gradeRepository;

        public GradesController(IGradeRepository gradeRepository)
        {
            _gradeRepository = gradeRepository;
        }

        /// <summary>
        /// Поставить оценку за работу (лабораторную)
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateGradeUseCase.CreateGradeRequest request,
            [FromServices] CreateGradeUseCase createUseCase,
            [FromHeader] Guid teacherId) // teacherId можно передавать в заголовке или брать из JWT
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

        /// <summary>
        /// Получить все оценки по конкретному предмету
        /// </summary>
        [HttpGet("subject/{subjectId}")]
        public async Task<IActionResult> GetBySubject(Guid subjectId)
        {
            var grades = await _gradeRepository.GetBySubject(subjectId);
            return Ok(grades ?? new List<Grade>());
        }

        /// <summary>
        /// Получить "карточку" студента: все его оценки, посещаемость и список предметов
        /// </summary>
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

        /// <summary>
        /// Обновить существующую оценку
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateGradeRequest request)
        {
            try
            {
                // Предполагаем, что у тебя есть метод в репозитории для обновления
                // Либо вызови соответствующий UseCase, если он есть в архитектуре
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
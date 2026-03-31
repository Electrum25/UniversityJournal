using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityJournal.Core.DTOs;
using UniversityJournal.Core.Entities;
using UniversityJournal.Core.Helpers;
using UniversityJournal.Core.UseCases.ScheduleUseCase;
using UniversityJournal.EfCore;

namespace UniversityJournal.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly UniversityJournalDbContext _context;
        private readonly GetScheduleByGroupUseCase _getScheduleUseCase;
        public ScheduleController(GetScheduleByGroupUseCase getScheduleUseCase, UniversityJournalDbContext context)
        {
            _getScheduleUseCase = getScheduleUseCase;
            _context = context;
        }

        /// <summary>
        /// Добавить пару в расписание с проверкой часов
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateScheduleRequest request)
        {
            // 1. Ищем предмет, чтобы узнать лимит часов
            var subject = await _context.Subjects.FindAsync(request.SubjectId);
            if (subject == null) return NotFound("Предмет не найден");

            // 2. Считаем, сколько ПАР в неделю разрешено (Всего часов / 2 часа на пару / 18 недель)
            // Пример: 72 часа / 36 = 2 пары в неделю.
            double maxPairsPerWeek = (double)subject.TotalHours / 36;

            // 3. Считаем, сколько пар этого предмета УЖЕ стоит в расписании для этой группы
            var existingPairsCount = await _context.ScheduleItems
                .CountAsync(s => s.SubjectId == request.SubjectId && s.GroupId == request.GroupId);

            if (existingPairsCount >= maxPairsPerWeek)
            {
                return BadRequest($"Лимит нагрузки исчерпан. Максимум пар в неделю: {Math.Floor(maxPairsPerWeek)}");
            }

            // 4. Проверяем, не занята ли эта пара в этот день у группы
            var isBusy = await _context.ScheduleItems.AnyAsync(s =>
                s.GroupId == request.GroupId &&
                s.DayOfWeek == request.DayOfWeek &&
                s.PairNumber == request.PairNumber);

            if (isBusy) return BadRequest("У этой группы в это время уже есть занятие");

            // 5. Сохраняем
            var newItem = new ScheduleItem
            {
                ScheduleItemId = Guid.NewGuid(),
                SubjectId = request.SubjectId,
                GroupId = request.GroupId,
                TeacherId = request.TeacherId,
                DayOfWeek = request.DayOfWeek,
                PairNumber = request.PairNumber
            };

            _context.ScheduleItems.Add(newItem);
            await _context.SaveChangesAsync();

            return Ok(newItem.ScheduleItemId);
        }

        /// <summary>
        /// Получить расписание для Группы (Студенту)
        /// </summary>
        [HttpGet("group/{groupId}")]
        public async Task<IActionResult> GetByGroup(Guid groupId)
        {
            var result = await _getScheduleUseCase.ExecuteAsync(groupId);
            return Ok(result);
        }

        [HttpGet("form-data")]
        public async Task<IActionResult> GetFormData()
        {
            try
            {
                var groups = await _context.Groups.ToListAsync();
                // Просто убираем .Include(s => s.TeacherId)
                var subjects = await _context.Subjects.ToListAsync();
                var teachers = await _context.Teachers.ToListAsync();

                return Ok(new { groups, subjects, teachers });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
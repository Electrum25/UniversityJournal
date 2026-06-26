using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenIddict.Validation.AspNetCore;
using UniversityJournal.Core.DTOs;
using UniversityJournal.Core.Entities;
using UniversityJournal.Core.Helpers;
using UniversityJournal.Core.UseCases.ScheduleUseCase;
using UniversityJournal.EfCore;

namespace UniversityJournal.Server.Controllers
{
    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ScheduleItem model)
        {
            var scheduleItem = new ScheduleItem
            {
                ScheduleItemId = Guid.NewGuid(),
                SubjectId = model.SubjectId,
                GroupId = model.GroupId,
                TeacherId = model.TeacherId,
                Date = DateTime.SpecifyKind(model.Date, DateTimeKind.Utc),
                PairNumber = model.PairNumber
            };

            var subject = await _context.Subjects.FindAsync(scheduleItem.SubjectId);
            if (subject == null) return NotFound("Предмет не найден");

            var totalAssigned = await _context.ScheduleItems
                .CountAsync(s => s.SubjectId == scheduleItem.SubjectId && s.GroupId == scheduleItem.GroupId);

            double maxPairsTotal = (double)subject.TotalHours / 2;

            if (totalAssigned >= maxPairsTotal)
            {
                return BadRequest($"Лимит часов по предмету исчерпан (Макс: {maxPairsTotal} пар)");
            }

            var isBusy = await _context.ScheduleItems.AnyAsync(s =>
                s.GroupId == scheduleItem.GroupId &&
                s.Date.Date == scheduleItem.Date.Date &&
                s.PairNumber == scheduleItem.PairNumber);

            if (isBusy) return BadRequest("У этой группы в это время уже есть занятие");

            _context.ScheduleItems.Add(scheduleItem);
            await _context.SaveChangesAsync();

            return Ok(scheduleItem.ScheduleItemId);
        }

        [HttpGet("group/{groupId}")]
        public async Task<IActionResult> GetByGroup(Guid groupId, [FromQuery] DateTime start, [FromQuery] DateTime end)
        {
            if (start == default) start = DateTime.UtcNow.Date;
            if (end == default) end = start.AddDays(7);

            var utcStart = DateTime.SpecifyKind(start, DateTimeKind.Utc);
            var utcEnd = DateTime.SpecifyKind(end, DateTimeKind.Utc);

            var result = await _getScheduleUseCase.ExecuteAsync(groupId, utcStart, utcEnd);
            return Ok(result);
        }

        [HttpGet("form-data")]
        public async Task<IActionResult> GetFormData()
        {
            var groups = await _context.Groups.ToListAsync();
            var subjects = await _context.Subjects.ToListAsync();
            var teachers = await _context.Teachers.ToListAsync();
            return Ok(new { groups, subjects, teachers });
        }
        [HttpGet("teacher/{teacherId}")]
        public async Task<IActionResult> GetByTeacher(
    Guid teacherId,
    [FromQuery] DateTime start,
    [FromQuery] DateTime end,
    [FromServices] GetScheduleByTeacherUseCase getScheduleByTeacherUseCase)
        {
            if (start == default) start = DateTime.UtcNow.Date;
            if (end == default) end = start.AddDays(7);

            var utcStart = DateTime.SpecifyKind(start, DateTimeKind.Utc);
            var utcEnd = DateTime.SpecifyKind(end, DateTimeKind.Utc);

            var result = await getScheduleByTeacherUseCase.ExecuteAsync(teacherId, utcStart, utcEnd);
            return Ok(result);
        }
    }
}

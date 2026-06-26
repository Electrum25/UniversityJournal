using UniversityJournal.Core.DTOs;
using UniversityJournal.Core.Helpers;
using UniversityJournal.Core.Storage.Repositories;

namespace UniversityJournal.Core.UseCases.ScheduleUseCase
{
    public class GetScheduleByTeacherUseCase
    {
        private readonly IScheduleRepository _repository;

        public GetScheduleByTeacherUseCase(IScheduleRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ScheduleItemDTO>> ExecuteAsync(Guid teacherId, DateTime startDate, DateTime endDate)
        {
            var items = await _repository.GetByTeacherDateRangeAsync(teacherId, startDate, endDate);

            return items.Select(s => new ScheduleItemDTO
            {
                ScheduleItemId = s.ScheduleItemId,
                SubjectId = s.SubjectId,
                SubjectName = s.Subject?.SubjectName ?? "Без названия",
                GroupId = s.GroupId,
                TeacherFullName = s.Teacher != null
                    ? $"{s.Teacher.LastName} {s.Teacher.FirstName[0]}."
                    : "Не назначен",
                Date = s.Date,
                PairNumber = s.PairNumber,
                TimeRange = ScheduleFormatter.GetPairTime(s.PairNumber)
            }).OrderBy(s => s.Date).ThenBy(s => s.PairNumber).ToList();
        }
    }
}
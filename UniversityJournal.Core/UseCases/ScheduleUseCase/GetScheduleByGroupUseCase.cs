using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityJournal.Core.DTOs;
using UniversityJournal.Core.Helpers;
using UniversityJournal.Core.Storage.Repositories;

namespace UniversityJournal.Core.UseCases.ScheduleUseCase
{
    public class GetScheduleByGroupUseCase
    {
        private readonly IScheduleRepository _repository;
        public GetScheduleByGroupUseCase(IScheduleRepository repository) => _repository = repository;

        public async Task<List<ScheduleItemDTO>> ExecuteAsync(Guid groupId)
        {
            var items = await _repository.GetByGroupIdAsync(groupId);

            return items.Select(s => new ScheduleItemDTO
            {
                ScheduleItemId = s.ScheduleItemId,
                SubjectId = s.SubjectId,
                SubjectName = s.Subject?.SubjectName ?? "Без названия",
                GroupId = s.GroupId,
                TeacherId = s.TeacherId,
                // Склеиваем ФИО из сущности Teacher
                TeacherFullName = s.Teacher != null
                    ? $"{s.Teacher.LastName} {s.Teacher.FirstName} {s.Teacher.Patronymic}".Trim()
                    : "Преподаватель не назначен",
                DayOfWeek = s.DayOfWeek,
                PairNumber = s.PairNumber,
                TimeRange = ScheduleFormatter.GetPairTime(s.PairNumber)
            }).ToList();
        }
    }
}

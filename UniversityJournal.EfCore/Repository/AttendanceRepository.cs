using Microsoft.EntityFrameworkCore;
using UniversityJournal.Core.Entities;
using UniversityJournal.Core.Repositories;
using UniversityJournal.EfCore;

namespace UniversityJournal.Storage.EfCore.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly UniversityJournalDbContext _context;

        public AttendanceRepository(UniversityJournalDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Guid> Create(Attendance attendance)
        {
            if (attendance == null) throw new ArgumentNullException(nameof(attendance));
            _context.Add(attendance);
            await _context.SaveChangesAsync();
            return attendance.AttendanceId;
        }

        public async Task Delete(Guid attendanceId)
        {
            var attendance = await _context.Attendances.FindAsync(attendanceId);
            if (attendance != null)
            {
                _context.Attendances.Remove(attendance);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("Attendance not found.", nameof(attendanceId));
            }
        }

        public async Task<Attendance?> Get(Guid attendanceId)
        {
            return await _context.Attendances.FindAsync(attendanceId);
        }

        public async Task<List<Attendance>?> GetByStudent(Guid studentId)
        {
            return await _context.Attendances
                .Where(a => a.StudentId == studentId)
                .ToListAsync();
        }

        public async Task<List<Attendance>?> GetBySubject(Guid subjectId)
        {
            return await _context.Attendances
                .Where(a => a.SubjectId == subjectId)
                .ToListAsync();
        }

        public async Task<bool> Update(Attendance attendance)
        {
            if (attendance == null) throw new ArgumentNullException(nameof(attendance));
            _context.Entry(attendance).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<Attendance>?> GetAll()  
        {
            return await _context.Attendances.ToListAsync();
        }
    }
}
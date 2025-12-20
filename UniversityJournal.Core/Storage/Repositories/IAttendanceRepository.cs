using UniversityJournal.Core.Entities;

namespace UniversityJournal.Core.Repositories
{
    public interface IAttendanceRepository
    {
        public Task<Guid> Create(Attendance attendance);
        public Task<bool> Update(Attendance attendance);
        public Task Delete(Guid attendanceId);
        public Task<Attendance?> Get(Guid attendanceId);
        public Task<List<Attendance>?> GetByStudent(Guid studentId);
        public Task<List<Attendance>?> GetBySubject(Guid subjectId);
        public Task<List<Attendance>?> GetAll(); 
    }
}
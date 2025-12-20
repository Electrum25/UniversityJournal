using UniversityJournal.Core.Entities;

namespace UniversityJournal.Core.Repositories
{
    public interface IGradeRepository
    {
        public Task<Guid> Create(Grade grade);
        public Task<bool> Update(Grade grade);
        public Task Delete(Guid gradeId);
        public Task<Grade?> Get(Guid gradeId);
        public Task<List<Grade>?> GetByStudent(Guid studentId);
        public Task<List<Grade>?> GetBySubject(Guid subjectId);
        public Task<List<Grade>?> GetAll();  
    }
}
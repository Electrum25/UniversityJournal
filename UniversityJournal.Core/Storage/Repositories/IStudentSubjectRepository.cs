using UniversityJournal.Core.Entities;

namespace UniversityJournal.Core.Repositories
{
    public interface IStudentSubjectRepository
    {
        public Task Create(StudentSubject studentSubject);
        public Task<bool> Update(StudentSubject studentSubject);
        public Task Delete(Guid studentId, Guid subjectId);
        public Task<StudentSubject?> Get(Guid studentId, Guid subjectId);
        public Task<List<StudentSubject>?> GetByStudent(Guid studentId);
        public Task<List<StudentSubject>?> GetBySubject(Guid subjectId);
        public Task<List<StudentSubject>?> GetAll();  
    }
}

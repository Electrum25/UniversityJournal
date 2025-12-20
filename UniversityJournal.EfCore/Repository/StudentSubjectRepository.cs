using Microsoft.EntityFrameworkCore;
using UniversityJournal.Core.Entities;
using UniversityJournal.Core.Repositories;
using UniversityJournal.EfCore;

namespace UniversityJournal.Storage.EfCore.Repositories
{
    public class StudentSubjectRepository : IStudentSubjectRepository
    {
        private readonly UniversityJournalDbContext _context;

        public StudentSubjectRepository(UniversityJournalDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task Create(StudentSubject studentSubject)
        {
            if (studentSubject == null) throw new ArgumentNullException(nameof(studentSubject));
            _context.Add(studentSubject);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid studentId, Guid subjectId)
        {
            var studentSubject = await _context.StudentSubjects.FindAsync(studentId, subjectId);
            if (studentSubject != null)
            {
                _context.StudentSubjects.Remove(studentSubject);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("StudentSubject not found.");
            }
        }

        public async Task<StudentSubject?> Get(Guid studentId, Guid subjectId)
        {
            return await _context.StudentSubjects.FindAsync(studentId, subjectId);
        }

        public async Task<List<StudentSubject>?> GetByStudent(Guid studentId)
        {
            return await _context.StudentSubjects
                .Where(ss => ss.StudentId == studentId)
                .ToListAsync();
        }

        public async Task<List<StudentSubject>?> GetBySubject(Guid subjectId)
        {
            return await _context.StudentSubjects
                .Where(ss => ss.SubjectId == subjectId)
                .ToListAsync();
        }

        public async Task<bool> Update(StudentSubject studentSubject)
        {
            if (studentSubject == null) throw new ArgumentNullException(nameof(studentSubject));
            _context.Entry(studentSubject).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<StudentSubject>?> GetAll()  
        {
            return await _context.StudentSubjects.ToListAsync();
        }
    }
}

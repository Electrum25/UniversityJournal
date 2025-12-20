using Microsoft.EntityFrameworkCore;
using UniversityJournal.Core.Entities;
using UniversityJournal.Core.Repositories;
using UniversityJournal.EfCore;

namespace UniversityJournal.Storage.EfCore.Repositories
{
    public class GradeRepository : IGradeRepository
    {
        private readonly UniversityJournalDbContext _context;

        public GradeRepository(UniversityJournalDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Guid> Create(Grade grade)
        {
            if (grade == null) throw new ArgumentNullException(nameof(grade));
            _context.Add(grade);
            await _context.SaveChangesAsync();
            return grade.GradeId;
        }

        public async Task Delete(Guid gradeId)
        {
            var grade = await _context.Grades.FindAsync(gradeId);
            if (grade != null)
            {
                _context.Grades.Remove(grade);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("Grade not found.", nameof(gradeId));
            }
        }

        public async Task<Grade?> Get(Guid gradeId)
        {
            return await _context.Grades.FindAsync(gradeId);
        }

        public async Task<List<Grade>?> GetByStudent(Guid studentId)
        {
            return await _context.Grades
                .Where(g => g.StudentId == studentId)
                .ToListAsync();
        }

        public async Task<List<Grade>?> GetBySubject(Guid subjectId)
        {
            return await _context.Grades
                .Where(g => g.SubjectId == subjectId)
                .ToListAsync();
        }

        public async Task<bool> Update(Grade grade)
        {
            if (grade == null) throw new ArgumentNullException(nameof(grade));
            _context.Entry(grade).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<Grade>?> GetAll() 
        {
            return await _context.Grades.ToListAsync();
        }
    }
}
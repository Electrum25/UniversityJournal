using Microsoft.EntityFrameworkCore;
using UniversityJournal.Core.Entities;
using UniversityJournal.Core.Repositories;
using UniversityJournal.EfCore;

namespace UniversityJournal.Storage.EfCore.Repositories
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly UniversityJournalDbContext _context;

        public SubjectRepository(UniversityJournalDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Guid> Create(Subject subject)
        {
            if (subject == null) throw new ArgumentNullException(nameof(subject));
            _context.Add(subject);
            await _context.SaveChangesAsync();
            return subject.SubjectId;
        }

        public async Task Delete(Guid subjectId)
        {
            var subject = await _context.Subjects.FindAsync(subjectId);
            if (subject != null)
            {
                _context.Subjects.Remove(subject);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("Subject not found.", nameof(subjectId));
            }
        }

        public async Task<Subject?> Get(Guid subjectId)
        {
            return await _context.Subjects.FindAsync(subjectId);
        }

        public async Task<List<Subject>?> GetByTeacher(Guid teacherId)
        {
            return await _context.Subjects
                .Where(s => s.TeacherId == teacherId)
                .ToListAsync();
        }

        public async Task<List<Subject>?> GetAll()
        {
            return await _context.Subjects.ToListAsync();
        }

        public async Task<bool> Update(Subject subject)
        {
            if (subject == null) throw new ArgumentNullException(nameof(subject));
            _context.Entry(subject).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
using Microsoft.EntityFrameworkCore;
using UniversityJournal.Core.Entities;
using UniversityJournal.Core.Repositories;
using UniversityJournal.EfCore;

namespace UniversityJournal.Storage.EfCore.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly UniversityJournalDbContext _context;

        public TeacherRepository(UniversityJournalDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Guid> Create(Teacher teacher)
        {
            if (teacher == null) throw new ArgumentNullException(nameof(teacher));
            _context.Add(teacher);
            await _context.SaveChangesAsync();
            return teacher.TeacherId;
        }

        public async Task Delete(Guid teacherId)
        {
            var teacher = await _context.Teachers.FindAsync(teacherId);
            if (teacher != null)
            {
                _context.Teachers.Remove(teacher);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("Teacher not found.", nameof(teacherId));
            }
        }

        public async Task<Teacher?> Get(Guid teacherId)
        {
            return await _context.Teachers.FindAsync(teacherId);
        }

        public async Task<List<Teacher>?> GetAll()
        {
            return await _context.Teachers.ToListAsync();
        }

        public async Task<bool> Update(Teacher teacher)
        {
            if (teacher == null) throw new ArgumentNullException(nameof(teacher));
            _context.Entry(teacher).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using UniversityJournal.Core.Entities;
using UniversityJournal.Core.Repositories;
using UniversityJournal.EfCore;

namespace UniversityJournal.Storage.EfCore.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly UniversityJournalDbContext _context;

        public StudentRepository(UniversityJournalDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Guid> Create(Student student)
        {
            if (student == null) throw new ArgumentNullException(nameof(student));
            _context.Add(student);
            await _context.SaveChangesAsync();
            return student.StudentId;
        }

        public async Task Delete(Guid studentId)
        {
            var student = await _context.Students.FindAsync(studentId);
            if (student != null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("Student not found.", nameof(studentId));
            }
        }

        public async Task<Student?> Get(Guid studentId)
        {
            return await _context.Students.FindAsync(studentId);
        }

        public async Task<List<Student>?> GetAll()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<List<Student>?> GetByGroup(Guid groupId)
        {
            return await _context.Students
                .Where(s => s.GroupId == groupId)
                .ToListAsync();
        }

        public async Task<bool> Update(Student student)
        {
            if (student == null) throw new ArgumentNullException(nameof(student));
            _context.Entry(student).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityJournal.Core.Entities;
using UniversityJournal.Core.Repositories;
using UniversityJournal.EfCore;

namespace UniversityJournal.Storage.EfCore.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UniversityJournalDbContext _context;

        public UserRepository(UniversityJournalDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context), "DbContext cannot be null.");
        }

        public async Task<Guid> Create(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null.");
            }

            _context.Add(user);
            await _context.SaveChangesAsync();
            return user.UserId;
        }

        public async Task Delete(Guid userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("User not found.", nameof(userId));
            }
        }

        public async Task<User?> Get(Guid userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task<User?> GetByLogin(string login)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Login == login);
        }

        public async Task<List<User>?> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<bool> Update(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            _context.Entry(user).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<User?> Authenticate(string login, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Login == login);
            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                return user;
            }
            return null;
        }
    }
}

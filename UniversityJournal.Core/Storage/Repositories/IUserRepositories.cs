using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityJournal.Core.Entities;

namespace UniversityJournal.Core.Repositories
{
    public interface IUserRepository
    {
        public Task<Guid> Create(User user);
        public Task<bool> Update(User user);
        public Task Delete(Guid userId);
        public Task<User?> Get(Guid userId);
        public Task<User?> GetByLogin(string login);
        public Task<List<User>?> GetAll();
        public Task<User?> Authenticate(string login, string password);
    }
}

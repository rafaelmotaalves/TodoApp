using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.User
{
    public interface IUserRepository
    {
        public Task<List<User>> GetAll();

        public Task<User?> Get(string id);
    }
}
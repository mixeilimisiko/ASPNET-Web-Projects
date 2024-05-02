using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using ToDo.App.Users;
using ToDo.Domain;
using ToDo.Persistence.Context;

namespace ToDo.Infrastructure.Users
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ToDoContext context) : base(context)
        {
        }

        public async Task<User> ReadAsync(int id, CancellationToken token)
        {
            object?[] keyValues = new object?[] { id };
            return await base.ReadAsync(keyValues, token);
        }
        public async Task<User> ReadByUsernameAsync(string username, CancellationToken token)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Username == username, token);
        }
        public async Task DeleteAsync(int id, CancellationToken token)
        {
            object?[] keyValues = new object?[] { id };
            await base.DeleteAsync(keyValues, token);
        }



    }
}

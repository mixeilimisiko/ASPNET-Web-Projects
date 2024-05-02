using ToDo.Domain;

namespace ToDo.App.Users
{
    public interface IUserRepository
    {

        public Task<IEnumerable<User>> ReadAllAsync(CancellationToken token);
        public Task<User> ReadAsync(int id, CancellationToken token);
        /* Method: ReadByUsernameAsync
         * Description: Tries to retrieve user with corresponding username 
         * returns null if does not find such user
        */
        public Task<User> ReadByUsernameAsync(string username, CancellationToken token);
        public Task CreateAsync(User user, CancellationToken token);
        public Task UpdateAsync(User user, CancellationToken token);
        public Task DeleteAsync(int id, CancellationToken token);
        public Task DeleteAsync(User user, CancellationToken token);

    }
}

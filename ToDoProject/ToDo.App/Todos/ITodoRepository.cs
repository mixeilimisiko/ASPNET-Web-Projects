using ToDo.Domain;
using ToDo.Domain.Enums;

namespace ToDo.App.Todos
{
    public interface ITodoRepository
    {
        public Task<IEnumerable<Todo>> ReadAllAsync(CancellationToken token);
        public Task<Todo?> ReadAsync(int id, CancellationToken token);

        public Task<Todo> ReadUserTodoAsync(int userId, string title, CancellationToken token);
        /* Method: ReadUserTodosAsync
         * Description: Tries to retrieve all Todos associated with concrete user 
         * returns null if does not find such user
        */
        public Task<IEnumerable<Todo>> ReadUserTodosAsync(int userId, Statuses status, CancellationToken token);
        public Task<IEnumerable<Todo>> ReadUserTodosAsync(int userId, CancellationToken token);
        public Task CreateAsync(Todo todo, CancellationToken token);
        public Task UpdateAsync(Todo todo, CancellationToken token);
        public Task DeleteAsync(int id, CancellationToken token);
        public Task DeleteAsync(Todo todo, CancellationToken token);

        public Task SoftDeleteAsync(int id, CancellationToken token);
    }
}

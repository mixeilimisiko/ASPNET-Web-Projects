using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using ToDo.App.Exceptions;
using ToDo.App.Todos;
using ToDo.Domain;
using ToDo.Domain.Enums;
using ToDo.Persistence.Context;


namespace ToDo.Infrastructure.Todos
{
    public class TodoRepository : BaseRepository<Todo>, ITodoRepository
    {
        public TodoRepository(ToDoContext context) : base(context)
        {
        }

        public override async Task CreateAsync(Todo todo, CancellationToken token)
        {
            await base.CreateAsync(todo, token);
        }

        public async Task DeleteAsync(int id, CancellationToken token)
        {
            object?[] keyValues = new object?[] { id };
            await base.DeleteAsync(keyValues, token);
        }

        //public async Task<Todo> ReadAsync(int id, CancellationToken token)
        //{
        //    object?[] keyValues = new object?[] { id };
        //    return  await base.ReadAsync(keyValues, token);

        //}

        public async Task<Todo?> ReadAsync(int id, CancellationToken token)
        {
            return await _dbSet.Include(todo => todo.Subtasks) 
                               .FirstOrDefaultAsync(todo => todo.Id == id, token);
        }




        public async Task<IEnumerable<Todo>> ReadUserTodosAsync(int userId, Statuses status, CancellationToken token)
        {
            return await _dbSet
                .Where(t => t.UserId == userId && t.Status == status)
                .Include(t => t.Subtasks)
                .ToListAsync(token);
        }

        public async Task<IEnumerable<Todo>> ReadUserTodosAsync(int userId, CancellationToken token)
        {
            return await _dbSet
                .Where(t => t.UserId == userId) //  && t.Status != Statuses.Deleted          take responsibility to this check to Service layer
                .Include(t => t.Subtasks)
                .ToListAsync(token);
        }


        public async Task<Todo?> ReadUserTodoAsync(int userId, string title, CancellationToken token)
        {
            return await _dbSet
                .Where(t => t.UserId == userId && t.Title == title)
                .Include(t => t.Subtasks) 
                .FirstOrDefaultAsync(token);
        }

        public async Task SoftDeleteAsync(int id, CancellationToken token)
        {
            var todo = await _dbSet.Include(t => t.Subtasks).FirstOrDefaultAsync(t => t.Id == id, token);

            todo.Status = Statuses.Deleted;
            _dbSet.Update(todo);

            foreach (var subtask in todo.Subtasks)
            {
                subtask.Status = Statuses.Deleted;
                _context.Entry(subtask).State = EntityState.Modified; 
            }

            await _context.SaveChangesAsync(token);
        }

    }
}

using Mapster;
using ToDo.App.Exceptions;
using ToDo.App.Todos.Requests;
using ToDo.App.Todos.Responses;
using ToDo.Domain;
using ToDo.Domain.Enums;

namespace ToDo.App.Todos
{
    public class TodoService : ITodoService
    {

        private readonly ITodoRepository _repository;

        public TodoService(ITodoRepository repository)
        {
            _repository = repository;
        }

        public async Task CreateTodoAsync(TodoCreateRequestModel todoRequest, int userId, CancellationToken token)
        {
            var existingTodo = await _repository.ReadUserTodoAsync(userId, todoRequest.Title, token);
            if (existingTodo != null && existingTodo.Status != Statuses.Deleted)
            {
                throw new AlreadyExistsError("A todo with the same title already exists for the user");
            }

            Todo todo = todoRequest.Adapt<Todo>();
            todo.UserId = userId;

            await _repository.CreateAsync(todo, token);
        }


        public async Task<TodoResponseModel> ReadUserTodoAsync(int id, int userId, CancellationToken token)
        {
            var todo = await RetrieveTodoAndValidateOwnership(id, userId, token);

            return todo.Adapt<TodoResponseModel>();
        }

        public async Task<IEnumerable<TodoResponseModel>> ReadUserTodosAsync(int userId, Statuses? status, CancellationToken token)
        {
            IEnumerable<Todo> todos;
            if (status.HasValue)
            {
                todos = await _repository.ReadUserTodosAsync(userId, status.Value, token);
            }
            else
            {
                todos = await _repository.ReadUserTodosAsync(userId, token);
                todos = todos.Where(todo => todo.Status != Statuses.Deleted);
            }

            return todos.Adapt<IEnumerable<TodoResponseModel>>();
        }

        public async Task UpdateTodoAsync(int id, int userId, TodoUpdateRequestModel todoUpdateModel, CancellationToken token)
        {
            var todo = await RetrieveTodoAndValidateOwnership(id, userId, token);

            todo.ModifiedAt = DateTime.UtcNow;
            todo.Title = todoUpdateModel.Title;
            todo.TargetDate = todoUpdateModel.TargetDate;
            todo.Id = id;

            await _repository.UpdateAsync(todo, token);
        }


        public async Task MarkTodoAsDoneAsync(int id, int userId, CancellationToken token)
        {
            var todo = await RetrieveTodoAndValidateOwnership(id, userId, token);

            todo.ModifiedAt = DateTime.UtcNow;
            todo.Status = Statuses.Done;
            todo.Id = id;

            await _repository.UpdateAsync(todo, token);
        }

        public async Task SoftDeleteTodoAsync(int id, int userId, CancellationToken token)
        {
            var todo = await RetrieveTodoAndValidateOwnership(id, userId, token);
            todo.ModifiedAt = DateTime.UtcNow;
            todo.Status = Statuses.Done;
            todo.Id = id;

            await _repository.SoftDeleteAsync(id, token);
        }

        public async Task DeleteTodoAsync(int id, int userId, CancellationToken token)
        {
            var todo = await RetrieveTodoAndValidateOwnership(id, userId, token);

            await _repository.DeleteAsync(id, token);
        }

        private async Task<Todo> RetrieveTodoAndValidateOwnership(int id, int userId, CancellationToken token)
        {
            var todo = await _repository.ReadAsync(id, token);

            if (todo == null || todo.Status == Statuses.Deleted)
            {
                throw new NotFoundError($"Todo with id {id} not found");
            }

            if (todo.UserId != userId)
            {
                throw new OwnershipError($"User {userId} is not authorized to access todo {id}");
            }

            return todo;
        }


    }
}

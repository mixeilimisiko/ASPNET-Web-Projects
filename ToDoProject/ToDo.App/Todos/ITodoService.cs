using ToDo.App.Todos.Requests;
using ToDo.App.Todos.Responses;
using ToDo.Domain.Enums;

namespace ToDo.App.Todos
{
    public interface ITodoService
    {
        Task CreateTodoAsync(TodoCreateRequestModel todoRequest, int userId, CancellationToken token);
        Task<TodoResponseModel> ReadUserTodoAsync(int id, int userId, CancellationToken cancellationToken);
        Task<IEnumerable<TodoResponseModel>> ReadUserTodosAsync(int userId, Statuses? status, CancellationToken token);
        Task UpdateTodoAsync(int id, int userId, TodoUpdateRequestModel todoUpdateModel, CancellationToken token);
        Task MarkTodoAsDoneAsync(int id, int userId, CancellationToken token);
        Task DeleteTodoAsync(int id, int userId, CancellationToken token);
        Task SoftDeleteTodoAsync(int id, int userId, CancellationToken token);
    }
}

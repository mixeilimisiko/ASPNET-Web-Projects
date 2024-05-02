using Mapster;
using ToDo.App.Exceptions;
using ToDo.App.Subtasks.Requests;
using ToDo.App.Subtasks.Responses;
using ToDo.App.Todos;
using ToDo.Domain;
using ToDo.Domain.Enums;

namespace ToDo.App.Subtasks
{
    public class SubtaskService : ISubtaskService
    {
        private readonly ISubtaskRepository _repository;
        private readonly ITodoService _todoService;

        public SubtaskService(ISubtaskRepository repository, ITodoService todoService)
        {
            _repository = repository;
            _todoService = todoService;
        }

        public async Task CreateSubtaskAsync(SubtaskRequestModel subtaskRequest, int userId, CancellationToken token)
        {
            var todo = await _todoService.ReadUserTodoAsync(subtaskRequest.TodoId, userId, token).ConfigureAwait(false);

            if(todo.Status == Statuses.Done) 
            {
                throw new ConflictError($"Todo with id '{todo.Id}' is already Done, you cannot add subtasks to it ");
            }

            var existingSubtask = todo.Subtasks.FirstOrDefault(s => s.Title == subtaskRequest.Title);
            if (existingSubtask != null)
            {
                throw new AlreadyExistsError($"Subtask with the same name '{subtaskRequest.Title}' already exists in the todo.");
            }

            Subtask subtask = subtaskRequest.Adapt<Subtask>();
            await _repository.CreateAsync(subtask, token).ConfigureAwait(false);

        }

        public async Task DeleteSubtaskAsync(int id, int userId, CancellationToken token)
        {
            Subtask subtask = await _repository.ReadAsync(id, token);
            try
            {
                var todo = await _todoService.ReadUserTodoAsync(subtask.ToDoId, userId, token).ConfigureAwait(false); // check for ownership
            }
            catch (OwnershipError ex)
            {
                throw new OwnershipError($"User {userId} is not authorized to access subtask {id} because its todo does not belong to user");
            }
            catch (NotFoundError)
            {
                throw new NotFoundError($"Subtask not found");
            }

            await _repository.DeleteAsync(subtask, token).ConfigureAwait(false); 
        }


        public async Task SoftDeleteSubtaskAsync(int id, int userId, CancellationToken token)
        {
            Subtask subtask = await _repository.ReadAsync(id, token).ConfigureAwait(false);
            try
            {
                var todo = await _todoService.ReadUserTodoAsync(subtask.ToDoId, userId, token).ConfigureAwait(false); // check for ownership
            }
            catch (OwnershipError ex)
            {
                throw new OwnershipError($"User {userId} is not authorized to access subtask {id} because its todo does not belong to user");
            }
            catch (NotFoundError)
            {
                throw new NotFoundError($"Subtask not found");
            }

            subtask.Status = Statuses.Deleted;
            subtask.ModifiedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(subtask, token).ConfigureAwait(false);
        }


        public async Task<SubtaskResponseModel> ReadSubtaskAsync(int id, int userId, CancellationToken token)
        {
            Subtask subtask = await _repository.ReadAsync(id, token).ConfigureAwait(false);
            try
            {
                var todo = await _todoService.ReadUserTodoAsync(subtask.ToDoId, userId, token).ConfigureAwait(false); // check for ownership
            }
            catch (OwnershipError)
            {
                throw new OwnershipError($"User {userId} is not authorized to access subtask {id} because its todo does not belong to user");
            }
            catch (NotFoundError)
            {
                throw new NotFoundError($"Subtask not found");
            }

            var result = subtask.Adapt<SubtaskResponseModel>();

            return result;
        }

        public async Task UpdateAsync(int id, int userId, SubtaskUpdateRequestModel subtaskRequest, CancellationToken token)
        {
            Subtask subtask = await _repository.ReadAsync(id, token).ConfigureAwait(false);
            try
            {
                var todo = await _todoService.ReadUserTodoAsync(subtask.ToDoId, userId, token); // check for ownership
            }
            catch (OwnershipError ex)
            {
                throw new OwnershipError($"User {userId} is not authorized to access subtask {id} because its todo does not belong to user");
            }
            catch (NotFoundError)
            {
                throw new NotFoundError($"Subtask not found");
            }

            subtask.ModifiedAt = DateTime.UtcNow;
            subtask.Status = subtaskRequest.Status;
            subtask.Title = subtaskRequest.Title;

            await _repository.UpdateAsync(subtask, token).ConfigureAwait(false);

        }


    } 
}

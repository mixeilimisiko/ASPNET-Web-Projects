using ToDo.App.Subtasks.Requests;
using ToDo.App.Subtasks.Responses;

namespace ToDo.App.Subtasks
{
    public interface ISubtaskService
    {
        Task CreateSubtaskAsync(SubtaskRequestModel subtaskRequest, int userId, CancellationToken token);
        Task DeleteSubtaskAsync(int id, int userId, CancellationToken token);

        Task SoftDeleteSubtaskAsync(int id, int userId, CancellationToken token);
        Task<SubtaskResponseModel> ReadSubtaskAsync(int id, int userId, CancellationToken token);
        Task UpdateAsync(int id, int userId, SubtaskUpdateRequestModel subtaskRequest, CancellationToken token);
    }
}

using ToDo.Domain;

namespace ToDo.App.Subtasks
{
    public interface ISubtaskRepository
    {
        public Task<IEnumerable<Subtask>> ReadAllAsync(CancellationToken token);
        public Task<Subtask> ReadAsync(int id, CancellationToken token);
        public Task CreateAsync(Subtask subtask, CancellationToken token);
        public Task UpdateAsync(Subtask subtask, CancellationToken token);
        public Task DeleteAsync(int id, CancellationToken token);
        public Task DeleteAsync(Subtask subtask, CancellationToken token);

    }
}

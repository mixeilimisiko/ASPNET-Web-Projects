using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.App.Subtasks;
using ToDo.Domain;
using ToDo.Persistence.Context;

namespace ToDo.Infrastructure.Subtasks
{
    public class SubtaskRepository : BaseRepository<Subtask>, ISubtaskRepository
    {
        public SubtaskRepository(ToDoContext context) : base(context)
        {
        }

        public async Task DeleteAsync(int id, CancellationToken token)
        {
            object?[] keyValues = new object?[] { id };
            await base.DeleteAsync(keyValues, token);
        }

        public async Task<Subtask> ReadAsync(int id, CancellationToken token)
        {
            object?[] keyValues = new object?[] { id };
            return await base.ReadAsync(keyValues, token);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using ToDo.Domain;

namespace ToDo.Persistence.Context
{
    public class ToDoContext : DbContext
    {
        public ToDoContext(DbContextOptions<ToDoContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Todo> ToDos { get; set; }
        public DbSet<Subtask> Subtasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ToDoContext).Assembly);
        }
    }
}

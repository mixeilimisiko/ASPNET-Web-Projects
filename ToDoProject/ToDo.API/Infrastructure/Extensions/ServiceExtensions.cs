using ToDo.App.Subtasks;
using ToDo.App.Todos;
using ToDo.App.Users;
using ToDo.Infrastructure;
using ToDo.Infrastructure.Subtasks;
using ToDo.Infrastructure.Todos;
using ToDo.Infrastructure.Users;

namespace ToDo.API.Infrastructure.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            //services.AddScoped(typeof(BaseRepository<>), typeof(BaseRepository<>));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<ITodoRepository, TodoRepository>();
            services.AddScoped<ITodoService, TodoService>();

            services.AddScoped<ISubtaskRepository, SubtaskRepository>();
            services.AddScoped<ISubtaskService, SubtaskService>();

        }
    }
}

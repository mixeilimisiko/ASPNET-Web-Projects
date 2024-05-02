using ToDo.API.Infrastructure.Middlewares;

namespace ToDo.API.Infrastructure.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder AddMiddlewares(this IApplicationBuilder app)
        {
            app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
            //app.UseMiddleware for requests and responses
            return app;
        }
    }
}

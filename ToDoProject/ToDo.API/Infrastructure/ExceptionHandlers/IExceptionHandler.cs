namespace ToDo.API.Infrastructure.ExceptionHandlers
{
    public interface IExceptionHandler
    {
        public IExceptionHandler SetNext(IExceptionHandler next);

        public Task HandleAsync(HttpContext contex, Exception exception);

        public Task HandleNext(HttpContext context, Exception exception);

    }
}

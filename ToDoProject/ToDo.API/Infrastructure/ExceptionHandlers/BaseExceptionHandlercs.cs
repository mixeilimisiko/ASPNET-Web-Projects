namespace ToDo.API.Infrastructure.ExceptionHandlers
{
    public abstract class BaseExceptionHandler : IExceptionHandler
    {
        protected IExceptionHandler _nextHandler;

        public IExceptionHandler SetNext(IExceptionHandler next)
        {
            _nextHandler = next;
            return _nextHandler;
        }

        public virtual async Task HandleAsync(HttpContext context, Exception exception)
        {
            using StreamWriter writer = new StreamWriter("logs.txt", true);
            await writer.WriteLineAsync($"{DateTime.Now} - {exception.Message}\n{exception.StackTrace}\n");
        }

        public virtual async Task HandleNext(HttpContext context, Exception exception)
        {

            if (_nextHandler != null)
            {
                await _nextHandler.HandleAsync(context, exception);
            }
            //else
            //{
            //    // If no next handler, you could log an unhandled exception, 
            //    // return a generic error response, or simply do nothing.
            //    // This behavior would be up to how you want to handle such cases.

            //    // Example of a generic response (customize as needed):
            //    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            //    await context.Response.WriteAsync("An unhandled exception occurred.");
            //}

        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ToDo.API.Infrastructure.ExceptionHandlers
{
    public class UnknownExceptionHandler : BaseExceptionHandler
    {
        public override async Task HandleAsync(HttpContext context, Exception exception)
        {
            await base.HandleAsync(context, exception);
            var problemDetails = new ProblemDetails
            {
                Title = "Internal Server Error",
                Detail = "An unexpected error occurred.",
                Status = StatusCodes.Status500InternalServerError,
                Instance = context.Request.Path,
            };

            // Optionally, set stack trace if available
            if (context.Response.StatusCode == StatusCodes.Status500InternalServerError)
            {
                problemDetails.Extensions["stackTrace"] = exception.StackTrace;
            }

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(problemDetails));

            await base.HandleNext(context, exception);
        }
    }
}

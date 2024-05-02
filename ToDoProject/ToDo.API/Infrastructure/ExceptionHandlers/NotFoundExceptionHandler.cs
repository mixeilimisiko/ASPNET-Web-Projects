using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ToDo.App.Exceptions;
using Newtonsoft.Json;

namespace ToDo.API.Infrastructure.ExceptionHandlers
{
    public class NotFoundExceptionHandler : BaseExceptionHandler
    {
        public override async Task HandleAsync(HttpContext context, Exception exception)
        {
  
            if (exception is NotFoundError notFoundError)
            {
                await base.HandleAsync(context, exception);
                var problemDetails = new ProblemDetails
                {
                  
                    Title = "Resource not found",
                    Detail = notFoundError.Message,
                    Status = StatusCodes.Status404NotFound,
                    Instance = context.Request.Path,
                };

                // Optionally, set stack trace if available
                if (context.Response.StatusCode == StatusCodes.Status500InternalServerError)
                {
                    problemDetails.Extensions["stackTrace"] = exception.StackTrace;
                }

                context.Response.StatusCode = StatusCodes.Status404NotFound;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(problemDetails));
            }
            
            else await base.HandleNext(context, exception);
        }
    }
}

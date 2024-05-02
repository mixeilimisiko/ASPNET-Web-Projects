using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ToDo.App.Exceptions;
using Newtonsoft.Json;

namespace ToDo.API.Infrastructure.ExceptionHandlers
{
    public class AlreadyExistsExceptionHandler : BaseExceptionHandler
    {
        public override async Task HandleAsync(HttpContext context, Exception exception)
        {
            

            if (exception is AlreadyExistsError alreadyExistsError)
            {
                await base.HandleAsync(context, exception);
                var problemDetails = new ProblemDetails
                {
                    Title = "Resource already exists",
                    Detail = alreadyExistsError.Message,
                    Status = StatusCodes.Status409Conflict,
                    Instance = context.Request.Path,
                };
                
                if (context.Response.StatusCode == StatusCodes.Status500InternalServerError)
                {
                    problemDetails.Extensions["stackTrace"] = exception.StackTrace;
                }

                context.Response.StatusCode = StatusCodes.Status409Conflict;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(problemDetails));
            }

            await base.HandleNext(context, exception);
        }
    }
}

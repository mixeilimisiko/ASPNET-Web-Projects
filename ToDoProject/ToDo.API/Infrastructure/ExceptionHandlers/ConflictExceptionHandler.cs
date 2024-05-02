using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ToDo.App.Exceptions;
using Newtonsoft.Json;

namespace ToDo.API.Infrastructure.ExceptionHandlers
{
    public class ConflictExceptionHandler : BaseExceptionHandler
    {
        public override async Task HandleAsync(HttpContext context, Exception exception)
        {
            
            if (exception is ConflictError conflictError)
            {
                await base.HandleAsync(context, exception);
                var problemDetails = new ProblemDetails
                {
                    Title = "Conflict",
                    Detail = conflictError.Message,
                    Status = StatusCodes.Status409Conflict,
                    Instance = context.Request.Path,
                };

                context.Response.StatusCode = StatusCodes.Status409Conflict;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(problemDetails));
            }

            else await base.HandleNext(context, exception);
        }
    }
}

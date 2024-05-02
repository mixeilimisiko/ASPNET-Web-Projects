using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ToDo.App.Exceptions;
using Newtonsoft.Json;

namespace ToDo.API.Infrastructure.ExceptionHandlers
{
    public class InvalidPasswordExceptionHandler : BaseExceptionHandler
    {
        public override async Task HandleAsync(HttpContext context, Exception exception)
        {
            if (exception is InvalidPasswordError invalidPasswordError)
            {
                await base.HandleAsync(context, exception);
                var problemDetails = new ProblemDetails
                {
                    Title = "Invalid Password",
                    Detail = invalidPasswordError.Message,
                    Status = StatusCodes.Status400BadRequest,
                    Instance = context.Request.Path,
                };

                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(problemDetails));
            }

            else await base.HandleNext(context, exception);
        }
    }
}

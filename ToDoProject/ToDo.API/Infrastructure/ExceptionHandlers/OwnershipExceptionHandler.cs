using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ToDo.App.Exceptions;
using Newtonsoft.Json;

namespace ToDo.API.Infrastructure.ExceptionHandlers
{
    public class OwnershipExceptionHandler : BaseExceptionHandler
    {
        public override async Task HandleAsync(HttpContext context, Exception exception)
        {
            if (exception is OwnershipError ownershipError)
            {
                await base.HandleAsync(context, exception);
                var problemDetails = new ProblemDetails
                {
                    Title = "Ownership Violation",
                    Detail = ownershipError.Message,
                    Status = StatusCodes.Status403Forbidden,
                    Instance = context.Request.Path,
                };

                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(problemDetails));
            }

            else await base.HandleNext(context, exception);
        }
    }
}

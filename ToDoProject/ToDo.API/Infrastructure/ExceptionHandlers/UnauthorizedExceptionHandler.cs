using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ToDo.App.Exceptions;
using Newtonsoft.Json;

namespace ToDo.API.Infrastructure.ExceptionHandlers
{
    public class UnauthorizedExceptionHandler : BaseExceptionHandler
    {
        public override async Task HandleAsync(HttpContext context, Exception exception)
        {
            if (exception is UnauthorizedError unauthorizedError)
            {
                await base.HandleAsync(context, exception);
                var problemDetails = new ProblemDetails
                {
                    Title = "Unauthorized Access",
                    Detail = unauthorizedError.Message,
                    Status = StatusCodes.Status401Unauthorized,
                    Instance = context.Request.Path,
                };

                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(problemDetails));
            }

            else await base.HandleNext(context, exception);
        }
    }
}

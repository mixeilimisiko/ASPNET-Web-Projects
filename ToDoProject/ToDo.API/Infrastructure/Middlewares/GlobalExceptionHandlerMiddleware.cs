using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ToDo.API.Infrastructure.ExceptionHandlers;

namespace ToDo.API.Infrastructure.Middlewares
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IExceptionHandler _exceptionHandlerChain;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
            _exceptionHandlerChain = ChainConfigurator.ConfigureExceptionChain();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                await _exceptionHandlerChain.HandleAsync(context, ex);
            }
        }

    }
}

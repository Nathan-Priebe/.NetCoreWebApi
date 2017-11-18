using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Logging;
using NetCoreWebAPI.Exceptions;

namespace NetCoreWebAPI.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class CustomExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionHandler> _logger;

        public CustomExceptionHandler(RequestDelegate next, ILogger<CustomExceptionHandler> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (NotFoundException ex)
            {
                //if (httpContext.Response.HasStarted)
                //{
                //    _logger.LogWarning(
                //        "The response has already started, the http status code middleware will not be executed");
                //}

                httpContext.Response.Clear();
                httpContext.Response.StatusCode = 400;

                await httpContext.Response.WriteAsync(ex.Message);
            }
            catch (Exception ex)
            {
                //if (httpContext.Response.HasStarted)
                //{
                //    _logger.LogWarning(
                //        "The response has already started, the http status code middleware will not be executed");
                //}

                httpContext.Response.Clear();
                httpContext.Response.StatusCode = 500;

                await httpContext.Response.WriteAsync(ex.Message);
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ExceptionHandlerExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandler>();
        }
    }
}

using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Runtime.Versioning;
namespace Lab3
{
    public class LogHTTPMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LogHTTPMiddleware> _logger;
        public LogHTTPMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory?.CreateLogger<LogHTTPMiddleware>() ??
            throw new ArgumentNullException(nameof(loggerFactory));
        }
        public async Task InvokeAsync(HttpContext context)
        {
            using (StreamWriter outputFile = new StreamWriter("log.txt", true))
            {
                outputFile.WriteLine(DateTime.Now.ToString() + " " + context.Connection.RemoteIpAddress?.ToString() + " " + context.Request.Path.ToString() + " " + context.Response.StatusCode.ToString());
            }
            await this._next(context);
        }
    }
    public static class LogHTTPMiddlewareExtensions
    {
        public static IApplicationBuilder UseLogHTTP(this IApplicationBuilder app)
        {
            return app.UseMiddleware<LogHTTPMiddleware>();
        }
    }
}

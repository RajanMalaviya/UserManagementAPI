using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Threading.Tasks;

namespace UserManagementAPI.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            // Log incoming request
            Console.WriteLine($"Incoming Request: {context.Request.Method} {context.Request.Path}");

            await _next(context);

            // Log outgoing response
            stopwatch.Stop();
            Console.WriteLine($"Outgoing Response: {context.Response.StatusCode} (Elapsed Time: {stopwatch.ElapsedMilliseconds} ms)");
        }
    }
}

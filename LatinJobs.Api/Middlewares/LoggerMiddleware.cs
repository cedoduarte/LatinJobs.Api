using System.Diagnostics;
using ILogger = Serilog.ILogger;

namespace LatinJobs.Api.Middlewares
{
    public class LoggerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public LoggerMiddleware(RequestDelegate next, ILogger logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            try
            {
                _logger.Information($"Handling request: {httpContext.Request.Method} {httpContext.Request.Path}");
                await _next(httpContext);
                _logger.Information($"Response sent: {httpContext.Response.StatusCode}");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "An error occurred while processing the request");
                throw;
            }
            finally
            {
                stopwatch.Stop();
                _logger.Information($"Request completed in {stopwatch.ElapsedMilliseconds} milliseconds");
            }
        }        
    }

    public static class LoggerMiddlewareExtensions
    {
        public static IApplicationBuilder UseLogger(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LoggerMiddleware>();
        }
    }
}
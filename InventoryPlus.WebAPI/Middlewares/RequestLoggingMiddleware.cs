// Middleware/RequestLoggingMiddleware.cs

using System.Diagnostics;
using Serilog;

namespace InventoryPlus.WebAPI.Middlewares;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public RequestLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var startTime = Stopwatch.GetTimestamp();
        
        try
        {
            await _next(context);
            var elapsed = GetElapsedMilliseconds(startTime, Stopwatch.GetTimestamp());
            
            Log.Information("HTTP {Method} {Path} responded {StatusCode} in {Elapsed:0.0000} ms",
                context.Request.Method,
                context.Request.Path,
                context.Response.StatusCode,
                elapsed);
        }
        catch
        {
            var elapsed = GetElapsedMilliseconds(startTime, Stopwatch.GetTimestamp());
            Log.Error("HTTP {Method} {Path} errored after {Elapsed:0.0000} ms",
                context.Request.Method,
                context.Request.Path,
                elapsed);
            throw;
        }
    }

    private static double GetElapsedMilliseconds(long start, long stop)
    {
        return (stop - start) * 1000 / (double)Stopwatch.Frequency;
    }
}
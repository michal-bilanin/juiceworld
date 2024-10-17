using System.Diagnostics;

namespace WebApi.Middleware;

public class RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
{
    public async Task Invoke(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();
        var traceIdentifier = context.TraceIdentifier;

        logger.LogInformation("[INCOMING_REQUEST] - TRACE_ID: {traceId} | METHOD: {method} | URL: {url} | IP: {ip}",
            traceIdentifier,
            context.Request.Method,
            context.Request.Path,
            context.Connection.RemoteIpAddress);

        await next(context);

        stopwatch.Stop();

        logger.LogInformation(
            "[OUTGOING_RESPONSE] - TRACE_ID: {traceId} | METHOD: {method} | URL: {url} | IP: {ip} | " +
            "STATUS: {status} | DOWNTIME: {downtime}ms",
            traceIdentifier,
            context.Request.Method,
            context.Request.Path,
            context.Connection.RemoteIpAddress,
            context.Response.StatusCode,
            stopwatch.ElapsedMilliseconds);
    }
}

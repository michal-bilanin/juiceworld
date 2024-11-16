using System.Diagnostics;

namespace PresentationLayer.Mvc.Middleware;

public class RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
{
    public async Task Invoke(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();
        var traceIdentifier = context.TraceIdentifier;

        // Log incoming request
        logger.LogInformation(
            "INCOMING_REQUEST - TRACE_ID: {TraceId}, METHOD: {Method}, URL: {Url}, IP: {IP}",
            traceIdentifier,
            context.Request.Method,
            context.Request.Path,
            context.Connection.RemoteIpAddress);

        await next(context);

        stopwatch.Stop();

        // Log outgoing response
        logger.LogInformation(
            "OUTGOING_RESPONSE - TRACE_ID: {TraceId}, METHOD: {Method}, URL: {Url}, IP: {IP}, STATUS: {Status}, DOWNTIME: {Downtime}ms",
            traceIdentifier,
            context.Request.Method,
            context.Request.Path,
            context.Connection.RemoteIpAddress,
            context.Response.StatusCode,
            stopwatch.ElapsedMilliseconds);
    }
}

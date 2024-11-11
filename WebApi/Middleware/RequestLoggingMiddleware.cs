using System.Diagnostics;
using Commons.Constants;
using Serilog;
using Serilog.Events;

namespace WebApi.Middleware;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly Serilog.Core.Logger? _logger;

    public RequestLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
        var connectionString = Environment.GetEnvironmentVariable(EnvironmentConstants.LoggingDbConnectionString);
        if (connectionString == null)
        {
            Debug.Fail(
                $"Logging database connection string is null, make sure it is specified " +
                $"in the environment variable: {EnvironmentConstants.LoggingDbConnectionString}");
            return; // Not throwing as we can still run the application
        }

        var collectionName = Environment.GetEnvironmentVariable(EnvironmentConstants.LoggingDbCollectionName);
        if (collectionName == null)
        {
            Debug.Fail(
                $"Logging database collection name is null, make sure it is specified " +
                $"in the environment variable: {EnvironmentConstants.LoggingDbCollectionName}");
            return;
        }

        _logger = new LoggerConfiguration()
            .WriteTo.MongoDB(connectionString, collectionName)
            .WriteTo.Console(LogEventLevel.Information)
            .CreateLogger();
    }

    public async Task Invoke(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();
        var traceIdentifier = context.TraceIdentifier;

        // Log incoming request
        _logger?.Information(
            "INCOMING_REQUEST - TRACE_ID: {TraceId}, METHOD: {Method}, URL: {Url}, IP: {IP}",
            traceIdentifier,
            context.Request.Method,
            context.Request.Path,
            context.Connection.RemoteIpAddress);

        await _next(context);

        stopwatch.Stop();

        // Log outgoing response
        _logger?.Information(
            "OUTGOING_RESPONSE - TRACE_ID: {TraceId}, METHOD: {Method}, URL: {Url}, IP: {IP}, STATUS: {Status}, DOWNTIME: {Downtime}ms",
            traceIdentifier,
            context.Request.Method,
            context.Request.Path,
            context.Connection.RemoteIpAddress,
            context.Response.StatusCode,
            stopwatch.ElapsedMilliseconds);
    }
}

using System.Diagnostics;
using Commons.Constants;
using MongoDB.Bson;
using MongoDB.Driver;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.ClickHouse.Extensions;
using Serilog.Sinks.ClickHouse.Provider;
using Serilog.Sinks.MongoDB;

namespace WebApi.Middleware;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly Serilog.Core.Logger? _logger;

    public RequestLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
        var connectionString = Environment.GetEnvironmentVariable(EnvironmentConstants.LoggingDbConnectionString);
        var collectionName = Environment.GetEnvironmentVariable(EnvironmentConstants.LoggingDbCollectionName);
        var databaseName = Environment.GetEnvironmentVariable(EnvironmentConstants.LoggingDbDatabaseName);

        if (connectionString is null || collectionName is null || databaseName is null)
        {
            Debug.Fail(
                $"Logging database connection string, collection name or database name is null, make sure they are specified " +
                $"in the environment variables: " +
                $"{EnvironmentConstants.LoggingDbConnectionString} ({connectionString}), " +
                $"{EnvironmentConstants.LoggingDbCollectionName} ({collectionName}), " +
                $"{EnvironmentConstants.LoggingDbDatabaseName} ({databaseName})");
            return;
        }

        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);

        _logger  = new LoggerConfiguration()
            .WriteTo.MongoDBCapped(database, collectionName: collectionName)
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

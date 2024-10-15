namespace WebApi.Middleware;

public class RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
{
    public async Task Invoke(HttpContext context)
    {
        logger.LogInformation("[INCOMING_REQUEST] - METHOD: {method} | URL: {url} | IP: {ip}",
            context.Request.Method,
            context.Request.Path,
            context.Connection.RemoteIpAddress);

        await next(context);
    }
}
using System.Diagnostics;
using System.Text.Json;
using BusinessLayer.Installers;
using Microsoft.AspNetCore.Diagnostics;
using WebApi.Installers;
using WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.BusinessLayerInstall();
builder.Services.WebApiInstall();

var app = builder.Build();

const string apiPortKey = "API_PORT";
var apiPort = Environment.GetEnvironmentVariable(apiPortKey);
if (apiPort == null)
{
    Debug.Fail(
        $"API port is null, make sure it is specified " +
        $"in the environment variable: {apiPortKey}");
    return;
}

app.Urls.Add($"https://localhost:{apiPort}");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/json";

        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
        if (exceptionHandlerPathFeature != null)
        {
            var exception = exceptionHandlerPathFeature.Error;

            var result = JsonSerializer.Serialize(new { error = exception.Message });
            await context.Response.WriteAsync(result);
        }
    });
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<RequestLoggingMiddleware>();

app.MapControllers();
app.Run();

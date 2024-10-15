using JuiceWorld.Data;
using Microsoft.EntityFrameworkCore;
using WebApi.Installers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.WebApiInstall("secret");

builder.Services.AddDbContextFactory<JuiceWorldDbContext>(options =>
{
    const string connectionStringKey = "DB_CONNECTION_STRING";
    var connectionString = Environment.GetEnvironmentVariable(connectionStringKey);

    if (connectionString == null)
    {
        System.Diagnostics.Debug.Fail(
            $"Connection string is null, make sure it is specified " +
            $"in the environment variable: {connectionStringKey}");
        return;
    }

    options
        .UseNpgsql(connectionString)
        .LogTo(s => System.Diagnostics.Debug.WriteLine(s))
        .UseLazyLoadingProxies();
});

var app = builder.Build();

const string apiPortKey = "API_PORT";
var apiPort = Environment.GetEnvironmentVariable(apiPortKey);
if (apiPort == null)
{
    System.Diagnostics.Debug.Fail(
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

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
using JuiceWorld.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContextFactory<JuiceWorldDbContext>(options =>
{
    IConfiguration configuration = builder.Configuration;
    var connectionString = configuration.GetConnectionString("JuiceWorldDb");
    options
        .UseNpgsql(connectionString)
        .LogTo(s => System.Diagnostics.Debug.WriteLine(s))
        .UseLazyLoadingProxies();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
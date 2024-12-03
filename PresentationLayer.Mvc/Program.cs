using BusinessLayer.Installers;
using JuiceWorld.Installers;
using Microsoft.Extensions.Logging.Console;
using PresentationLayer.Mvc.Installers;
using PresentationLayer.Mvc.Middleware;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.BusinessLayerInstall();
builder.Services.DalInstall();
builder.Services.MvcInstall();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<RequestLoggingMiddleware>();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();

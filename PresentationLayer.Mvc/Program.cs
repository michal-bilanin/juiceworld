using BusinessLayer.Installers;
using Commons.Middleware;
using JuiceWorld.Installers;
using PresentationLayer.Mvc.Installers;

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

app.UseMiddleware<RequestLoggingMiddleware>("MVC");

app.MapControllerRoute(
    "areas",
    "{area:exists=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();
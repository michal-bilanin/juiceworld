using JuiceWorld.Entities;
using Microsoft.OpenApi.Models;

namespace WebApi.Installers;

public static class WebApiInstaller
{
    public static IServiceCollection WebApiInstall(this IServiceCollection services, string secret)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" }); });

        return services;
    }
}
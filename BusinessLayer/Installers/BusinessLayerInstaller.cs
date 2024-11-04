using Microsoft.Extensions.DependencyInjection;

namespace BusinessLayer.Installers;

public static class BusinessLayerInstaller
{
    public static IServiceCollection BusinessLayerInstall(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(BusinessLayerInstaller));
        return services;
    }
}

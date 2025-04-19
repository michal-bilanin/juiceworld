using Azure.Storage.Blobs;
using BusinessLayer.Facades;
using BusinessLayer.Facades.Interfaces;
using BusinessLayer.Services;
using BusinessLayer.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BusinessLayer.Installers;

public static class BusinessLayerInstaller
{
    public static IServiceCollection BusinessLayerInstall(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(BusinessLayerInstaller));

        services.AddScoped<IGiftCardService, GiftCardService>();
        services.AddScoped<ICartItemService, CartItemService>();
        services.AddScoped<IManufacturerService, ManufacturerService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IReviewService, ReviewService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IWishListItemService, WishListItemService>();
        services.AddScoped<IAuditTrailService, AuditTrailService>();
        services.AddScoped<ITagService, TagService>();
        services.AddScoped<IProductFacade, ProductFacade>();
        services.AddMemoryCache();

        var connectionString = configuration["AzureBlobStorage:ConnectionString"];
        var containerName = configuration["AzureBlobStorage:ContainerName"];

        services.AddSingleton(x => new BlobServiceClient(connectionString));
        services.AddScoped<IImageService>(x => new ImageService(
            x.GetRequiredService<ILogger<ImageService>>(),
            x.GetRequiredService<BlobServiceClient>(),
            containerName!
        ));

        return services;
    }
}

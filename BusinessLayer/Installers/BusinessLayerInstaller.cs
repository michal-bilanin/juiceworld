using Azure.Storage.Blobs;
using BusinessLayer.Facades;
using BusinessLayer.Facades.Interfaces;
using BusinessLayer.Services;
using BusinessLayer.Services.Interfaces;
using Commons.Constants;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BusinessLayer.Installers;

public static class BusinessLayerInstaller
{
    public static IServiceCollection BusinessLayerInstall(this IServiceCollection services)
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

        var connectionString = Environment.GetEnvironmentVariable(EnvironmentConstants.AzureBlobConnectionString);
        var containerName = Environment.GetEnvironmentVariable(EnvironmentConstants.AzureBlobContainerName);

        if (string.IsNullOrEmpty(connectionString) || string.IsNullOrEmpty(containerName))
        {
            throw new InvalidOperationException("Azure Blob Storage environment variables are not set.");
        }

        services.AddSingleton(x => new BlobServiceClient(connectionString));
        services.AddScoped<IImageService>(x => new ImageService(
            x.GetRequiredService<ILogger<ImageService>>(),
            x.GetRequiredService<BlobServiceClient>(),
            containerName
        ));

        return services;
    }
}

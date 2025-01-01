using BusinessLayer.Facades;
using BusinessLayer.Facades.Interfaces;
using BusinessLayer.Services;
using BusinessLayer.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

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
        services.AddScoped<IImageService, ImageService>();
        services.AddScoped<IProductFacade, ProductFacade>();
        services.AddMemoryCache();

        return services;
    }
}

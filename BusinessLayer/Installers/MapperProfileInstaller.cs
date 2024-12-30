using AutoMapper;
using BusinessLayer.DTOs;
using JuiceWorld.Entities;

namespace BusinessLayer.Installers;

public class MapperProfileInstaller : Profile
{
    public MapperProfileInstaller()
    {
        CreateMap<AddressDto, Address>().ReverseMap();
        CreateMap<CartItemDto, CartItem>().ReverseMap();
        CreateMap<ManufacturerDto, Manufacturer>().ReverseMap();
        CreateMap<OrderDto, Order>().ReverseMap();
        CreateMap<CreateOrderDto, Order>().ReverseMap();
        CreateMap<OrderProductDto, OrderProduct>().ReverseMap();
        CreateMap<ProductDto, Product>().ReverseMap();
        CreateMap<ProductDetailDto, Product>().ReverseMap();
        CreateMap<ReviewDto, Review>().ReverseMap();
        CreateMap<UserDto, User>().ReverseMap();
        CreateMap<WishListItemDto, WishListItem>().ReverseMap();
        CreateMap<WishListItemDetailDto, WishListItem>().ReverseMap();
        CreateMap<AuditTrailDto, AuditTrail>().ReverseMap();
        CreateMap<TagDto, Tag>().ReverseMap();
    }
}

using AutoMapper;
using JuiceWorld.Entities;

namespace WebApi.Installers;

public class MapperProfileInstaller : Profile
{
    public MapperProfileInstaller()
    {
        CreateMap<AddressDto, Address>().ReverseMap();
        CreateMap<CartItemDto, CartItem>().ReverseMap();
        CreateMap<ManufacturerDto, Manufacturer>().ReverseMap();
        CreateMap<OrderDto, Order>().ReverseMap();
        CreateMap<ProductDto, Product>().ReverseMap();
        CreateMap<ReviewDto, Review>().ReverseMap();
        CreateMap<UserDto, User>().ReverseMap();
        CreateMap<WishListItem, WishListItem>().ReverseMap();
    }
}

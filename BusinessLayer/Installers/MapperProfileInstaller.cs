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
        CreateMap<OrderDetailDto, Order>().ReverseMap();
        CreateMap<CreateOrderDto, Order>().ReverseMap();
        CreateMap<OrderProductDto, OrderProduct>().ReverseMap();
        CreateMap<OrderProductDetailDto, OrderProduct>().ReverseMap();
        CreateMap<ProductDto, Product>()
            .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.TagIds.Select(id => new Tag { Id = id })))
            .ReverseMap()
            .ForMember(dest => dest.TagIds, opt => opt.MapFrom(src => src.Tags.Select(tag => tag.Id)));
        CreateMap<ProductDetailDto, Product>().ReverseMap();
        CreateMap<ReviewDto, Review>().ReverseMap();
        CreateMap<UserDto, User>().ReverseMap();
        CreateMap<UserDto, UserUpdateDto>().ReverseMap();
        CreateMap<UserDto, UserRegisterDto>().ReverseMap();
        CreateMap<UserRegisterDto, UserDto>().ReverseMap();
        CreateMap<WishListItemDto, WishListItem>().ReverseMap();
        CreateMap<WishListItemDetailDto, WishListItem>().ReverseMap();
        CreateMap<AuditTrailDto, AuditTrail>().ReverseMap();
        CreateMap<TagDto, Tag>().ReverseMap();
    }
}

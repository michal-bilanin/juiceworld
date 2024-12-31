using AutoMapper;
using BusinessLayer.DTOs;
using PresentationLayer.Mvc.Models;

namespace PresentationLayer.Mvc.Installers;

public class MvcMapperInstaller : Profile
{
    public MvcMapperInstaller()
    {
        CreateMap<UserProfileViewModel, UserDto>().ReverseMap();
        CreateMap<UserSimpleViewModel, UserDto>().ReverseMap();
        CreateMap<UserUpdateRestrictedViewModel, UserDto>().ReverseMap();
        CreateMap<UserUpdateRestrictedViewModel, UserUpdateDto>().ReverseMap();

        CreateMap<SearchablesFilterViewModel, ProductFilterDto>()
            .ForMember(dest => dest.NameQuery, opt => opt.MapFrom(src => src.NameQuery))
            .ForMember(dest => dest.PriceMax, opt => opt.MapFrom(src => src.ProductPriceMax))
            .ForMember(dest => dest.PriceMin, opt => opt.MapFrom(src => src.ProductPriceMin))
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.ProductCategory))
            .ForMember(dest => dest.PageIndex, opt => opt.MapFrom(src => src.ProductPageIndex))
            .ForMember(dest => dest.PageSize, opt => opt.MapFrom(src => src.ProductPageSize))
            .ForMember(dest => dest.TagId, opt => opt.MapFrom(src => src.TagId))
            .ForMember(dest => dest.ManufacturerId, opt => opt.MapFrom(src => src.ManufacturerId));

        CreateMap<SearchablesFilterViewModel, ManufacturerFilterDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.NameQuery))
            .ForMember(dest => dest.PageIndex, opt => opt.MapFrom(src => src.ManufacturerPageIndex))
            .ForMember(dest => dest.PageSize, opt => opt.MapFrom(src => src.ManufacturerPageSize));

        CreateMap<SearchablesFilterViewModel, TagFilterDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.NameQuery))
            .ForMember(dest => dest.PageIndex, opt => opt.MapFrom(src => src.TagPageIndex))
            .ForMember(dest => dest.PageSize, opt => opt.MapFrom(src => src.TagPageSize));
    }
}

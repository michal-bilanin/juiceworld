using AutoMapper;
using BusinessLayer.DTOs;
using PresentationLayer.Mvc.Models;

namespace PresentationLayer.Mvc.Installers;

public class MvcMapperInstaller : Profile
{
    public MvcMapperInstaller()
    {
        CreateMap<UserRegisterViewModel, UserRegisterDto>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Username))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
            .ForMember(dest => dest.Bio, opt => opt.MapFrom(src => src.Bio)).ReverseMap();
        CreateMap<UserProfileViewModel, UserDto>().ReverseMap();
    }
}

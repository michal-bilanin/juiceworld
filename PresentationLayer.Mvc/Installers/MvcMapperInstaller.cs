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
    }
}

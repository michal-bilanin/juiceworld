using AutoMapper;
using BusinessLayer.DTOs;
using JuiceWorld.Entities;
using PresentationLayer.Mvc.Models;

namespace BusinessLayer.Installers;

public class MapperInstaller : Profile
{
    public MapperInstaller()
    {
        CreateMap<UserRegisterViewModel, UserRegisterDto>();
    }
}
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.Services.Interfaces;
using Commons.Constants;
using Commons.Enums;
using Commons.Utils;
using Infrastructure.QueryObjects;
using Infrastructure.Repositories;
using JuiceWorld.Data;
using JuiceWorld.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BusinessLayer.Services;

public class UserService(IRepository<User> userRepository,
    IQueryObject<User> userQueryObject,
    UserManager<User> userManager,
    IMapper mapper) : IUserService
{
    public async Task<IdentityResult> RegisterUserAsync(UserRegisterDto userRegisterDto)
    {
        if (await GetUserByEmailAsync(userRegisterDto.Email) is not null)
        {
            return null;
        }

        var user = new User
        {
            UserName = userRegisterDto.UserName,
            Email = userRegisterDto.Email,
            Bio = userRegisterDto.Bio,
            UserRole = UserRole.Customer
        };
        
        var result = await userManager.CreateAsync(user, userRegisterDto.Password);
        return result;
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        var users = await userRepository.GetAllAsync();
        return mapper.Map<List<UserDto>>(users);
    }

    public async Task<UserDto?> GetUserByEmailAsync(string email)
    {
        var user = (await userQueryObject.Filter(user => user.Email == email).ExecuteAsync()).FirstOrDefault();
        return user is null ? null : mapper.Map<UserDto>(user);
    }

    public async Task<UserDto?> GetUserByIdAsync(int id)
    {
        var user = await userRepository.GetByIdAsync(id);
        return user is null ? null : mapper.Map<UserDto>(user);
    }

    public async Task<UserDto?> UpdateUserAsync(UserDto userDto)
    {
        await userManager.UpdateAsync(mapper.Map<User>(userDto));
        var updatedUser = await userRepository.GetByIdAsync(userDto.Id);
        return updatedUser is null ? null : mapper.Map<UserDto>(updatedUser);
    }

    public async Task<bool> DeleteUserByIdAsync(int id)
    {
        return await userRepository.DeleteAsync(id);
    }
}

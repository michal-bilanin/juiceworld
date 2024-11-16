using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.Services.Interfaces;
using Commons.Utils;
using Infrastructure.QueryObjects;
using Infrastructure.Repositories;
using JuiceWorld.Entities;

namespace BusinessLayer.Services;

public class UserService(IRepository<User> userRepository, IQueryObject<User> userQueryObject, IMapper mapper) : IUserService
{
    public async Task<UserDto?> CreateUserAsync(UserDto userDto)
    {
        var newUser = await userRepository.CreateAsync(mapper.Map<User>(userDto));
        return newUser is null ? null : mapper.Map<UserDto>(newUser);
    }
    
    public async Task<UserDto?> RegisterUserAsync(UserRegisterDto userRegisterDto)
    {
        
        if (await GetUserByEmailAsync(userRegisterDto.Email) is not null)
        {
            return null;
        }

        var salt = AuthUtils.GenerateSalt();
        var userDto = new UserDto
        {
            UserName = userRegisterDto.UserName,
            Email = userRegisterDto.Email,
            PasswordSalt = salt,
            PasswordHash = AuthUtils.HashPassword(userRegisterDto.Password, salt, 10),
            PasswordHashRounds = 10,
            Bio = userRegisterDto.Bio,
        };
        
        return await CreateUserAsync(userDto);
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        var users = await userRepository.GetAllAsync();
        return mapper.Map<List<UserDto>>(users);
    }

    public async Task<UserDto?> GetUserByEmailAsync(string email)
    {
        var user = (await userQueryObject.Filter(user => user.Email == email).Execute()).FirstOrDefault();
        return user is null ? null : mapper.Map<UserDto>(user);
    }

    public async Task<UserDto?> GetUserByIdAsync(int id)
    {
        var user = await userRepository.GetByIdAsync(id);
        return user is null ? null : mapper.Map<UserDto>(user);
    }

    public async Task<UserDto?> UpdateUserAsync(UserDto userDto)
    {
        var updatedUser = await userRepository.UpdateAsync(mapper.Map<User>(userDto));
        return updatedUser is null ? null : mapper.Map<UserDto>(updatedUser);
    }

    public async Task<bool> DeleteUserByIdAsync(int id)
    {
        return await userRepository.DeleteAsync(id);
    }
}

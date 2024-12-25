using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.Services.Interfaces;
using Commons.Constants;
using Commons.Utils;
using Infrastructure.QueryObjects;
using Infrastructure.Repositories;
using JuiceWorld.Entities;
using Microsoft.IdentityModel.Tokens;

namespace BusinessLayer.Services;

public class UserService(IRepository<User> userRepository, IQueryObject<User> userQueryObject, IMapper mapper) : IUserService
{
    private const int PasswordHashRounds = 10;

    public async Task<UserDto?> CreateUserAsync(UserDto userDto)
    {
        var newUser = await userRepository.CreateAsync(mapper.Map<User>(userDto));
        return newUser is null ? null : mapper.Map<UserDto>(newUser);
    }

    public async Task<string?> LoginAsync(LoginDto login)
    {
        var user = await GetUserByEmailAsync(login.Email);
        if (user is null)
        {
            return null;
        }

        return !VerifyPassword(user, login.Password) ? null : CreateToken(user);
    }

    public async Task<string?> RegisterUserAsync(UserRegisterDto userRegisterDto)
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
            PasswordHash = AuthUtils.HashPassword(userRegisterDto.Password, salt, PasswordHashRounds),
            PasswordHashRounds = PasswordHashRounds,
            Bio = userRegisterDto.Bio,
        };

        var user = await CreateUserAsync(userDto);
        return user is null ? null : CreateToken(user);
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
        var updatedUser = await userRepository.UpdateAsync(mapper.Map<User>(userDto));
        return updatedUser is null ? null : mapper.Map<UserDto>(updatedUser);
    }

    public async Task<bool> DeleteUserByIdAsync(int id)
    {
        return await userRepository.DeleteAsync(id);
    }

    private static string CreateToken(UserDto user)
    {
        var handler = new JwtSecurityTokenHandler();

        var secret = Environment.GetEnvironmentVariable(EnvironmentConstants.JwtSecret);

        if (secret == null)
        {
            throw new Exception($"{EnvironmentConstants.JwtSecret} environment variable is not set");
        }

        var privateKey = Encoding.UTF8.GetBytes(secret);

        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(privateKey),
            SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            SigningCredentials = credentials,
            Expires = DateTime.UtcNow.AddHours(1),
            Subject = GenerateClaims(user)
        };

        var token = handler.CreateToken(tokenDescriptor);
        return handler.WriteToken(token);
    }

    private static ClaimsIdentity GenerateClaims(UserDto user)
    {
        var ci = new ClaimsIdentity();

        ci.AddClaim(new Claim(ClaimTypes.Sid, user.Id.ToString()));
        ci.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
        ci.AddClaim(new Claim(ClaimTypes.Email, user.Email));
        ci.AddClaim(new Claim(ClaimTypes.Role, user.UserRole.ToString()));

        return ci;
    }

    private bool VerifyPassword(UserDto user, string password) =>
        AuthUtils.HashPassword(password, user.PasswordSalt, user.PasswordHashRounds) == user.PasswordHash;
}

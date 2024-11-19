using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.Installers;
using BusinessLayer.Services;
using BusinessLayer.Services.Interfaces;
using Commons.Enums;
using Commons.Utils;
using JuiceWorld.Data;
using JuiceWorld.Entities;
using JuiceWorld.QueryObjects;
using JuiceWorld.Repositories;
using TestUtilities.MockedObjects;
using Xunit;
using Assert = Xunit.Assert;

namespace BusinessLayer.Tests.Services;

public class UserServiceTests
{
    private IUserService _userService;
    private IMapper _mapper;

    public UserServiceTests()
    {
        var dbContextOptions = MockedDbContext.GetOptions();
        var dbContext = MockedDbContext.CreateFromOptions(dbContextOptions);
        var userRepository = new Repository<User>(dbContext);
        var config = new MapperConfiguration(cfg => cfg.AddProfile<MapperProfileInstaller>());
        _mapper = config.CreateMapper();
        _userService = new UserService(userRepository, new QueryObject<User>(dbContext), _mapper);
    }

    [Fact]
    public async Task GetAllUsersAsync_ExactMatch()
    {
        // Arrange
        var userIdsToRetrieve = new[] { 1, 2 };

        // Act
        var result = await _userService.GetAllUsersAsync();

        // Assert
        var userDtos = result.ToList();
        Assert.Equal(userIdsToRetrieve.Length, userDtos.Count);
        Assert.All(userIdsToRetrieve, id => Assert.Contains(userDtos, user => user.Id == id));
    }

    [Fact]
    public async Task GetUserByIdAsync_ExactMatch()
    {
        // Arrange
        var userId = 1;

        // Act
        var result = await _userService.GetUserByIdAsync(userId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(userId, result.Id);
    }

    [Fact]
    public async Task GetUserByEmailAsync_ExactMatch()
    {
        // Arrange
        var email = "user@example.com";

        // Act
        var result = await _userService.GetUserByEmailAsync(email);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(email, result.Email);
    }

    [Fact]
    public async Task CreateUserAsync_Simple()
    {
        // Arrange
        var userRaw = DataInitializer.CreateUser(3, "test@test.com", "Test user", "random password", "boring bio", UserRole.Customer);
        var user = _mapper.Map<UserDto>(userRaw);

        // Act
        var result = await _userService.CreateUserAsync(user);

        // Assert
        Assert.NotNull(result);
        Assert.True(user.Id == result.Id && user.Email == result.Email &&
                    user.Bio == result.Bio && user.UserName == result.UserName &&
                    user.UserRole == result.UserRole);
        Assert.True(AuthUtils.HashPassword("random password", result.PasswordSalt, result.PasswordHashRounds) == user.PasswordHash);
    }


    [Fact]
    public async Task UpdateUserAsync_Simple()
    {
        // Arrange
        var userRaw = DataInitializer.CreateUser(2, "test@test.com", "Test user", "random password", "boring bio", UserRole.Customer);
        var user = _mapper.Map<UserDto>(userRaw);

        // Act
        var result = await _userService.UpdateUserAsync(user);

        // Assert
        Assert.NotNull(result);
        Assert.True(user.Id == result.Id && user.Email == result.Email &&
                    user.Bio == result.Bio && user.UserName == result.UserName &&
                    user.UserRole == result.UserRole);
        Assert.True(AuthUtils.HashPassword("random password", result.PasswordSalt, result.PasswordHashRounds) == user.PasswordHash);
    }



    [Fact]
    public async Task DeleteUserByIdAsync_Simple()
    {
        // Arrange
        var userId = 1;

        // Act
        var result = await _userService.DeleteUserByIdAsync(userId);

        // Assert
        Assert.True(result);
    }
}

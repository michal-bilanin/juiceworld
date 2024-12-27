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
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
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
        var userRepository = new UserRepository(dbContext);
        var config = new MapperConfiguration(cfg => cfg.AddProfile<MapperProfileInstaller>());
        _mapper = config.CreateMapper();
        _userService = new UserService(userRepository,  new UserQueryObject(dbContext), MockUserManager().Object, _mapper);
    }
    public static Mock<UserManager<User>> MockUserManager()
    {
        var userManagerMock = new Mock<UserManager<User>>(
            new Mock<IUserStore<User>>().Object,
            new Mock<IOptions<IdentityOptions>>().Object,
            new Mock<IPasswordHasher<User>>().Object,
            new IUserValidator<User>[0],
            new IPasswordValidator<User>[0],
            new Mock<ILookupNormalizer>().Object,
            new Mock<IdentityErrorDescriber>().Object,
            new Mock<IServiceProvider>().Object,
            new Mock<ILogger>().Object);
        userManagerMock
            .Setup(userManager => userManager.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
            .Returns(Task.FromResult(IdentityResult.Success));
        userManagerMock
            .Setup(userManager => userManager.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>()));
        return userManagerMock;
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

    // [Fact]
    // public async Task CreateUserAsync_Simple()
    // {
    //     // Arrange
    //     var userRaw = DataInitializer.CreateUser(3, "test@test.com", "Test user", "random password", "boring bio", UserRole.Customer);
    //     var user = _mapper.Map<UserRegisterDto>(userRaw);
    //
    //     // Act
    //     var result = await _userService.RegisterUserAsync(user);
    //
    //     // Assert
    //     Assert.NotNull(result);
    //     Assert.True(user.Id == result.Id && user.Email == result.Email &&
    //                 user.Bio == result.Bio && user.UserName == result.UserName &&
    //                 UserRole.Customer == result.UserRole);
    //     var hasher = new PasswordHasher<User>();
    //     
    //     Assert.True(hasher.VerifyHashedPassword(null, result.PasswordHash, "random password") == PasswordVerificationResult.Success);
    // }


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

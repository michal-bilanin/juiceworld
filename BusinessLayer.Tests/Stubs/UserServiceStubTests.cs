using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.Services;
using BusinessLayer.Services.Interfaces;
using Commons.Enums;
using JuiceWorld.Entities;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BusinessLayer.Installers;
using Infrastructure.QueryObjects;
using Infrastructure.Repositories;
using Xunit;
using Assert = Xunit.Assert;

namespace BusinessLayer.Tests.Services;

public class UserServiceStubTests
{
    private readonly IUserService _userService;
    private readonly Mock<IRepository<User>> _userRepositoryMock;
    private readonly Mock<IQueryObject<User>> _userQueryObjectMock;
    private readonly IMapper _mapper;
    private readonly FilteredResult<User> users = new FilteredResult<User>
    {
        Entities = new List<User>
        {
            new User
            {
                Id = 1,
                UserName = "JohnDoe",
                Email = "john.doe@example.com",
                PasswordHash = "hashedPassword1",
                PasswordHashRounds = 10,
                PasswordSalt = "salt1",
                UserRole = UserRole.Customer,
                Bio = "Bio for John Doe"
            },
            new User
            {
                Id = 2,
                UserName = "JaneSmith",
                Email = "jane.smith@example.com",
                PasswordHash = "hashedPassword2",
                PasswordHashRounds = 10,
                PasswordSalt = "salt2",
                UserRole = UserRole.Admin,
                Bio = "Bio for Jane Smith"
            }
        }
    };

    public UserServiceStubTests()
    {
        // Initialize mock repository and query object
        _userRepositoryMock = new Mock<IRepository<User>>();
        _userQueryObjectMock = new Mock<IQueryObject<User>>();

        // Configure AutoMapper
        var config = new MapperConfiguration(cfg => cfg.AddProfile<MapperProfileInstaller>());
        _mapper = config.CreateMapper();

        // Initialize the service with the mocked repository and mapper
        _userService = new UserService(_userRepositoryMock.Object, _userQueryObjectMock.Object, _mapper);
    }

    [Fact]
    public async Task GetAllUsersAsync_ExactMatch()
    {
        // Arrange
        _userRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(users.Entities);

        // Act
        var result = await _userService.GetAllUsersAsync();

        // Assert
        var userDtos = result.ToList();
        Assert.Equal(users.Entities.Count(), userDtos.Count);
        Assert.All(users.Entities, user => Assert.Contains(userDtos, dto => dto.Id == user.Id));
    }

    [Fact]
    public async Task GetUserByIdAsync_ExactMatch()
    {
        // Arrange
        var userId = 1;
        _userRepositoryMock.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync(users.Entities.First());

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
        var email = "john.doe@example.com";
        _userQueryObjectMock.Setup(q => q.Filter(It.IsAny<Expression<Func<User, bool>>>()))
            .Returns(_userQueryObjectMock.Object);
        _userQueryObjectMock.Setup(q => q.ExecuteAsync())
            .ReturnsAsync(new FilteredResult<User>
            {
                Entities = users.Entities.Where(u => u.Email == email)
            });

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
        var userDto = new UserDto
        {
            Id = 3,
            UserName = "NewUser",
            Email = "new.user@example.com",
            PasswordHash = "hashedPassword3",
            PasswordHashRounds = 10,
            PasswordSalt = "salt3",
            Bio = "Bio for New User",
            UserRole = UserRole.Customer
        };

        var user = _mapper.Map<User>(userDto);
        _userRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<User>(), null)).ReturnsAsync(user);

        // Act
        var result = await _userService.CreateUserAsync(userDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(userDto.Id, result.Id);
        Assert.Equal(userDto.UserName, result.UserName);
        Assert.Equal(userDto.Email, result.Email);
        Assert.Equal(userDto.Bio, result.Bio);
    }

    [Fact]
    public async Task UpdateUserAsync_Simple()
    {
        // Arrange
        var userDto = new UserDto
        {
            Id = 1,
            UserName = "UpdatedUser",
            Email = "updated.user@example.com",
            PasswordHash = "updatedHash",
            PasswordHashRounds = 10,
            PasswordSalt = "updatedSalt",
            Bio = "Updated bio",
            UserRole = UserRole.Admin
        };

        var user = _mapper.Map<User>(userDto);
        _userRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<User>(), null)).ReturnsAsync(user);

        // Act
        var result = await _userService.UpdateUserAsync(userDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(userDto.Id, result.Id);
        Assert.Equal(userDto.UserName, result.UserName);
        Assert.Equal(userDto.Email, result.Email);
        Assert.Equal(userDto.Bio, result.Bio);
    }

    [Fact]
    public async Task DeleteUserByIdAsync_Simple()
    {
        // Arrange
        var userId = 1;
        _userRepositoryMock.Setup(repo => repo.DeleteAsync(userId, null)).ReturnsAsync(true);

        // Act
        var result = await _userService.DeleteUserByIdAsync(userId);

        // Assert
        Assert.True(result);
    }
}

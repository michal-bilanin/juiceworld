using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.Installers;
using BusinessLayer.Services;
using BusinessLayer.Services.Interfaces;
using Commons.Enums;
using JuiceWorld.Entities;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Repositories;
using Xunit;
using Assert = Xunit.Assert;

namespace BusinessLayer.Tests.Services;

public class AddressServiceStubTests
{
    private readonly IAddressService _addressService;
    private readonly Mock<IRepository<Address>> _addressRepositoryMock;
    private readonly IMapper _mapper;
    private readonly List<Address> addresses = new List<Address>
    {
        new Address
        {
            Id = 1,
            Name = "John Doe",
            City = "New York",
            Street = "Main Street",
            HouseNumber = "123",
            ZipCode = "10001",
            Country = "USA",
            Type = AddressType.Shipping,
            UserId = 1,
            User = null,
            Orders = new List<Order>()
        },
        new Address
        {
            Id = 2,
            Name = "Jane Smith",
            City = "Los Angeles",
            Street = "Second Street",
            HouseNumber = "456",
            ZipCode = "90001",
            Country = "USA",
            Type = AddressType.Billing,
            UserId = 2,
            User = null,
            Orders = new List<Order>()
        }
    };

    public AddressServiceStubTests()
    {
        // Initialize mock repository
        _addressRepositoryMock = new Mock<IRepository<Address>>();

        // Configure AutoMapper
        var config = new MapperConfiguration(cfg => cfg.AddProfile<MapperProfileInstaller>());
        _mapper = config.CreateMapper();

        // Initialize the service with the mocked repository and mapper
        _addressService = new AddressService(_addressRepositoryMock.Object, _mapper);
    }

    [Fact]
    public async Task GetAllAddresssAsync_ExactMatch()
    {
        // Arrange

        
        _addressRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(addresses);

        // Act
        var result = await _addressService.GetAllAddressesAsync();

        // Assert
        var addressDtos = result.ToList();
        Assert.Equal(addresses.Count, addressDtos.Count);
        Assert.All(addresses, address => Assert.Contains(addressDtos, dto => dto.Id == address.Id));
    }

    [Fact]
    public async Task GetAddressByIdAsync_ExactMatch()
    {
        // Arrange
        var addressId = 1;
        _addressRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(addresses[0]);

        // Act
        var result = await _addressService.GetAddressByIdAsync(addressId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(addressId, result.Id);
    }

    [Fact]
    public async Task CreateAddressAsync_Simple()
    {
        // Arrange
        var addressDto = new AddressDto
        {
            Id = 111,
            UserId = 1,
            Name = "John Pork",
            HouseNumber = "123",
            Street = "Main Street",
            City = "New York",
            ZipCode = "10001",
            Country = "USA",
            Type = AddressType.Shipping
        };

        var address = _mapper.Map<Address>(addressDto);
        _addressRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Address>(), null)).ReturnsAsync(address);

        // Act
        var result = await _addressService.CreateAddressAsync(addressDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(addressDto.Id, result.Id);
        Assert.Equal(addressDto.Name, result.Name);
        Assert.Equal(addressDto.UserId, result.UserId);
        Assert.Equal(addressDto.HouseNumber, result.HouseNumber);
        Assert.Equal(addressDto.Street, result.Street);
        Assert.Equal(addressDto.City, result.City);
        Assert.Equal(addressDto.ZipCode, result.ZipCode);
        Assert.Equal(addressDto.Country, result.Country);
        Assert.Equal(addressDto.Type, result.Type);
    }

    [Fact]
    public async Task UpdateAddressAsync_Simple()
    {
        // Arrange
        var addressDto = new AddressDto
        {
            Id = 1,
            UserId = 1,
            Name = "John Pork",
            HouseNumber = "123",
            Street = "Main Street",
            City = "New York",
            ZipCode = "10001",
            Country = "USA",
            Type = AddressType.Shipping
        };

        var address = _mapper.Map<Address>(addressDto);
        _addressRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Address>(), null)).ReturnsAsync(address);

        // Act
        var result = await _addressService.UpdateAddressAsync(addressDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(addressDto.Id, result.Id);
        Assert.Equal(addressDto.Name, result.Name);
        Assert.Equal(addressDto.UserId, result.UserId);
        Assert.Equal(addressDto.HouseNumber, result.HouseNumber);
        Assert.Equal(addressDto.Street, result.Street);
        Assert.Equal(addressDto.City, result.City);
        Assert.Equal(addressDto.ZipCode, result.ZipCode);
        Assert.Equal(addressDto.Country, result.Country);
        Assert.Equal(addressDto.Type, result.Type);
    }

    [Fact]
    public async Task DeleteAddressByIdAsync_Simple()
    {
        // Arrange
        var addressId = 1;
        _addressRepositoryMock.Setup(repo => repo.DeleteAsync(addressId, null)).ReturnsAsync(true);

        // Act
        var result = await _addressService.DeleteAddressByIdAsync(addressId);

        // Assert
        Assert.True(result);
    }
}

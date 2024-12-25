using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.Installers;
using BusinessLayer.Services;
using BusinessLayer.Services.Interfaces;
using Commons.Enums;
using JuiceWorld.Entities;
using JuiceWorld.Repositories;
using TestUtilities.MockedObjects;
using Xunit;
using Assert = Xunit.Assert;

namespace BusinessLayer.Tests.Services;

public class AddressServiceTests
{
    private IAddressService _addressService;

    public AddressServiceTests()
    {
        var dbContextOptions = MockedDbContext.GetOptions();
        var dbContext = MockedDbContext.CreateFromOptions(dbContextOptions);
        var addressRepository = new Repository<Address>(dbContext);
        var config = new MapperConfiguration(cfg => cfg.AddProfile<MapperProfileInstaller>());
        var mapper = config.CreateMapper();
        _addressService = new AddressService(addressRepository, mapper);
    }

    [Fact]
    public async Task GetAllAddresssAsync_ExactMatch()
    {
        // Arrange
        var addressIdsToRetrieve = new[] { 1, 2, 3, 4 };

        // Act
        var result = await _addressService.GetAllAddressesAsync();

        // Assert
        var addressDtos = result.ToList();
        Assert.Equal(addressIdsToRetrieve.Length, addressDtos.Count);
        Assert.All(addressIdsToRetrieve, id => Assert.Contains(addressDtos, address => address.Id == id));
    }

    [Fact]
    public async Task GetAddressByIdAsync_ExactMatch()
    {
        // Arrange
        var addressId = 1;

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
        var address = new AddressDto
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

        // Act
        var result = await _addressService.CreateAddressAsync(address);

        // Assert
        Assert.NotNull(result);
        Assert.True(address.UserId == result.UserId && address.Id == result.Id &&
                    address.Name == result.Name && address.HouseNumber == result.HouseNumber &&
                    address.Street == result.Street && address.City == result.City &&
                    address.ZipCode == result.ZipCode && address.Country == result.Country &&
                    address.Type == result.Type);
    }

    [Fact]
    public async Task UpdateAddressAsync_Simple()
    {
        // Arrange
        var address = new AddressDto
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

        // Act
        var result = await _addressService.UpdateAddressAsync(address);

        // Assert
        Assert.NotNull(result);
        Assert.True(address.UserId == result.UserId && address.Id == result.Id &&
                    address.Name == result.Name && address.HouseNumber == result.HouseNumber &&
                    address.Street == result.Street && address.City == result.City &&
                    address.ZipCode == result.ZipCode && address.Country == result.Country &&
                    address.Type == result.Type);
    }

    [Fact]
    public async Task DeleteAddressByIdAsync_Simple()
    {
        // Arrange
        var addressId = 1;

        // Act
        var result = await _addressService.DeleteAddressByIdAsync(addressId);

        // Assert
        Assert.True(result);
    }
}

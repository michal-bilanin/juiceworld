using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.Services;
using BusinessLayer.Services.Interfaces;
using Infrastructure.Repositories;
using JuiceWorld.Entities;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.Installers;
using JuiceWorld.QueryObjects;
using TestUtilities.MockedObjects;
using Xunit;
using Assert = Xunit.Assert;

namespace BusinessLayer.Tests.Services
{
    public class ManufacturerServiceStubTests
    {
        private readonly List<Manufacturer> manufacturers = new List<Manufacturer>
        {
            new Manufacturer { Id = 1, Name = "Manufacturer 1" },
            new Manufacturer { Id = 2, Name = "Manufacturer 2" }
        };

        private readonly IManufacturerService _manufacturerService;
        private readonly Mock<IRepository<Manufacturer>> _manufacturerRepositoryMock;
        private readonly IMapper _mapper;

        public ManufacturerServiceStubTests()
        {
            // Mock the manufacturer repository
            _manufacturerRepositoryMock = new Mock<IRepository<Manufacturer>>();

            // Configure AutoMapper
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MapperProfileInstaller>());
            _mapper = config.CreateMapper();

            var queryObject = new QueryObject<Manufacturer>(MockedDbContext.CreateFromOptions(MockedDbContext.GetOptions()));
            // Initialize the service with the mocked repository
            _manufacturerService = new ManufacturerService(_manufacturerRepositoryMock.Object,queryObject, _mapper);
        }

        [Fact]
        public async Task GetAllManufacturersAsync_ExactMatch()
        {
            // Arrange
            _manufacturerRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(manufacturers);

            // Act
            var result = await _manufacturerService.GetAllManufacturersAsync();

            // Assert
            Assert.Equal(manufacturers.Count, result.Count());
            Assert.All(manufacturers, manufacturer => Assert.Contains(result, dto => dto.Name == manufacturer.Name));
        }

        [Fact]
        public async Task GetManufacturerByIdAsync_ExactMatch()
        {
            // Arrange
            var manufacturerId = 1;
            _manufacturerRepositoryMock.Setup(repo => repo.GetByIdAsync(manufacturerId)).ReturnsAsync(manufacturers[0]);

            // Act
            var result = await _manufacturerService.GetManufacturerByIdAsync(manufacturerId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(manufacturerId, result.Id);
            Assert.Equal(manufacturers[0].Name, result.Name);
        }

        [Fact]
        public async Task CreateManufacturerAsync_Simple()
        {
            // Arrange
            var manufacturerDto = new ManufacturerDto { Name = "New Manufacturer" };
            var manufacturer = _mapper.Map<Manufacturer>(manufacturerDto);
            _manufacturerRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Manufacturer>(), null)).ReturnsAsync(manufacturer);

            // Act
            var result = await _manufacturerService.CreateManufacturerAsync(manufacturerDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(manufacturerDto.Name, result.Name);
        }

        [Fact]
        public async Task UpdateManufacturerAsync_Simple()
        {
            // Arrange
            var manufacturerDto = new ManufacturerDto { Id = 1, Name = "Updated Manufacturer" };
            var updatedManufacturer = _mapper.Map<Manufacturer>(manufacturerDto);
            _manufacturerRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Manufacturer>(), null)).ReturnsAsync(updatedManufacturer);

            // Act
            var result = await _manufacturerService.UpdateManufacturerAsync(manufacturerDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(manufacturerDto.Name, result.Name);
        }

        [Fact]
        public async Task DeleteManufacturerByIdAsync_Simple()
        {
            // Arrange
            var manufacturerId = 1;
            _manufacturerRepositoryMock.Setup(repo => repo.DeleteAsync(manufacturerId, null)).ReturnsAsync(true);

            // Act
            var result = await _manufacturerService.DeleteManufacturerByIdAsync(manufacturerId);

            // Assert
            Assert.True(result);
        }
    }
}

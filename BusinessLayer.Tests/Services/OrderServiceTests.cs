<<<<<<< Updated upstream
using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.Installers;
using BusinessLayer.Services;
using BusinessLayer.Services.Interfaces;
using Commons.Enums;
using JuiceWorld.Entities;
using JuiceWorld.Repositories;
using JuiceWorld.UnitOfWork;
using TestUtilities.MockedObjects;
using Xunit;
using Assert = Xunit.Assert;

namespace BusinessLayer.Tests.Services;

public class OrderServiceTests
{
    private IOrderService _orderService;

    public OrderServiceTests()
    {
        var dbContextOptions = MockedDbContext.GetOptions();
        var dbContext = MockedDbContext.CreateFromOptions(dbContextOptions);
        var orderRepository = new Repository<Order>(dbContext);
        var config = new MapperConfiguration(cfg => cfg.AddProfile<MapperProfileInstaller>());
        var mapper = config.CreateMapper();
        var unitOfWorkProvider = new OrderUnitOfWorkProvider(() => MockedDbContext.CreateFromOptions(dbContextOptions));
        _orderService = new OrderService(orderRepository, unitOfWorkProvider, mapper);
    }

    [Fact]
    public async Task GetAllOrdersAsync_ExactMatch()
    {
        // Arrange
        var orderIdsToRetrieve = new[] { 1, 2, 3, 4 };

        // Act
        var result = await _orderService.GetAllOrdersAsync();

        // Assert
        var orderDtos = result.ToList();
        Assert.Equal(orderIdsToRetrieve.Length, orderDtos.Count);
        Assert.All(orderIdsToRetrieve, id => Assert.Contains(orderDtos, order => order.Id == id));
    }

    [Fact]
    public async Task GetOrderByIdAsync_ExactMatch()
    {
        // Arrange
        var orderId = 1;

        // Act
        var result = await _orderService.GetOrderByIdAsync(orderId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(orderId, result.Id);
    }

    [Fact]
    public async Task CreateOrderAsync_Simple()
    {
        // Arrange
        var order = new CreateOrderDto
        {
            UserId = 1,
            AddressId = 4,
            DeliveryType = DeliveryType.Express,
            PaymentMethodType = PaymentMethodType.Monero
        };

        // Act
        var result = await _orderService.CreateOrderAsync(order);

        // Assert
        Assert.NotNull(result);
        Assert.True(order.UserId == result.UserId && order.AddressId == result.AddressId &&
                    order.DeliveryType == result.DeliveryType &&
                    order.PaymentMethodType == result.PaymentMethodType);
    }

    [Fact]
    public async Task UpdateOrderAsync_Simple()
    {
        // Arrange
        var order = new OrderDto
        {
            Id = 1,
            UserId = 1,
            AddressId = 4,
            Status = OrderStatus.Pending,
            DeliveryType = DeliveryType.Express,
            PaymentMethodType = PaymentMethodType.Monero
        };

        // Act
        var result = await _orderService.UpdateOrderAsync(order);

        // Assert
        Assert.NotNull(result);
        Assert.True(order.Id == result.Id && order.UserId == result.UserId && order.AddressId == result.AddressId &&
                    order.Status == result.Status && order.DeliveryType == result.DeliveryType &&
                    order.PaymentMethodType == result.PaymentMethodType);
    }

    [Fact]
    public async Task DeleteOrderByIdAsync_Simple()
    {
        // Arrange
        var orderId = 1;

        // Act
        var result = await _orderService.DeleteOrderByIdAsync(orderId);

        // Assert
        Assert.True(result);
    }
}
=======
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

public class OrderServiceTests
{
    private IOrderService _orderService;

    public OrderServiceTests()
    {
        var dbContextOptions = MockedDbContext.GetOptions();
        var dbContext = MockedDbContext.CreateFromOptions(dbContextOptions);
        var orderRepository = new Repository<Order>(dbContext);
        var config = new MapperConfiguration(cfg => cfg.AddProfile<MapperProfileInstaller>());
        var mapper = config.CreateMapper();
        _orderService = new OrderService(orderRepository, mapper);
    }

    [Fact]
    public async Task GetAllOrdersAsync_ExactMatch()
    {
        // Arrange
        var orderIdsToRetrieve = new[] { 1, 2, 3, 4 };

        // Act
        var result = await _orderService.GetAllOrdersAsync();

        // Assert
        var orderDtos = result.ToList();
        Assert.Equal(orderIdsToRetrieve.Length, orderDtos.Count);
        Assert.All(orderIdsToRetrieve, id => Assert.Contains(orderDtos, order => order.Id == id));
    }

    [Fact]
    public async Task GetOrderByIdAsync_ExactMatch()
    {
        // Arrange
        var orderId = 1;

        // Act
        var result = await _orderService.GetOrderByIdAsync(orderId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(orderId, result.Id);
    }

    [Fact]
    public async Task CreateOrderAsync_Simple()
    {
        // Arrange
        var order = new OrderDto
        {
            UserId = 1,
            AddressId = 4,
            Status = OrderStatus.Pending,
            DeliveryType = DeliveryType.Express,
            PaymentMethodType = PaymentMethodType.Monero
        };

        // Act
        var result = await _orderService.CreateOrderAsync(order);

        // Assert
        Assert.NotNull(result);
        Assert.True(order.UserId == result.UserId && order.AddressId == result.AddressId &&
                    order.Status == result.Status && order.DeliveryType == result.DeliveryType &&
                    order.PaymentMethodType == result.PaymentMethodType);
    }

    [Fact]
    public async Task UpdateOrderAsync_Simple()
    {
        // Arrange
        var order = new OrderDto
        {
            Id = 1,
            UserId = 1,
            AddressId = 4,
            Status = OrderStatus.Pending,
            DeliveryType = DeliveryType.Express,
            PaymentMethodType = PaymentMethodType.Monero
        };

        // Act
        var result = await _orderService.UpdateOrderAsync(order);

        // Assert
        Assert.NotNull(result);
        Assert.True(order.Id == result.Id && order.UserId == result.UserId && order.AddressId == result.AddressId &&
                    order.Status == result.Status && order.DeliveryType == result.DeliveryType &&
                    order.PaymentMethodType == result.PaymentMethodType);
    }

    [Fact]
    public async Task DeleteOrderByIdAsync_Simple()
    {
        // Arrange
        var orderId = 1;

        // Act
        var result = await _orderService.DeleteOrderByIdAsync(orderId);

        // Assert
        Assert.True(result);
    }
}
>>>>>>> Stashed changes

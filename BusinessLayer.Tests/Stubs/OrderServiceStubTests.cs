using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.Installers;
using BusinessLayer.Services;
using BusinessLayer.Services.Interfaces;
using Commons.Enums;
using Infrastructure.Repositories;
using JuiceWorld.Entities;
using JuiceWorld.QueryObjects;
using JuiceWorld.UnitOfWork;
using Moq;
using TestUtilities.MockedObjects;
using Xunit;
using Assert = Xunit.Assert;

namespace BusinessLayer.Tests.Stubs
{
    public class OrderServiceStubTests
    {
        private readonly List<Order> _orders = new List<Order>
        {
            new Order
            {
                Id = 1,
                UserId = 1,
                DeliveryType = DeliveryType.Standard,
                Status = OrderStatus.Pending,
                City = "null",
                Street = "null",
                HouseNumber = "null",
                ZipCode = "null",
                Country = "null"
            },
            new Order
            {
                Id = 2,
                UserId = 2,
                DeliveryType = DeliveryType.Express,
                Status = OrderStatus.Delivered,
                City = "null",
                Street = "null",
                HouseNumber = "null",
                ZipCode = "null",
                Country = "null"
            }
        };

        private readonly IOrderService _orderService;
        private readonly Mock<IRepository<Order>> _orderRepositoryMock;
        private readonly IMapper _mapper;

        public OrderServiceStubTests()
        {
            // Mock the repositories
            _orderRepositoryMock = new Mock<IRepository<Order>>();

            // Mock the OrderUnitOfWork (this is where we inject the mocked repositories)
            var dbContextOptions = MockedDbContext.GetOptions();
            var dbContext = MockedDbContext.CreateFromOptions(dbContextOptions);
            Mock<OrderUnitOfWork> orderUnitOfWorkMock = new(dbContext);
            // Mock the Commit method (to prevent actual database calls)
            // Configure AutoMapper
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MapperProfileInstaller>());
            _mapper = config.CreateMapper();

            var orderQueryObject = new QueryObject<Order>(dbContext);
            // Initialize the service with the mocked repositories and unit of work
            _orderService = new OrderService(_orderRepositoryMock.Object, orderQueryObject, orderUnitOfWorkMock.Object, _mapper);
        }

        [Fact]
        public async Task GetAllOrdersAsync_ExactMatch()
        {
            _orderRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(_orders);
            var result = (await _orderService.GetAllOrdersAsync()).ToList();
            Assert.Equal(_orders.Count, result.Count());
            Assert.All(_orders, order => Assert.Contains(result, dto => dto.Id == order.Id));
        }

        [Fact]
        public async Task GetOrderByIdAsync_ExactMatch()
        {
            var orderId = 1;
            _orderRepositoryMock.Setup(repo => repo.GetByIdAsync(orderId)).ReturnsAsync(_orders[0]);
            var result = await _orderService.GetOrderByIdAsync(orderId);
            Assert.NotNull(result);
            Assert.Equal(orderId, result.Id);
        }

        [Fact]
        public async Task CreateOrderAsync_Simple()
        {
            var orderDto = new CreateOrderDto { Id = 3, UserId = 1, AddressId = 1, DeliveryType = DeliveryType.Standard, PaymentMethodType = PaymentMethodType.Bitcoin };
            var order = _mapper.Map<Order>(orderDto);
            _orderRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Order>(), null)).ReturnsAsync(order);
            var result = await _orderService.CreateOrderAsync(orderDto);
            Assert.NotNull(result);
            Assert.Equal(orderDto.Id, result.Id);
        }

        [Fact]
        public async Task UpdateOrderAsync_Simple()
        {
            var orderDto = new OrderDto
            {
                Id = 1,
                DeliveryType = DeliveryType.Express,
                Status = OrderStatus.Delivered,
                Departure = DateTime.Now.AddDays(-2),
                Arrival = DateTime.Now,
                PaymentMethodType = PaymentMethodType.Bitcoin,
                UserId = 1,
                AddressId = 1,
                City = "null",
                Street = "null",
                HouseNumber = "null",
                ZipCode = "null",
                Country = "null"
            };
            var updatedOrder = _mapper.Map<Order>(orderDto);
            _orderRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Order>(), null)).ReturnsAsync(updatedOrder);
            var result = await _orderService.UpdateOrderAsync(orderDto);
            Assert.NotNull(result);
            Assert.Equal(orderDto.Status, result.Status);
        }

        [Fact]
        public async Task DeleteOrderByIdAsync_Simple()
        {
            var orderId = 1;
            _orderRepositoryMock.Setup(repo => repo.DeleteAsync(orderId, null)).ReturnsAsync(true);
            var result = await _orderService.DeleteOrderByIdAsync(orderId);
            Assert.True(result);
        }
    }
}

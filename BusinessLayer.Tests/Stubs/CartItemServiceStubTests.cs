using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.Services;
using Infrastructure.Repositories;
using JuiceWorld.Entities;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.Installers;
using BusinessLayer.Services.Interfaces;
using Infrastructure.QueryObjects;
using JuiceWorld.UnitOfWork;
using Xunit;
using Assert = Xunit.Assert;

namespace BusinessLayer.Tests.Services
{
    public class CartItemServiceStubTests
    {
        private readonly ICartItemService _cartItemService;
        private readonly Mock<IRepository<CartItem>> _cartItemRepositoryMock;
        private readonly IMapper _mapper;
        private readonly List<CartItem> cartItems = new List<CartItem>
        {
            new CartItem { Id = 1, ProductId = 1, Quantity = 2 },
            new CartItem { Id = 2, ProductId = 2, Quantity = 1 }
        };

        public CartItemServiceStubTests()
        {
            _cartItemRepositoryMock = new Mock<IRepository<CartItem>>();
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MapperProfileInstaller>());
            _mapper = config.CreateMapper();
            Mock<OrderUnitOfWork> queryObjectMock = new();

            _cartItemService = new CartItemService(_cartItemRepositoryMock.Object, queryObjectMock.Object, _mapper);
        }

        [Fact]
        public async Task GetAllCartItemsAsync_ExactMatch()
        {
            _cartItemRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(cartItems);
            var result = await _cartItemService.GetAllCartItemsAsync();
            Assert.Equal(cartItems.Count, result.Count());
            Assert.All(cartItems, cartItem => Assert.Contains(result, dto => dto.Id == cartItem.Id));
        }

        [Fact]
        public async Task GetCartItemByIdAsync_ExactMatch()
        {
            var cartItemId = 1;
            _cartItemRepositoryMock.Setup(repo => repo.GetByIdAsync(cartItemId)).ReturnsAsync(cartItems[0]);
            var result = await _cartItemService.GetCartItemByIdAsync(cartItemId);
            Assert.NotNull(result);
            Assert.Equal(cartItemId, result.Id);
        }

        [Fact]
        public async Task CreateCartItemAsync_Simple()
        {
            var cartItemDto = new CartItemDto { Id = 3, ProductId = 3, Quantity = 1 };
            var cartItem = _mapper.Map<CartItem>(cartItemDto);
            _cartItemRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<CartItem>(), null)).ReturnsAsync(cartItem);
            var result = await _cartItemService.CreateCartItemAsync(cartItemDto);
            Assert.NotNull(result);
            Assert.Equal(cartItemDto.Id, result.Id);
        }

        [Fact]
        public async Task UpdateCartItemAsync_Simple()
        {
            var cartItemDto = new CartItemDto { Id = 1, ProductId = 1, Quantity = 3 };
            var updatedCartItem = _mapper.Map<CartItem>(cartItemDto);
            _cartItemRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<CartItem>(), null)).ReturnsAsync(updatedCartItem);
            var result = await _cartItemService.UpdateCartItemAsync(cartItemDto);
            Assert.NotNull(result);
            Assert.Equal(cartItemDto.Quantity, result.Quantity);
        }

        [Fact]
        public async Task DeleteCartItemByIdAsync_Simple()
        {
            var cartItemId = 1;
            _cartItemRepositoryMock.Setup(repo => repo.DeleteAsync(cartItemId, null)).ReturnsAsync(true);
            var result = await _cartItemService.DeleteCartItemByIdAsync(cartItemId);
            Assert.True(result);
        }
    }
}

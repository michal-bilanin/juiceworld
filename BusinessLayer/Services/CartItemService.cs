using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.Services.Interfaces;
using Infrastructure.Repositories;
using JuiceWorld.Entities;

namespace BusinessLayer.Services;

public class CartItemService(IRepository<CartItem> cartItemRepository, IMapper mapper) : ICartItemService
{
    public async Task<CartItemDto?> CreateCartItemAsync(CartItemDto cartItemDto)
    {
        var newCartItem = await cartItemRepository.CreateAsync(mapper.Map<CartItem>(cartItemDto));
        return newCartItem is null ? null : mapper.Map<CartItemDto>(newCartItem);
    }

    public async Task<IEnumerable<CartItemDto>> GetAllCartItemsAsync()
    {
        var cartItems = await cartItemRepository.GetAllAsync();
        return mapper.Map<List<CartItemDto>>(cartItems);
    }

    public async Task<IEnumerable<CartItemDetailDto>> GetCartItemsByUserIdAsync(int userId)
    {
        var cartItems = await cartItemRepository.GetByConditionAsync(c => c.UserId == userId, nameof(CartItem.Product));
        return mapper.Map<List<CartItemDetailDto>>(cartItems);
    }

    public async Task<CartItemDto?> GetCartItemByIdAsync(int id)
    {
        var cartItem = await cartItemRepository.GetByIdAsync(id);
        return cartItem is null ? null : mapper.Map<CartItemDto>(cartItem);
    }

    public async Task<CartItemDto?> UpdateCartItemAsync(CartItemDto cartItemDto)
    {
        var updatedCartItem = await cartItemRepository.UpdateAsync(mapper.Map<CartItem>(cartItemDto));
        return updatedCartItem is null ? null : mapper.Map<CartItemDto>(updatedCartItem);
    }

    public async Task<bool> DeleteCartItemByIdAsync(int id)
    {
        return await cartItemRepository.DeleteAsync(id);
    }
}

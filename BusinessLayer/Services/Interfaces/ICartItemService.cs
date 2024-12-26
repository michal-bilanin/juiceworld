using BusinessLayer.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BusinessLayer.Services.Interfaces;

public interface ICartItemService
{
    Task<CartItemDto?> CreateCartItemAsync(CartItemDto cartItemDto);
    Task<IEnumerable<CartItemDto>> GetAllCartItemsAsync();
    Task<IEnumerable<CartItemDetailDto>> GetCartItemsByUserIdAsync(int userId);
    Task<CartItemDto?> GetCartItemByIdAsync(int id);
    Task<CartItemDto?> UpdateCartItemAsync(CartItemDto cartItemDto);
    Task<bool> DeleteCartItemByIdAsync(int id);
}

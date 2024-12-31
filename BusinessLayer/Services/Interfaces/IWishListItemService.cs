using BusinessLayer.DTOs;

namespace BusinessLayer.Services.Interfaces;

public interface IWishListItemService
{
    Task<WishListItemDto?> CreateWishListItemAsync(WishListItemDto wishListItemDto);
    Task<IEnumerable<WishListItemDto>> GetAllWishListItemsAsync();
    Task<WishListItemDto?> GetWishListItemByIdAsync(int id);
    Task<WishListItemDetailDto?> GetWishListItemDetailByIdAsync(int id);
    Task<WishListItemDto?> UpdateWishListItemAsync(WishListItemDto wishListItemDto);
    Task<bool> DeleteWishListItemByIdAsync(int id);
}

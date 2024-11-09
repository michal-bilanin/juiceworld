using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.Services.Interfaces;
using Infrastructure.Repositories;
using JuiceWorld.Entities;

namespace BusinessLayer.Services;

public class WishListItemService(IRepository<WishListItem> wishListItemRepository, IMapper mapper) : IWishListItemService
{
    public async Task<WishListItemDto?> CreateWishListItemAsync(WishListItemDto wishListItemDto)
    {
        var newWishListItem = await wishListItemRepository.CreateAsync(mapper.Map<WishListItem>(wishListItemDto));
        return newWishListItem is null ? null : mapper.Map<WishListItemDto>(newWishListItem);
    }

    public async Task<IEnumerable<WishListItemDto>> GetAllWishListItemsAsync()
    {
        var wishListItems = await wishListItemRepository.GetAllAsync();
        return mapper.Map<List<WishListItemDto>>(wishListItems);
    }

    public async Task<WishListItemDto?> GetWishListItemByIdAsync(int id)
    {
        var wishListItem = await wishListItemRepository.GetByIdAsync(id);
        return wishListItem is null ? null : mapper.Map<WishListItemDto>(wishListItem);
    }

    public async Task<WishListItemDto?> UpdateWishListItemAsync(WishListItemDto wishListItemDto)
    {
        var updatedWishListItem = await wishListItemRepository.UpdateAsync(mapper.Map<WishListItem>(wishListItemDto));
        return updatedWishListItem is null ? null : mapper.Map<WishListItemDto>(updatedWishListItem);
    }

    public async Task<bool> DeleteWishListItemByIdAsync(int id)
    {
        return await wishListItemRepository.DeleteAsync(id);
    }
}

using AutoMapper;
using BusinessLayer.DTOs;
using Infrastructure.Repositories;
using JuiceWorld.Entities;
using JuiceWorld.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = nameof(UserRole.Customer))]
public class WishListItemController(IRepository<WishListItem> wishListItemRepository, IMapper mapper) : ControllerBase
{
    private const string ApiBaseName = "WishListItem";

    [HttpPost]
    [OpenApiOperation(ApiBaseName + nameof(CreateWishListItem))]
    public async Task<ActionResult<WishListItemDto>> CreateWishListItem(WishListItemDto wishListItem)
    {
        var result = await wishListItemRepository.CreateAsync(mapper.Map<WishListItem>(wishListItem));
        return result == null ? Problem() : Ok(mapper.Map<WishListItemDto>(result));
    }

    [HttpGet]
    [OpenApiOperation(ApiBaseName + nameof(GetAllWishListItems))]
    public async Task<ActionResult<List<WishListItemDto>>> GetAllWishListItems()
    {
        var result = await wishListItemRepository.GetAllAsync();
        return Ok(mapper.Map<ICollection<WishListItemDto>>(result).ToList());
    }

    [HttpGet("{wishListItemId:int}")]
    [OpenApiOperation(ApiBaseName + nameof(GetWishListItem))]
    public async Task<ActionResult<WishListItemDto>> GetWishListItem(int wishListItemId)
    {
        var result = await wishListItemRepository.GetByIdAsync(wishListItemId);
        return result == null ? NotFound() : Ok(mapper.Map<WishListItemDto>(result));
    }

    [HttpPut]
    [OpenApiOperation(ApiBaseName + nameof(UpdateWishListItem))]
    public async Task<ActionResult<WishListItemDto>> UpdateWishListItem(WishListItemDto wishListItem)
    {
        var result = await wishListItemRepository.UpdateAsync(mapper.Map<WishListItem>(wishListItem));
        return result == null ? Problem() : Ok(mapper.Map<WishListItemDto>(result));
    }

    [HttpDelete("{wishListItemId:int}")]
    [OpenApiOperation(ApiBaseName + nameof(DeleteWishListItem))]
    public async Task<ActionResult<bool>> DeleteWishListItem(int wishListItemId)
    {
        var result = await wishListItemRepository.DeleteAsync(wishListItemId);
        return result ? Ok() : NotFound();
    }
}

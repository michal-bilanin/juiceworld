using BusinessLayer.DTOs;
using BusinessLayer.Services.Interfaces;
using Commons.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = nameof(UserRole.Customer))]
public class WishListItemController(IWishListItemService wishListItemService) : ControllerBase
{
    private const string ApiBaseName = "WishListItem";

    [HttpPost]
    [OpenApiOperation(ApiBaseName + nameof(CreateWishListItem))]
    public async Task<ActionResult<WishListItemDto>> CreateWishListItem(WishListItemDto wishListItem)
    {
        var result = await wishListItemService.CreateWishListItemAsync(wishListItem);
        return result == null ? Problem() : Ok(result);
    }

    [HttpGet]
    [OpenApiOperation(ApiBaseName + nameof(GetAllWishListItems))]
    public async Task<ActionResult<IEnumerable<WishListItemDto>>> GetAllWishListItems()
    {
        var result = await wishListItemService.GetAllWishListItemsAsync();
        return Ok(result);
    }

    [HttpGet("{wishListItemId:int}")]
    [OpenApiOperation(ApiBaseName + nameof(GetWishListItem))]
    public async Task<ActionResult<WishListItemDto>> GetWishListItem(int wishListItemId)
    {
        var result = await wishListItemService.GetWishListItemByIdAsync(wishListItemId);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPut]
    [OpenApiOperation(ApiBaseName + nameof(UpdateWishListItem))]
    public async Task<ActionResult<WishListItemDto>> UpdateWishListItem(WishListItemDto wishListItem)
    {
        var result = await wishListItemService.UpdateWishListItemAsync(wishListItem);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{wishListItemId:int}")]
    [OpenApiOperation(ApiBaseName + nameof(DeleteWishListItem))]
    public async Task<ActionResult<bool>> DeleteWishListItem(int wishListItemId)
    {
        var result = await wishListItemService.DeleteWishListItemByIdAsync(wishListItemId);
        return result ? Ok() : NotFound();
    }
}

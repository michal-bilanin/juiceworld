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
public class CartItemController(ICartItemService cartItemService) : ControllerBase
{
    private const string ApiBaseName = "CartItem";

    [HttpPost]
    [OpenApiOperation(ApiBaseName + nameof(CreateCartItem))]
    public async Task<ActionResult<CartItemDto>> CreateCartItem(CartItemDto cartItem)
    {
        var result = await cartItemService.CreateCartItemAsync(cartItem);
        return result == null ? Problem() : Ok(result);
    }

    [HttpGet]
    [OpenApiOperation(ApiBaseName + nameof(GetAllCartItems))]
    public async Task<ActionResult<IEnumerable<CartItemDto>>> GetAllCartItems()
    {
        var result = await cartItemService.GetAllCartItemsAsync();
        return Ok(result);
    }

    [HttpGet("{cartItemId:int}")]
    [OpenApiOperation(ApiBaseName + nameof(GetCartItem))]
    public async Task<ActionResult<CartItemDto>> GetCartItem(int cartItemId)
    {
        var result = await cartItemService.GetCartItemByIdAsync(cartItemId);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPut]
    [OpenApiOperation(ApiBaseName + nameof(UpdateCartItem))]
    public async Task<ActionResult<CartItemDto>> UpdateCartItem(CartItemDto cartItem)
    {
        var result = await cartItemService.UpdateCartItemAsync(cartItem);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{cartItemId:int}")]
    [OpenApiOperation(ApiBaseName + nameof(DeleteCartItem))]
    public async Task<ActionResult<bool>> DeleteCartItem(int cartItemId)
    {
        var result = await cartItemService.DeleteCartItemByIdAsync(cartItemId);
        return result ? Ok() : NotFound();
    }
}

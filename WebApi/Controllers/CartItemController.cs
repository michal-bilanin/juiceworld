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
public class CartItemController(IRepository<CartItem> cartItemRepository, IMapper mapper) : ControllerBase
{
    private const string ApiBaseName = "CartItem";

    [HttpPost]
    [OpenApiOperation(ApiBaseName + nameof(CreateCartItem))]
    public async Task<ActionResult<CartItemDto>> CreateCartItem(CartItemDto cartItem)
    {
        var result = await cartItemRepository.CreateAsync(mapper.Map<CartItem>(cartItem));
        return result == null ? Problem() : Ok(mapper.Map<CartItemDto>(result));
    }

    [HttpGet]
    [OpenApiOperation(ApiBaseName + nameof(GetAllCartItems))]
    public async Task<ActionResult<List<CartItemDto>>> GetAllCartItems()
    {
        var result = await cartItemRepository.GetAllAsync();
        return Ok(mapper.Map<ICollection<CartItemDto>>(result).ToList());
    }

    [HttpGet("{cartItemId:int}")]
    [OpenApiOperation(ApiBaseName + nameof(GetCartItem))]
    public async Task<ActionResult<CartItemDto>> GetCartItem(int cartItemId)
    {
        var result = await cartItemRepository.GetByIdAsync(cartItemId);
        return result == null ? NotFound() : Ok(mapper.Map<CartItemDto>(result));
    }

    [HttpPut]
    [OpenApiOperation(ApiBaseName + nameof(UpdateCartItem))]
    public async Task<ActionResult<CartItemDto>> UpdateCartItem(CartItemDto cartItem)
    {
        var result = await cartItemRepository.UpdateAsync(mapper.Map<CartItem>(cartItem));
        return result == null ? Problem() : Ok(mapper.Map<CartItemDto>(result));
    }

    [HttpDelete("{cartItemId:int}")]
    [OpenApiOperation(ApiBaseName + nameof(DeleteCartItem))]
    public async Task<ActionResult<bool>> DeleteCartItem(int cartItemId)
    {
        var result = await cartItemRepository.DeleteAsync(cartItemId);
        return result ? Ok() : NotFound();
    }
}

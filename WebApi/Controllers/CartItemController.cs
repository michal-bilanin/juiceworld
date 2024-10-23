using AutoMapper;
using Infrastructure.UnitOfWork;
using JuiceWorld.Entities;
using JuiceWorld.Enums;
using JuiceWorld.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = nameof(UserRole.Customer))]
public class CartItemController(IUnitOfWorkProvider<UnitOfWork> unitOfWorkProvider, IMapper mapper) : ControllerBase
{
    private const string ApiBaseName = "CartItem";

    [HttpPost]
    [OpenApiOperation(ApiBaseName + nameof(CreateCartItem))]
    public async Task<ActionResult<CartItemDto>> CreateCartItem(CartItemDto cartItem)
    {
        using var unitOfWork = unitOfWorkProvider.Create();
        var result = await unitOfWork.CartItemRepository.Create(mapper.Map<CartItem>(cartItem));
        if (result == null)
        {
            return Problem();
        }

        await unitOfWork.Commit();
        return Ok(mapper.Map<CartItemDto>(result));
    }

    [HttpGet]
    [OpenApiOperation(ApiBaseName + nameof(GetAllCartItems))]
    public async Task<ActionResult<List<CartItemDto>>> GetAllCartItems()
    {
        using var unitOfWork = unitOfWorkProvider.Create();
        var result = await unitOfWork.CartItemRepository.GetAll();
        return Ok(mapper.Map<ICollection<CartItemDto>>(result).ToList());
    }

    [HttpGet("{cartItemId:int}")]
    [OpenApiOperation(ApiBaseName + nameof(GetCartItem))]
    public async Task<ActionResult<CartItemDto>> GetCartItem(int cartItemId)
    {
        using var unitOfWork = unitOfWorkProvider.Create();
        var result = await unitOfWork.CartItemRepository.GetById(cartItemId);
        if (result == null)
        {
            return NotFound();
        }

        return Ok(mapper.Map<CartItemDto>(result));
    }

    [HttpPut]
    [OpenApiOperation(ApiBaseName + nameof(UpdateCartItem))]
    public async Task<ActionResult<CartItemDto>> UpdateCartItem(CartItemDto cartItem)
    {
        using var unitOfWork = unitOfWorkProvider.Create();
        if (!await unitOfWork.CartItemRepository.Update(mapper.Map<CartItem>(cartItem)))
        {
            return NotFound();
        }

        await unitOfWork.Commit();
        return Ok(cartItem);
    }

    [HttpDelete("{cartItemId:int}")]
    [OpenApiOperation(ApiBaseName + nameof(DeleteCartItem))]
    public async Task<ActionResult<bool>> DeleteCartItem(int cartItemId)
    {
        using var unitOfWork = unitOfWorkProvider.Create();
        var result = await unitOfWork.CartItemRepository.Delete(cartItemId);
        if (!result)
        {
            return Problem();
        }

        await unitOfWork.Commit();
        return Ok(result);
    }
}

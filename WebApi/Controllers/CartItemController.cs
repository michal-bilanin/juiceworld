using Infrastructure.UnitOfWork;
using JuiceWorld.Entities;
using JuiceWorld.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "user")]
public class CartItemController(IUnitOfWorkProvider<UnitOfWork> unitOfWorkProvider) : ControllerBase
{
    private const string ApiBaseName = "CartItem";

    [HttpPost]
    [OpenApiOperation(ApiBaseName + nameof(CreateCartItem))]
    public async Task<ActionResult<CartItem>> CreateCartItem(CartItem cartItem)
    {
        using var unitOfWork = unitOfWorkProvider.Create();
        var result = await unitOfWork.CartItemRepository.Create(cartItem);
        if (result == null)
        {
            return Problem();
        }

        await unitOfWork.Commit();
        return Ok(result);
    }

    [HttpGet]
    [OpenApiOperation(ApiBaseName + nameof(GetAllCartItems))]
    public async Task<ActionResult<List<CartItem>>> GetAllCartItems()
    {
        using var unitOfWork = unitOfWorkProvider.Create();
        var result = await unitOfWork.CartItemRepository.GetAll();
        return Ok(result);
    }

    [HttpGet("{cartItemId:int}")]
    [OpenApiOperation(ApiBaseName + nameof(GetCartItem))]
    public async Task<ActionResult<CartItem>> GetCartItem(int cartItemId)
    {
        using var unitOfWork = unitOfWorkProvider.Create();
        var result = await unitOfWork.CartItemRepository.GetById(cartItemId);
        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPut]
    [OpenApiOperation(ApiBaseName + nameof(UpdateCartItem))]
    public async Task<ActionResult<CartItem>> UpdateCartItem(CartItem cartItem)
    {
        using var unitOfWork = unitOfWorkProvider.Create();
        var result = await unitOfWork.CartItemRepository.Update(cartItem);
        if (result == null)
        {
            return Problem();
        }

        await unitOfWork.Commit();
        return Ok(result);
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

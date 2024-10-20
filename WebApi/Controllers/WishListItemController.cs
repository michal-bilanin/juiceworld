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
public class WishListItemController(IUnitOfWorkProvider<UnitOfWork> unitOfWorkProvider) : ControllerBase
{
    private const string ApiBaseName = "WishListItem";

    [HttpPost]
    [OpenApiOperation(ApiBaseName + nameof(CreateWishListItem))]
    public async Task<ActionResult<WishListItem>> CreateWishListItem(WishListItem wishListItem)
    {
        using var unitOfWork = unitOfWorkProvider.Create();
        var result = await unitOfWork.WishListItemRepository.Create(wishListItem);
        if (result == null)
        {
            return Problem();
        }

        await unitOfWork.Commit();
        return Ok(result);
    }

    [HttpGet]
    [OpenApiOperation(ApiBaseName + nameof(GetAllWishListItems))]
    public async Task<ActionResult<List<WishListItem>>> GetAllWishListItems()
    {
        using var unitOfWork = unitOfWorkProvider.Create();
        var result = await unitOfWork.WishListItemRepository.GetAll();
        return Ok(result);
    }

    [HttpGet("{wishListItemId:int}")]
    [OpenApiOperation(ApiBaseName + nameof(GetWishListItem))]
    public async Task<ActionResult<WishListItem>> GetWishListItem(int wishListItemId)
    {
        using var unitOfWork = unitOfWorkProvider.Create();
        var result = await unitOfWork.WishListItemRepository.GetById(wishListItemId);
        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPut]
    [OpenApiOperation(ApiBaseName + nameof(UpdateWishListItem))]
    public async Task<ActionResult<WishListItem>> UpdateWishListItem(WishListItem wishListItem)
    {
        using var unitOfWork = unitOfWorkProvider.Create();
        var result = await unitOfWork.WishListItemRepository.Update(wishListItem);
        if (result == null)
        {
            return Problem();
        }

        await unitOfWork.Commit();
        return Ok(result);
    }

    [HttpDelete("{wishListItemId:int}")]
    [OpenApiOperation(ApiBaseName + nameof(DeleteWishListItem))]
    public async Task<ActionResult<bool>> DeleteWishListItem(int wishListItemId)
    {
        using var unitOfWork = unitOfWorkProvider.Create();
        var result = await unitOfWork.WishListItemRepository.Delete(wishListItemId);
        if (!result)
        {
            return Problem();
        }

        await unitOfWork.Commit();
        return Ok(result);
    }
}

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
public class WishListItemController(IUnitOfWorkProvider<UnitOfWork> unitOfWorkProvider, IMapper mapper) : ControllerBase
{
    private const string ApiBaseName = "WishListItem";

    [HttpPost]
    [OpenApiOperation(ApiBaseName + nameof(CreateWishListItem))]
    public async Task<ActionResult<WishListItemDto>> CreateWishListItem(WishListItemDto wishListItem)
    {
        using var unitOfWork = unitOfWorkProvider.Create();
        var result = await unitOfWork.WishListItemRepository.Create(mapper.Map<WishListItem>(wishListItem));
        if (result == null)
        {
            return Problem();
        }

        await unitOfWork.Commit();
        return Ok(mapper.Map<WishListItemDto>(result));
    }

    [HttpGet]
    [OpenApiOperation(ApiBaseName + nameof(GetAllWishListItems))]
    public async Task<ActionResult<List<WishListItemDto>>> GetAllWishListItems()
    {
        using var unitOfWork = unitOfWorkProvider.Create();
        var result = await unitOfWork.WishListItemRepository.GetAll();
        return Ok(mapper.Map<ICollection<WishListItemDto>>(result).ToList());
    }

    [HttpGet("{wishListItemId:int}")]
    [OpenApiOperation(ApiBaseName + nameof(GetWishListItem))]
    public async Task<ActionResult<WishListItemDto>> GetWishListItem(int wishListItemId)
    {
        using var unitOfWork = unitOfWorkProvider.Create();
        var result = await unitOfWork.WishListItemRepository.GetById(wishListItemId);
        if (result == null)
        {
            return NotFound();
        }

        return Ok(mapper.Map<WishListItemDto>(result));
    }

    [HttpPut]
    [OpenApiOperation(ApiBaseName + nameof(UpdateWishListItem))]
    public async Task<ActionResult<WishListItemDto>> UpdateWishListItem(WishListItemDto wishListItem)
    {
        using var unitOfWork = unitOfWorkProvider.Create();
        var result = await unitOfWork.WishListItemRepository.Update(mapper.Map<WishListItem>(wishListItem));
        if (result == null)
        {
            return Problem();
        }

        await unitOfWork.Commit();
        return Ok(mapper.Map<WishListItemDto>(result));
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

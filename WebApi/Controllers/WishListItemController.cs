using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WishListItemController : ControllerBase
{
    private const string ApiBaseName = "WishListItem";

    [HttpPost]
    [OpenApiOperation(ApiBaseName + nameof(CreateWishListItem))]
    public async Task<ActionResult<bool>> CreateWishListItem()
    {
        return Problem();
    }

    [HttpGet]
    [OpenApiOperation(ApiBaseName + nameof(GetAllWishListItems))]
    public async Task<ActionResult<bool>> GetAllWishListItems()
    {
        return Problem();
    }

    [HttpGet("{wishListItemId:int}")]
    [OpenApiOperation(ApiBaseName + nameof(GetWishListItem))]
    public async Task<ActionResult<bool>> GetWishListItem(int wishListItemId)
    {
        return Problem();
    }

    [HttpPut("{wishListItemId:int}")]
    [OpenApiOperation(ApiBaseName + nameof(UpdateWishListItem))]
    public async Task<ActionResult<bool>> UpdateWishListItem(int wishListItemId)
    {
        return Problem();
    }

    [HttpDelete("{wishListItemId:int}")]
    [OpenApiOperation(ApiBaseName + nameof(DeleteWishListItem))]
    public async Task<ActionResult<bool>> DeleteWishListItem(int wishListItemId)
    {
        return Problem();
    }
}
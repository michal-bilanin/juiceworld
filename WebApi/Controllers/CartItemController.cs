using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "user")]
public class CartItemController : ControllerBase
{
    private const string ApiBaseName = "CartItem";

    [HttpPost]
    [OpenApiOperation(ApiBaseName + nameof(CreateCartItem))]
    public async Task<ActionResult<bool>> CreateCartItem()
    {
        return Problem();
    }

    [HttpGet]
    [OpenApiOperation(ApiBaseName + nameof(GetAllCartItems))]
    public async Task<ActionResult<bool>> GetAllCartItems()
    {
        return Problem();
    }

    [HttpGet("{cartItemId:int}")]
    [OpenApiOperation(ApiBaseName + nameof(GetCartItem))]
    public async Task<ActionResult<bool>> GetCartItem(int cartItemId)
    {
        return Problem();
    }

    [HttpPut("{cartItemId:int}")]
    [OpenApiOperation(ApiBaseName + nameof(UpdateCartItem))]
    public async Task<ActionResult<bool>> UpdateCartItem(int cartItemId)
    {
        return Problem();
    }

    [HttpDelete("{cartItemId:int}")]
    [OpenApiOperation(ApiBaseName + nameof(DeleteCartItem))]
    public async Task<ActionResult<bool>> DeleteCartItem(int cartItemId)
    {
        return Problem();
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "user")]
public class OrderController : ControllerBase
{
    private const string ApiBaseName = "Order";

    [HttpPost]
    [OpenApiOperation(ApiBaseName + nameof(CreateOrder))]
    public async Task<ActionResult<bool>> CreateOrder()
    {
        return Problem();
    }

    [HttpGet]
    [OpenApiOperation(ApiBaseName + nameof(GetAllOrders))]
    public async Task<ActionResult<bool>> GetAllOrders()
    {
        return Problem();
    }

    [HttpGet("{orderId:int}")]
    [OpenApiOperation(ApiBaseName + nameof(GetOrder))]
    public async Task<ActionResult<bool>> GetOrder(int orderId)
    {
        return Problem();
    }

    [HttpPut("{orderId:int}")]
    [OpenApiOperation(ApiBaseName + nameof(UpdateOrder))]
    public async Task<ActionResult<bool>> UpdateOrder(int orderId)
    {
        return Problem();
    }

    [HttpDelete("{orderId:int}")]
    [OpenApiOperation(ApiBaseName + nameof(DeleteOrder))]
    public async Task<ActionResult<bool>> DeleteOrder(int orderId)
    {
        return Problem();
    }
}

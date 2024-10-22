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
public class OrderController(IUnitOfWorkProvider<UnitOfWork> unitOfWorkProvider) : ControllerBase
{
    private const string ApiBaseName = "Order";

    [HttpPost]
    [OpenApiOperation(ApiBaseName + nameof(CreateOrder))]
    public async Task<ActionResult<Order>> CreateOrder(Order order)
    {
        using var unitOfWork = unitOfWorkProvider.Create();
        var result = await unitOfWork.OrderRepository.Create(order);
        if (result == null)
        {
            return Problem();
        }

        await unitOfWork.Commit();
        return Ok(result);
    }

    [HttpGet]
    [OpenApiOperation(ApiBaseName + nameof(GetAllOrders))]
    public async Task<ActionResult<List<Order>>> GetAllOrders()
    {
        using var unitOfWork = unitOfWorkProvider.Create();
        var result = await unitOfWork.OrderRepository.GetAll();
        return Ok(result);
    }

    [HttpGet("{orderId:int}")]
    [OpenApiOperation(ApiBaseName + nameof(GetOrder))]
    public async Task<ActionResult<Order>> GetOrder(int orderId)
    {
        using var unitOfWork = unitOfWorkProvider.Create();
        var result = await unitOfWork.OrderRepository.GetById(orderId);
        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPut]
    [OpenApiOperation(ApiBaseName + nameof(UpdateOrder))]
    public async Task<ActionResult<Order>> UpdateOrder(Order order)
    {
        using var unitOfWork = unitOfWorkProvider.Create();
        var result = await unitOfWork.OrderRepository.Update(order);
        if (result == null)
        {
            return Problem();
        }

        await unitOfWork.Commit();
        return Ok(result);
    }

    [HttpDelete("{orderId:int}")]
    [OpenApiOperation(ApiBaseName + nameof(DeleteOrder))]
    public async Task<ActionResult<bool>> DeleteOrder(int orderId)
    {
        using var unitOfWork = unitOfWorkProvider.Create();
        var result = await unitOfWork.OrderRepository.Delete(orderId);
        if (!result)
        {
            return NotFound();
        }

        await unitOfWork.Commit();
        return Ok(result);
    }
}

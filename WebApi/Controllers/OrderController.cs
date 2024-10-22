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
public class OrderController(IUnitOfWorkProvider<UnitOfWork> unitOfWorkProvider, IMapper mapper) : ControllerBase
{
    private const string ApiBaseName = "Order";

    [HttpPost]
    [OpenApiOperation(ApiBaseName + nameof(CreateOrder))]
    public async Task<ActionResult<OrderDto>> CreateOrder(OrderDto order)
    {
        using var unitOfWork = unitOfWorkProvider.Create();
        var result = await unitOfWork.OrderRepository.Create(mapper.Map<Order>(order));
        if (result == null)
        {
            return Problem();
        }

        await unitOfWork.Commit();
        return Ok(mapper.Map<OrderDto>(result));
    }

    [HttpGet]
    [OpenApiOperation(ApiBaseName + nameof(GetAllOrders))]
    public async Task<ActionResult<List<OrderDto>>> GetAllOrders()
    {
        using var unitOfWork = unitOfWorkProvider.Create();
        var result = await unitOfWork.OrderRepository.GetAll();
        return Ok(mapper.Map<ICollection<OrderDto>>(result).ToList());
    }

    [HttpGet("{orderId:int}")]
    [OpenApiOperation(ApiBaseName + nameof(GetOrder))]
    public async Task<ActionResult<OrderDto>> GetOrder(int orderId)
    {
        using var unitOfWork = unitOfWorkProvider.Create();
        var result = await unitOfWork.OrderRepository.GetById(orderId);
        if (result == null)
        {
            return NotFound();
        }

        return Ok(mapper.Map<OrderDto>(result));
    }

    [HttpPut]
    [OpenApiOperation(ApiBaseName + nameof(UpdateOrder))]
    public async Task<ActionResult<OrderDto>> UpdateOrder(OrderDto order)
    {
        using var unitOfWork = unitOfWorkProvider.Create();
        var result = await unitOfWork.OrderRepository.Update(mapper.Map<Order>(order));
        if (result == null)
        {
            return Problem();
        }

        await unitOfWork.Commit();
        return Ok(mapper.Map<OrderDto>(result));
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

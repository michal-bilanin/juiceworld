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
public class OrderController(IRepository<Order> orderRepository, IMapper mapper) : ControllerBase
{
    private const string ApiBaseName = "Order";

    [HttpPost]
    [OpenApiOperation(ApiBaseName + nameof(CreateOrder))]
    public async Task<ActionResult<OrderDto>> CreateOrder(OrderDto order)
    {
        var result = await orderRepository.CreateAsync(mapper.Map<Order>(order));
        return result == null ? Problem() : Ok(mapper.Map<OrderDto>(result));
    }

    [HttpGet]
    [OpenApiOperation(ApiBaseName + nameof(GetAllOrders))]
    public async Task<ActionResult<List<OrderDto>>> GetAllOrders()
    {
        var result = await orderRepository.GetAllAsync();
        return Ok(mapper.Map<ICollection<OrderDto>>(result).ToList());
    }

    [HttpGet("{orderId:int}")]
    [OpenApiOperation(ApiBaseName + nameof(GetOrder))]
    public async Task<ActionResult<OrderDto>> GetOrder(int orderId)
    {
        var result = await orderRepository.GetByIdAsync(orderId);
        return result == null ? NotFound() : Ok(mapper.Map<OrderDto>(result));
    }

    [HttpPut]
    [OpenApiOperation(ApiBaseName + nameof(UpdateOrder))]
    public async Task<ActionResult<OrderDto>> UpdateOrder(OrderDto order)
    {
        var result = await orderRepository.UpdateAsync(mapper.Map<Order>(order));
        return result == null ? Problem() : Ok(mapper.Map<OrderDto>(result));
    }

    [HttpDelete("{orderId:int}")]
    [OpenApiOperation(ApiBaseName + nameof(DeleteOrder))]
    public async Task<ActionResult<bool>> DeleteOrder(int orderId)
    {
        var result = await orderRepository.DeleteAsync(orderId);
        return result ? Ok() : NotFound();
    }
}

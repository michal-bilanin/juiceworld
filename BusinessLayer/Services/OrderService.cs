using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.Services.Interfaces;
using Infrastructure.Repositories;
using JuiceWorld.Entities;

namespace BusinessLayer.Services;

public class OrderService(IRepository<Order> orderRepository, IMapper mapper) : IOrderService
{
    public async Task<OrderDto?> CreateOrderAsync(OrderDto orderDto)
    {
        var newOrder = await orderRepository.CreateAsync(mapper.Map<Order>(orderDto));
        return newOrder is null ? null : mapper.Map<OrderDto>(newOrder);
    }

    public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
    {
        var orders = await orderRepository.GetAllAsync();
        return mapper.Map<List<OrderDto>>(orders);
    }

    public async Task<OrderDto?> GetOrderByIdAsync(int id)
    {
        var order = await orderRepository.GetByIdAsync(id);
        return order is null ? null : mapper.Map<OrderDto>(order);
    }

    public async Task<OrderDto?> UpdateOrderAsync(OrderDto orderDto)
    {
        var updatedOrder = await orderRepository.UpdateAsync(mapper.Map<Order>(orderDto));
        return updatedOrder is null ? null : mapper.Map<OrderDto>(updatedOrder);
    }

    public Task<bool> DeleteOrderByIdAsync(int id)
    {
        return orderRepository.DeleteAsync(id);
    }
}

using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.Services.Interfaces;
using Infrastructure.QueryObjects;
using Infrastructure.Repositories;
using JuiceWorld.Entities;
using JuiceWorld.UnitOfWork;

namespace BusinessLayer.Services;

public class OrderService(
    IRepository<Order> orderRepository,
    IQueryObject<Order> orderQueryObject,
    OrderUnitOfWork orderUnitOfWork,
    IMapper mapper) : IOrderService
{
    public async Task<OrderDto?> ExecuteOrderAsync(CreateOrderDto orderDto)
    {
        var order = mapper.Map<Order>(orderDto);
        var newOrder = await orderUnitOfWork.OrderRepository.CreateAsync(order, order.UserId);

        if (newOrder is null)
        {
            return null;
        }

        var cartItems =
            (List<CartItem>)await orderUnitOfWork.CartItemRepository.GetByConditionAsync(
                ci => ci.UserId == order.UserId, nameof(CartItem.Product));

        // remove cart items
        await orderUnitOfWork.CartItemRepository.RemoveAllByConditionAsync(ci => ci.UserId == order.UserId,
            order.UserId);

        // create order products
        List<OrderProduct> orderProducts = [];
        foreach (var cartItem in cartItems)
        {
            if (cartItem.Product is null)
            {
                return null;
            }

            orderProducts.Add(new OrderProduct
            {
                OrderId = newOrder.Id,
                ProductId = cartItem.ProductId,
                Quantity = cartItem.Quantity,
                Price = cartItem.Product.Price
            });
        }

        await orderUnitOfWork.OrderProductRepository.CreateRangeAsync(orderProducts);
        await orderUnitOfWork.Commit();

        return mapper.Map<OrderDto>(newOrder);
    }

    public async Task<OrderDto?> CreateOrderAsync(CreateOrderDto orderDto)
    {
        var newOrder = await orderRepository.CreateAsync(mapper.Map<Order>(orderDto));
        return newOrder is null ? null : mapper.Map<OrderDto>(newOrder);
    }

    public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
    {
        var orders = await orderRepository.GetAllAsync();
        return mapper.Map<List<OrderDto>>(orders);
    }

    public async Task<FilteredResult<OrderDto>> GetOrdersByUserIdAsync(int userId, PaginationDto paginationDto)
    {
        var query = orderQueryObject.Filter(o => o.UserId == userId)
            .OrderBy(o => o.Id)
            .Paginate(paginationDto.PageIndex, paginationDto.PageSize);
        var filteredOrders = await query.ExecuteAsync();

        return new FilteredResult<OrderDto>
        {
            Entities = mapper.Map<List<OrderDto>>(filteredOrders.Entities),
            PageIndex = filteredOrders.PageIndex,
            TotalPages = filteredOrders.TotalPages
        };
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

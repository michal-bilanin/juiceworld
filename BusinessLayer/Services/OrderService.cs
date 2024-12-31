using System.Linq.Expressions;
using System.Text.Json;
using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.Services.Interfaces;
using Commons.Enums;
using Infrastructure.QueryObjects;
using Infrastructure.Repositories;
using JuiceWorld.Entities;
using JuiceWorld.UnitOfWork;
using Microsoft.Extensions.Caching.Memory;

namespace BusinessLayer.Services;

public class OrderService(
    IRepository<Order> orderRepository,
    IQueryObject<Order> orderQueryObject,
    OrderUnitOfWork orderUnitOfWork,
    IMemoryCache memoryCache,
    IMapper mapper) : IOrderService
{
    private string _cacheKeyPrefix = nameof(OrderService);
    
    public async Task<OrderDto?> ExecuteOrderAsync(CreateOrderDto orderDto, int? couponId)
    {
        var order = mapper.Map<Order>(orderDto);
        order.Status = OrderStatus.Pending;
        order.CouponId = couponId;
        var newOrder = await orderUnitOfWork.OrderRepository.CreateAsync(order, order.UserId);

        if (newOrder is null)
        {
            return null;
        }

        var cartItems =
            (List<CartItem>)await orderUnitOfWork.CartItemRepository.GetByConditionAsync(
                ci => ci.UserId == order.UserId, nameof(CartItem.Product));

        if (cartItems.Count == 0)
        {
            return null;
        }

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

    public async Task<FilteredResult<OrderDto>> GetOrdersAsync(PaginationDto orderFilter)
    {
        var query = orderQueryObject
            .Paginate(orderFilter.PageIndex, orderFilter.PageSize)
            .OrderBy(
                new (Expression<Func<Order, object>> KeySelector, bool IsDesc)[]
                {
                    (o => o.Status, false),
                    (o => o.Id, false)
                });

        var filteredOrders = await query.ExecuteAsync();
        return new FilteredResult<OrderDto>
        {
            Entities = mapper.Map<List<OrderDto>>(filteredOrders.Entities),
            PageIndex = filteredOrders.PageIndex,
            TotalPages = filteredOrders.TotalPages
        };
    }

    public async Task<FilteredResult<OrderDto>> GetOrdersByUserIdAsync(int userId, PaginationDto paginationDto)
    {
        string cacheKey = $"{_cacheKeyPrefix}-Orders{userId}{JsonSerializer.Serialize(paginationDto)}";
        if (memoryCache.TryGetValue(cacheKey, out FilteredResult<Order>? value))
        {
            
            var query = orderQueryObject.Filter(o => o.UserId == userId)
                    .OrderBy(
                        new (Expression<Func<Order, object>> KeySelector, bool IsDesc)[]
                        {
                            (o => o.Status, false),
                            (o => o.Id, false)
                        })
                .Paginate(paginationDto.PageIndex, paginationDto.PageSize);
    
            value = await query.ExecuteAsync();
            
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(30));
            memoryCache.Set(cacheKey, value, cacheEntryOptions);
        }

        return new FilteredResult<OrderDto>
        {
            Entities = mapper.Map<List<OrderDto>>(value!.Entities),
            PageIndex = value.PageIndex,
            TotalPages = value.TotalPages
        };
    }

    public async Task<OrderDto?> GetOrderByIdAsync(int id)
    {
        var order = await orderRepository.GetByIdAsync(id);
        return order is null ? null : mapper.Map<OrderDto>(order);
    }

    public async Task<OrderDetailDto?> GetOrderDetailByIdAsync(int id)
    {
        var order = await orderRepository.GetByIdAsync(id,
            nameof(Order.OrderProducts), $"{nameof(Order.OrderProducts)}.{nameof(OrderProduct.Product)}", nameof(User));
        return order is null ? null : mapper.Map<OrderDetailDto>(order);
    }

    public async Task<OrderDto?> UpdateOrderAsync(OrderDto orderDto)
    {
        var updatedOrder = await orderRepository.UpdateAsync(mapper.Map<Order>(orderDto));
        return updatedOrder is null ? null : mapper.Map<OrderDto>(updatedOrder);
    }

    public async Task<OrderDto?> UpdateOrderAsync(OrderDetailDto orderDto)
    {
        var order = mapper.Map<Order>(orderDto);
        order.OrderProducts = [];
        var updatedOrder = await orderUnitOfWork.OrderRepository.UpdateAsync(order);
        if (updatedOrder is null)
        {
            return null;
        }

        // remove old order products
        await orderUnitOfWork.OrderProductRepository.RemoveAllByConditionAsync(op => op.OrderId == orderDto.Id);

        // fetch products
        var products = (await orderUnitOfWork.ProductRepository.GetByConditionAsync(p =>
            orderDto.OrderProducts.Select(op => op.ProductId).Contains(p.Id))).ToList();

        // create new order products
        List<OrderProduct> orderProducts = [];
        foreach (var orderProduct in orderDto.OrderProducts)
        {
            if (orderProduct.Quantity <= 0)
            {
                continue;
            }

            var product = products.FirstOrDefault(p => p.Id == orderProduct.ProductId);
            if (product is null)
            {
                return null;
            }

            orderProducts.Add(new OrderProduct
            {
                OrderId = orderDto.Id,
                ProductId = orderProduct.ProductId,
                Quantity = orderProduct.Quantity,
                Price = product.Price
            });
        }
        await orderUnitOfWork.OrderProductRepository.CreateRangeAsync(orderProducts);

        await orderUnitOfWork.Commit();
        return mapper.Map<OrderDto>(updatedOrder);
    }

    public Task<bool> DeleteOrderByIdAsync(int id)
    {
        return orderRepository.DeleteAsync(id);
    }
}

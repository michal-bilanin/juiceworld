using Infrastructure.Repositories;
using Infrastructure.UnitOfWork;
using JuiceWorld.Data;
using JuiceWorld.Entities;
using JuiceWorld.Repositories;

namespace JuiceWorld.UnitOfWork;

public class OrderUnitOfWork : IUnitOfWork
{
    private readonly JuiceWorldDbContext _context;
    public readonly IRepository<CartItem> CartItemRepository;
    public readonly IRepository<Order> OrderRepository;

    public OrderUnitOfWork(JuiceWorldDbContext context)
    {
        _context = context;
        OrderRepository = new Repository<Order>(_context);
        CartItemRepository = new Repository<CartItem>(_context);
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public async Task Commit()
    {
        await _context.SaveChangesAsync();
    }
}

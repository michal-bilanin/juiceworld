using Infrastructure.Repositories;
using JuiceWorld.Data;
using JuiceWorld.Entities;
using JuiceWorld.Repositories;

namespace JuiceWorld.UnitOfWork;

public class OrderUnitOfWork
{
    private readonly JuiceWorldDbContext _context;
    public  IRepository<CartItem> CartItemRepository;
    public  IRepository<Order> OrderRepository;

    public OrderUnitOfWork(JuiceWorldDbContext context)
    {
        _context = context;
        OrderRepository = new Repository<Order>(_context);
        CartItemRepository = new Repository<CartItem>(_context);
    }

    public async Task Commit()
    {
        await _context.SaveChangesAsync();
    }
}

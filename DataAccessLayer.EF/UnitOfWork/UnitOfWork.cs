using Infrastructure.UnitOfWork;
using JuiceWorld.Data;
using JuiceWorld.Entities;
using JuiceWorld.Repositories;

namespace JuiceWorld.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly JuiceWorldDbContext _context;

    public Repository<Address> AddressRepository { get; }
    public Repository<CartItem> CartItemRepository { get; }
    public Repository<Manufacturer> ManufacturerRepository { get; }
    public Repository<Order> OrderRepository { get; }
    public Repository<OrderProduct> OrderProductRepository { get; }
    public Repository<Product> ProductRepository { get; }
    public Repository<Review> ReviewRepository { get; }
    public Repository<User> UserRepository { get; }
    public Repository<WishListItem> WishListItemRepository { get; }

    public UnitOfWork(JuiceWorldDbContext context)
    {
        _context = context;

        AddressRepository = new Repository<Address>(_context);
        CartItemRepository = new Repository<CartItem>(_context);
        ManufacturerRepository = new Repository<Manufacturer>(_context);
        OrderRepository = new Repository<Order>(_context);
        OrderProductRepository = new Repository<OrderProduct>(_context);
        ProductRepository = new Repository<Product>(_context);
        ReviewRepository = new Repository<Review>(_context);
        UserRepository = new Repository<User>(_context);
        WishListItemRepository = new Repository<WishListItem>(_context);
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

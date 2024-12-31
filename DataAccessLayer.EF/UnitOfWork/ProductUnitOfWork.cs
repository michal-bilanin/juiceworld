using Infrastructure.Repositories;
using JuiceWorld.Data;
using JuiceWorld.Entities;
using JuiceWorld.Repositories;

namespace JuiceWorld.UnitOfWork;

public class ProductUnitOfWork
{
    private readonly JuiceWorldDbContext _context;
    public readonly IRepository<Product> ProductRepository;
    public readonly IRepository<Tag> TagRepository;

    public ProductUnitOfWork(JuiceWorldDbContext context)
    {
        _context = context;
        ProductRepository = new Repository<Product>(_context);
        TagRepository = new Repository<Tag>(_context);
    }

    public ProductUnitOfWork() //for stub testing
    {
    }

    public async Task Commit()
    {
        await _context.SaveChangesAsync();
    }
}

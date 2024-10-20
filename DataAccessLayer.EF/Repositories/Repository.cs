using Infrastructure.Repositories;
using JuiceWorld.Data;
using JuiceWorld.Entities;
using Microsoft.EntityFrameworkCore;

namespace JuiceWorld.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    private readonly JuiceWorldDbContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public Repository(JuiceWorldDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    public async Task<TEntity?> Create(TEntity entity)
    {
        var result = await _dbSet.AddAsync(entity);
        int savedEntries = await _context.SaveChangesAsync();
        return savedEntries == 1 ? result.Entity : null;
    }

    public async Task<TEntity?> GetById(object id)
    {
        return await _dbSet.Where(e => e.Id == (int)id && e.DeletedAt == null).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<TEntity>> GetAll()
    {
        return await _dbSet.Where(e => e.DeletedAt == null).ToListAsync();
    }

    public async Task<TEntity?> Update(TEntity entity)
    {
        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
        var savedEntries = await _context.SaveChangesAsync();
        return savedEntries == 1 ? entity : null;
    }

    public async Task<bool> Delete(object id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity is not { DeletedAt: null })
        {
            return false;
        }

        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}

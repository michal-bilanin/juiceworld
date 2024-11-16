using Infrastructure.Repositories;
using JuiceWorld.Data;
using JuiceWorld.Entities;
using Microsoft.EntityFrameworkCore;

namespace JuiceWorld.Repositories;

public class Repository<TEntity>(JuiceWorldDbContext context) : IRepository<TEntity>
    where TEntity : BaseEntity
{
    private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

    public async Task<TEntity?> CreateAsync(TEntity entity, object? userId = null)
    {
        var result = await _dbSet.AddAsync(entity);

        if (userId is null)
        {
            await context.SaveChangesAsync();
        }
        else
        {
            await context.SaveChangesAsync((int)userId);
        }

        return result.Entity;
    }

    public async Task<TEntity?> GetByIdAsync(object id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<TEntity?> UpdateAsync(TEntity entity, object? userId = null)
    {
        _dbSet.Update(entity);

        if (userId is null)
        {
            await context.SaveChangesAsync();
        }
        else
        {
            await context.SaveChangesAsync((int)userId);
        }

        return entity;
    }

    public async Task<bool> DeleteAsync(object id, object? userId = null)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity is null)
        {
            return false;
        }

        _dbSet.Remove(entity);

        if (userId is null)
        {
            await context.SaveChangesAsync();
        }
        else
        {
            await context.SaveChangesAsync((int)userId);
        }

        return true;
    }
}

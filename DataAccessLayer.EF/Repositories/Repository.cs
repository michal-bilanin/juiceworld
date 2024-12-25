using System.Linq.Expressions;
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

    public async Task<TEntity?> GetByIdAsync(object id, params string[] includes)
    {
        var query = includes.Aggregate(_dbSet.AsQueryable(), (current, include) => current.Include(include));
        return await query.FirstOrDefaultAsync(e => e.Id == (int)id);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(params string[] includes)
    {
        var query = includes.Aggregate(_dbSet.AsQueryable(), (current, include) => current.Include(include));
        return await query.ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> GetByIdRangeAsync(IEnumerable<object> ids)
    {
        return await _dbSet.Where(e => ids.Contains(e.Id)).ToListAsync();
    }

    public async Task<TEntity?> UpdateAsync(TEntity entity, object? userId = null)
    {
        var existingEntity = await _dbSet.FindAsync(entity.Id);
        if (existingEntity != null)
        {
            context.Entry(existingEntity).State = EntityState.Detached;
        }

        var result = _dbSet.Update(entity);

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

    public async Task<int> RemoveAllByConditionAsync(Expression<Func<TEntity, bool>> predicate, object? userId = null)
    {
        var entities = await _dbSet.Where(predicate).ToListAsync();
        _dbSet.RemoveRange(entities);

        if (userId is null)
        {
            await context.SaveChangesAsync();
        }
        else
        {
            await context.SaveChangesAsync((int)userId);
        }

        return entities.Count;
    }
}

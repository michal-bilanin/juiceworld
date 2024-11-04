using Infrastructure.Repositories;
using JuiceWorld.Data;
using JuiceWorld.Entities;
using Microsoft.EntityFrameworkCore;

namespace JuiceWorld.Repositories;

public class Repository<TEntity>(JuiceWorldDbContext context) : IRepository<TEntity>
    where TEntity : BaseEntity
{
    private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

    public async Task<TEntity?> CreateAsync(TEntity entity)
    {
        var result = await _dbSet.AddAsync(entity);
        await context.SaveChangesAsync();
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

    public async Task<TEntity?> UpdateAsync(TEntity entity)
    {
        var existingEntity = await _dbSet.FindAsync(entity.Id);
        if (existingEntity is null)
        {
            return null;
        }

        entity.UpdatedAt = DateTime.Now;
        _dbSet.Update(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> DeleteAsync(object id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity is null)
        {
            return false;
        }

        _dbSet.Remove(entity);
        await context.SaveChangesAsync();
        return true;
    }
}

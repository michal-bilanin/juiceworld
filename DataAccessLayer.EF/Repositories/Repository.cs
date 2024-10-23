using Infrastructure.Repositories;
using JuiceWorld.Data;
using JuiceWorld.Entities;
using Microsoft.EntityFrameworkCore;

namespace JuiceWorld.Repositories;

public class Repository<TEntity>(JuiceWorldDbContext context) : IRepository<TEntity>
    where TEntity : BaseEntity
{
    private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

    public async Task<TEntity?> Create(TEntity entity)
    {
        var result = await _dbSet.AddAsync(entity);
        await context.SaveChangesAsync();
        return result.Entity;
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
        var existingEntity = await _dbSet.FindAsync(entity.Id);
        if (existingEntity is null || existingEntity.DeletedAt != null)
        {
            return null;
        }

        _dbSet.Update(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> Delete(object id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity is not { DeletedAt: null })
        {
            return false;
        }

        _dbSet.Remove(entity);
        await context.SaveChangesAsync();
        return true;
    }
}

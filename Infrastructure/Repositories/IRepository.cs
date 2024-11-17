namespace Infrastructure.Repositories;

public interface IRepository<TEntity>
    where TEntity : class
{
    Task<TEntity?> CreateAsync(TEntity entity, object? userId = null);
    Task<TEntity?> GetByIdAsync(object id, params string[] includes);
    Task<IEnumerable<TEntity>> GetAllAsync(params string[] includes);
    Task<TEntity?> UpdateAsync(TEntity entity, object? userId = null);
    Task<bool> DeleteAsync(object id, object? userId = null);
}

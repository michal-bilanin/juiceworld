namespace Infrastructure.Repositories;

public interface IRepository<TEntity>
    where TEntity : class
{
    Task<TEntity?> CreateAsync(TEntity entity);
    Task<TEntity?> GetByIdAsync(object id);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity?> UpdateAsync(TEntity entity);
    Task<bool> DeleteAsync(object id);
}

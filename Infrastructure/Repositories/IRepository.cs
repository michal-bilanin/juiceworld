using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public interface IRepository<TEntity>
    where TEntity : class
{
    Task<TEntity?> CreateAsync(TEntity entity, object? userId = null);
    Task<TEntity?> GetByIdAsync(object id, params string[] includes);
    Task<IEnumerable<TEntity>> GetAllAsync(params string[] includes);
    Task<IEnumerable<TEntity>> GetByIdRangeAsync(IEnumerable<object> ids);
    Task<TEntity?> UpdateAsync(TEntity entity, object? userId = null);
    Task<bool> DeleteAsync(object id, object? userId = null);
    Task<int> RemoveAllByConditionAsync(Expression<Func<TEntity, bool>> predicate, object? userId = null);
}

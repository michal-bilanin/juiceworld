namespace Infrastructure.Repositories;

public interface IRepository<TEntity>
    where TEntity : class
{
    Task<TEntity?> Create(TEntity entity);
    Task<TEntity?> GetById(object id);
    Task<IEnumerable<TEntity>> GetAll();
    Task<bool> Update(TEntity entity);
    Task<bool> Delete(object id);
}

using System.Linq.Expressions;

namespace Infrastructure.QueryObjects;

public interface IQueryObject<TEntity> where TEntity : class, new()
{
    IQueryObject<TEntity> Filter(Expression<Func<TEntity, bool>> filter);

    IQueryObject<TEntity> OrderBy<TKey>(Expression<Func<TEntity, TKey>> keySelector, bool isDesc = false);

    IQueryObject<TEntity> Paginate(int pageIndex, int pageSize);

    Task<IEnumerable<TEntity>> Execute();
}

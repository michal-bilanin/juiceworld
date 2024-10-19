using System.Linq.Expressions;
using Infrastructure.QueryObjects;
using JuiceWorld.Data;
using Microsoft.EntityFrameworkCore;

namespace JuiceWorld.QueryObjects;

public class QueryObject<TEntity>(JuiceWorldDbContext context) : IQueryObject<TEntity>
    where TEntity : class, new()
{
    private IQueryable<TEntity> _query = context.Set<TEntity>();

    public IQueryObject<TEntity> Filter(Expression<Func<TEntity, bool>> filter)
    {
        _query = _query.Where(filter);
        return this;
    }

    public IQueryObject<TEntity> OrderBy<TKey>(Expression<Func<TEntity, TKey>> keySelector, bool isDesc = false)
    {
        _query = isDesc ? _query.OrderByDescending(keySelector) : _query.OrderBy(keySelector);
        return this;
    }

    public IQueryObject<TEntity> Paginate(int pageIndex, int pageSize)
    {
        _query = _query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        return this;
    }

    public async Task<IEnumerable<TEntity>> Execute()
    {
        return await _query.ToListAsync();
    }
}

using System.Linq.Expressions;
using Infrastructure.QueryObjects;
using JuiceWorld.Data;
using JuiceWorld.Entities;
using Microsoft.EntityFrameworkCore;

namespace JuiceWorld.QueryObjects;

public class QueryObject<TEntity>(JuiceWorldDbContext context) : IQueryObject<TEntity>
    where TEntity : BaseEntity
{
    private IQueryable<TEntity> _query = context.Set<TEntity>();
    private bool _pagingEnabled = false;
    private int _pageIndex = 1;
    private int _pageSize = 10;

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
        _pagingEnabled = true;
        _pageIndex = pageIndex;
        _pageSize = pageSize;
        return this;
    }

    public IQueryObject<TEntity> Include(params string[] includes)
    {
        _query = includes.Aggregate(_query, (current, include) => current.Include(include));
        return this;
    }

    public async Task<FilteredResult<TEntity>> ExecuteAsync()
    {
        var totalEntities = await _query.CountAsync();
        var entities = _pagingEnabled
            ? await _query.Skip((_pageIndex - 1) * _pageSize).Take(_pageSize).ToListAsync()
            : await _query.ToListAsync();

        return new FilteredResult<TEntity>
        {
            Entities = entities,
            PageIndex = _pageIndex * _pageSize > totalEntities ? 1 : _pageIndex,
            TotalPages = (int)Math.Ceiling(totalEntities / (double)_pageSize),
        };
    }
}

using System.Linq.Expressions;
using Infrastructure.QueryObjects;
using JuiceWorld.Data;
using JuiceWorld.Entities;
using Microsoft.EntityFrameworkCore;

public class UserQueryObject(JuiceWorldDbContext context) : IQueryObject<User>
{
    private IQueryable<User> _query = context.Set<User>();

    public IQueryObject<User> Filter(Expression<Func<User, bool>> filter)
    {
        _query = _query.Where(filter);
        return this;
    }

    public IQueryObject<User> OrderBy<TKey>(Expression<Func<User, TKey>> keySelector, bool isDesc = false)
    {
        _query = isDesc ? _query.OrderByDescending(keySelector) : _query.OrderBy(keySelector);
        return this;
    }

    public IQueryObject<User> Paginate(int pageIndex, int pageSize)
    {
        _query = _query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        return this;
    }

    public async Task<IEnumerable<User>> ExecuteAsync()
    {
        return await _query.ToListAsync();
    }
}
using System.Linq.Expressions;
using Infrastructure.Repositories;
using JuiceWorld.Data;
using JuiceWorld.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JuiceWorld.Repositories;

public class UserRepository : IRepository<User>
{
    private readonly JuiceWorldDbContext _context;
    private readonly DbSet<User> _dbSet;

    public UserRepository(JuiceWorldDbContext context)
    {
        _context = context;
        _dbSet = context.Set<User>();
    }

    public async Task<User?> CreateAsync(User user, object? userId = null)
    {
        var result = await _dbSet.AddAsync(user);

        if (userId is null)
        {
            await _context.SaveChangesAsync();
        }
        else
        {
            await _context.SaveChangesAsync((int)userId);
        }

        return result.Entity;
    }

    public async Task<User?> GetByIdAsync(object id, params string[] includes)
    {
        var query = includes.Aggregate(_dbSet.AsQueryable(), (current, include) => current.Include(include));
        return await query.FirstOrDefaultAsync(u => u.Id == (int)id);
    }

    public async Task<IEnumerable<User>> GetAllAsync(params string[] includes)
    {
        var query = includes.Aggregate(_dbSet.AsQueryable(), (current, include) => current.Include(include));
        return await query.ToListAsync();
    }

    public async Task<User?> UpdateAsync(User user, object? userId = null)
    {
        var existingUser = await _dbSet.FindAsync(user.Id);
        if (existingUser != null)
        {
            _context.Entry(existingUser).State = EntityState.Detached;
        }

        var result = _dbSet.Update(user);

        if (userId is null)
        {
            await _context.SaveChangesAsync();
        }
        else
        {
            await _context.SaveChangesAsync((int)userId);
        }

        return result.Entity;
    }

    public async Task<bool> DeleteAsync(object id, object? userId = null)
    {
        var user = await _dbSet.FindAsync(id);
        if (user is null)
        {
            return false;
        }

        _dbSet.Remove(user);

        if (userId is null)
        {
            await _context.SaveChangesAsync();
        }
        else
        {
            await _context.SaveChangesAsync((int)userId);
        }

        return true;
    }

    public async Task<int> RemoveAllByConditionAsync(Expression<Func<User, bool>> predicate, object? userId = null)
    {
        var users = await _dbSet.Where(predicate).ToListAsync();
        _dbSet.RemoveRange(users);

        if (userId is null)
        {
            await _context.SaveChangesAsync();
        }
        else
        {
            await _context.SaveChangesAsync((int)userId);
        }

        return users.Count;
    }
}


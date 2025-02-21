using Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Data.Repositories;

public abstract class BaseRepository<TEntity>(DataContext context) where TEntity : class
{
    protected readonly DataContext _context = context;
    protected readonly DbSet<TEntity> _db = context.Set<TEntity>();


    public async Task AddAsync(TEntity entity)
    {
        await _db.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<TEntity>> GetAsync(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
    {
        IQueryable<TEntity> query = _db;

        if (include != null)
        {
            query = include(query);
        }

        return await query.ToListAsync();
    }

    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
    {
        IQueryable<TEntity> query = _db;

        if (include != null)
        {
            query = include(query);
        }

        return await query.FirstOrDefaultAsync(expression);
    }

    public async Task UpdateAsync(TEntity entity)
    {
        _db.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveAsync(TEntity entity)
    {
        _db.Remove(entity);
        await _context.SaveChangesAsync();
    }

}

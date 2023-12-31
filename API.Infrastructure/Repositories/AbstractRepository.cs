using System.Linq.Expressions;
using API.Domain.Contracts;
using API.Domain.Repositories;
using API.Infrastructure.Database;
using API.Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;

namespace API.Infrastructure.Repositories;

public class AbstractRepository<TEntity, TEntityKey>(AppDbContext dbContext) : IRepository<TEntity, TEntityKey>
    where TEntity : class, IEntity<TEntityKey>
{
    private readonly DbSet<TEntity> _entities = dbContext.Set<TEntity>();

    /// <inheritdoc />
    public async Task AddAsync(TEntity entity)
    {
        await this._entities.AddAsync(entity);
    }

    /// <inheritdoc />
    public async Task UpdateAsync(TEntity entity)
    {
        await Task.Run(() => { this._entities.Update(entity); });
    }

    /// <inheritdoc />
    public async Task<TEntity?> GetByIdAsync(TEntityKey id, IEnumerable<string>? entitiesToInclude = null)
    {
        return await this._entities.Include(entitiesToInclude)
            .FirstOrDefaultAsync(e => e.Id != null && e.Id.Equals(id));
    }

    /// <inheritdoc />
    public async Task<IEnumerable<TEntity>> GetAllAsync(
        IEnumerable<Expression<Func<TEntity, bool>>>? predicates = null,
        IEnumerable<string>? entitiesToInclude = null
    )
    {
        return await this._entities
            .Filter(predicates)
            .Include(entitiesToInclude)
            .ToListAsync();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<TEntity>> GetAllAsync(
        int skip,
        int limit,
        IEnumerable<Expression<Func<TEntity, bool>>>? predicates = null,
        IEnumerable<string>? entitiesToInclude = null
    )
    {
        return await this._entities
            .Filter(predicates)
            .Skip(skip)
            .Take(limit)
            .Include(entitiesToInclude)
            .ToListAsync();
    }

    /// <inheritdoc />
    public void Delete(TEntity entity)
    {
        this._entities.Remove(entity);
    }

    /// <inheritdoc />
    public async Task DeleteAsync(TEntity entity)
    {
        await Task.Run(() => { this.Delete(entity); });
    }

    /// <inheritdoc />
    public virtual async Task<int> Count(IEnumerable<Expression<Func<TEntity, bool>>>? predicates = null)
    {
        return await this._entities
            .Filter(predicates)
            .CountAsync();
    }

    /// <inheritdoc />
    public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await this._entities
            .AnyAsync(predicate);
    }

    /// <inheritdoc />
    public int SaveChanges()
    {
        return dbContext.SaveChanges();
    }

    /// <inheritdoc />
    public async Task<int> SaveChangesAsync()
    {
        return await dbContext.SaveChangesAsync();
    }
}
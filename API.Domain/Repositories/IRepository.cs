using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace API.Domain.Repositories;

public interface IRepository<TEntity, in TEntityKey> where TEntity : class
{
    /// <summary>
    /// Add an entity asynchronously.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task AddAsync(TEntity entity);

    /// <summary>
    /// Update an entity asynchronously.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task UpdateAsync(TEntity entity);

    /// <summary>
    /// Get an entity by ID asynchronously with optional related entities included.
    /// </summary>
    /// <param name="id">The ID of the entity.</param>
    /// <param name="entitiesToInclude">The related entities to include.</param>
    /// <returns>A task that represents the asynchronous operation and contains the entity if found, or null otherwise.</returns>
    public Task<TEntity?> GetByIdAsync(TEntityKey id, IEnumerable<string>? entitiesToInclude = null);

    /// <summary>
    /// Get all entities asynchronously, optionally filtering and including related entities.
    /// </summary>
    /// <param name="predicates">The filters to apply.</param>
    /// <param name="entitiesToInclude">The related entities to include.</param>
    /// <returns>A task that represents the asynchronous operation and contains the entity if found.</returns>
    public Task<IEnumerable<TEntity>> GetAllAsync(IEnumerable<Expression<Func<TEntity, bool>>>? predicates = null, IEnumerable<string>? entitiesToInclude = null);

    /// <summary>
    /// Get a limited set of entities asynchronously, optionally skipping some, filtering, and including related entities.
    /// </summary>
    /// <param name="skip">The number of entities to skip.</param>
    /// <param name="limit">The maximum number of entities to return.</param>
    /// <param name="predicates">The filters to apply.</param>
    /// <param name="entitiesToInclude">The related entities to include.</param>
    /// <returns>A task that represents the asynchronous operation and contains the entities if found.</returns>
    public Task<IEnumerable<TEntity>> GetAllAsync(int skip, int limit, IEnumerable<Expression<Func<TEntity, bool>>>? predicates = null, IEnumerable<string>? entitiesToInclude = null);

    /// <summary>
    /// Delete an entity.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    public void Delete(TEntity entity);

    /// <summary>
    /// Delete an entity asynchronously.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task DeleteAsync(TEntity entity);

    /// <summary>
    /// Count the number of entities that satisfy the specified predicates.
    /// </summary>
    /// <param name="predicates">The predicates that the entities must satisfy.</param>
    /// <returns>A task that represents the asynchronous operation and contains the count of entities.</returns>
    public Task<int> Count(IEnumerable<Expression<Func<TEntity, bool>>>? predicates = null);

    /// <summary>
    /// Determine whether an entity that satisfies the specified predicate exists.
    /// </summary>
    /// <param name="predicate">The predicate that the entity must satisfy.</param>
    /// <returns>A task that represents the asynchronous operation and contains a value that indicates whether such an entity exists.</returns>
    public Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Saves all changes made in this context to the underlying database.
    /// </summary>
    /// <returns>
    /// The number of state entries written to the underlying database.
    /// </returns>
    /// <exception cref="DbUpdateException">
    /// Thrown if any error occurs when saving changes to the database.
    /// </exception>
    /// <exception cref="DbUpdateConcurrencyException">
    /// Thrown if a concurrency violation is encountered while saving to the database.
    /// </exception>
    /// <exception cref="NotSupportedException">
    /// Thrown if the context has been disposed.
    /// </exception>
    public int SaveChanges();

    /// <summary>
    /// Asynchronously saves all changes made in this context to the underlying database.
    /// </summary>
    /// <returns>
    /// A <see cref="Task{int}"/> representing the asynchronous operation, 
    /// the result of the task will be the number of state entries written to the underlying database.
    /// </returns>
    /// <exception cref="DbUpdateException">
    /// Thrown if any error occurs when saving changes to the database.
    /// </exception>
    /// <exception cref="DbUpdateConcurrencyException">
    /// Thrown if a concurrency violation is encountered while saving to the database.
    /// </exception>
    /// <exception cref="NotSupportedException">
    /// Thrown if the context has been disposed.
    /// </exception>
    public Task<int> SaveChangesAsync();
}
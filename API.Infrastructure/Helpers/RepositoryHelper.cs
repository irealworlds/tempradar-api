using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace API.Infrastructure.Helpers;

/// <summary>
///     Helper declarations for repositories.
/// </summary>
public static class RepositoryHelper
{
    /// <summary>
    ///     Includes related entities to the query.
    /// </summary>
    /// <typeparam name="TEntity">The type of the query's entities.</typeparam>
    /// <param name="source">The source query.</param>
    /// <param name="entitiesToInclude">The related entities to include.</param>
    /// <returns>The query with the related entities included.</returns>
    public static IQueryable<TEntity> Include<TEntity>(this IQueryable<TEntity> source,
        IEnumerable<string>? entitiesToInclude) where TEntity : class
    {
        var entitiesList = entitiesToInclude?.ToList();

        if (entitiesList == null || entitiesList.Count == 0)
            return source;

        return entitiesList.Aggregate(source, (current, entity) => current.Include(entity));
    }

    /// <summary>
    ///     Applies a list of predicates to the query.
    /// </summary>
    /// <typeparam name="TEntity">The type of the query's entities.</typeparam>
    /// <param name="source">The source query.</param>
    /// <param name="predicates">The list of predicates.</param>
    /// <returns>The query with the predicates applied.</returns>
    public static IQueryable<TEntity> Filter<TEntity>(this IQueryable<TEntity> source,
        IEnumerable<Expression<Func<TEntity, bool>>>? predicates) where TEntity : class
    {
        var predicatesList = predicates?.ToList();

        if (predicatesList == null || predicatesList.Count == 0)
            return source;

        return predicatesList.Aggregate(source, (current, predicate) => current.Where(predicate));
    }
}
namespace BudgetBuddy.Domain.Abstractions.Repository.Core;

/// <summary>
/// Defines a repository interface for performing CRUD operations on entities.
/// </summary>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
public interface IRepository<TEntity> where TEntity : class
{
    /// <summary>
    /// Retrieves an entity by its identifiers.
    /// </summary>
    /// <param name="identifiers">The identifiers of the entity.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the entity.</returns>
    Task<TEntity> GetByIdentifiersAsync(params object[] identifiers);

    /// <summary>
    /// Adds a new entity to the repository.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the added entity.</returns>
    Task<TEntity> AddAsync(TEntity entity);

    /// <summary>
    /// Updates an existing entity in the repository.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <param name="identifiers">The identifiers of the entity.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the updated entity.</returns>
    Task<TEntity> Update(TEntity entity, params object[] identifiers);

    /// <summary>
    /// Deletes an entity from the repository by its identifiers.
    /// </summary>
    /// <param name="identifiers">The identifiers of the entity.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the deleted entity.</returns>
    Task<TEntity> Delete(params object[] identifiers);
}

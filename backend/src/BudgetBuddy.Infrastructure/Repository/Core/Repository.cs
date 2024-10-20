using BudgetBuddy.Domain.Abstractions.Repository.Core;
using BudgetBuddy.Domain.Abstractions.Repository.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BudgetBuddy.Infrastructure.Repository.Core;

/// <summary>
/// Abstract base class for a repository that provides basic CRUD operations for entities.
/// </summary>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
/// <param name="context">The DbContext instance.</param>
public abstract class Repository<TEntity>(DbContext context) : IRepository<TEntity> where TEntity : class
{
    private readonly DbContext _context = context;

    /// <summary>
    /// Adds a new entity to the repository asynchronously.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <returns>The added entity.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the entity is null.</exception>
    /// <exception cref="DuplicatedEntityException">Thrown when the entity already exists.</exception>
    /// <exception cref="RepositoryException">Thrown when an error occurs while adding the entity.</exception>
    public virtual async Task<TEntity> AddAsync(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));

        var entityExists = await _context.Set<TEntity>()
                                         .AsNoTracking()
                                         .AnyAsync(e => e.Equals(entity));

        if (entityExists)
        {
            throw new DuplicatedEntityException(typeof(TEntity), GetEntityKey(entity));
        }

        try
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }
        catch (InvalidOperationException ex)
        {
            throw new RepositoryException($"An error has occurred while adding an entity of type {typeof(TEntity).Name}.\n", ex);
        }

        return entity;
    }

    /// <summary>
    /// Deletes an entity from the repository asynchronously.
    /// </summary>
    /// <param name="identifiers">The identifiers of the entity to delete.</param>
    /// <returns>The deleted entity.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the identifiers are null.</exception>
    /// <exception cref="EntityNotFoundException">Thrown when the entity is not found.</exception>
    public virtual async Task<TEntity> Delete(params object[] identifiers)
    {
        ArgumentNullException.ThrowIfNull(identifiers, nameof(identifiers));

        var entity = await _context.Set<TEntity>().FindAsync(identifiers)
                     ?? throw new EntityNotFoundException(typeof(TEntity), identifiers);

        _context.Set<TEntity>().Remove(entity);

        return entity;
    }

    /// <summary>
    /// Retrieves an entity by its identifiers asynchronously.
    /// </summary>
    /// <param name="identifiers">The identifiers of the entity to retrieve.</param>
    /// <returns>The retrieved entity.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the identifiers are null.</exception>
    /// <exception cref="EntityNotFoundException">Thrown when the entity is not found.</exception>
    public virtual async Task<TEntity> GetByIdentifiersAsync(params object[] identifiers)
    {
        ArgumentNullException.ThrowIfNull(identifiers);

        var entity = await _context.Set<TEntity>().FindAsync(identifiers)
                     ?? throw new EntityNotFoundException(typeof(TEntity), identifiers);

        return entity;
    }

    /// <summary>
    /// Updates an existing entity in the repository asynchronously.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <param name="identifiers">The identifiers of the entity to update.</param>
    /// <returns>The updated entity.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the entity or identifiers are null.</exception>
    /// <exception cref="EntityNotFoundException">Thrown when the entity is not found.</exception>
    public virtual async Task<TEntity> Update(TEntity entity, params object[] identifiers)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));
        ArgumentNullException.ThrowIfNull(identifiers, nameof(identifiers));

        var oldEntity = await _context.Set<TEntity>()
                                      .FindAsync(identifiers)
                                       ?? throw new EntityNotFoundException(typeof(TEntity), identifiers);

        _context.Entry(oldEntity).CurrentValues.SetValues(entity);

        return entity;
    }

    /// <summary>
    /// Retrieves the primary key value of an entity.
    /// </summary>
    /// <param name="entity">The entity to get the key from.</param>
    /// <returns>The primary key value of the entity.</returns>
    private object GetEntityKey(TEntity entity)
    {
        var keyName = _context.Model.FindEntityType(typeof(TEntity))!
                                    .FindPrimaryKey()!.Properties
                                    .Select(static p => p.Name)
                                    .Single();

        return entity.GetType()
                     .GetProperty(keyName)!
                     .GetValue(entity)!;
    }
}

using BudgetBuddy.Domain.Abstractions.Repository.Core;
using BudgetBuddy.Domain.Abstractions.Repository.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BudgetBuddy.Infrastructure.Repository.Core;

public abstract class Repository<TEntity>(DbContext context) : IRepository<TEntity> where TEntity : class
{
    private readonly DbContext _context = context;

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
    public virtual async Task<TEntity> Delete(params object[] identifiers)
    {
        ArgumentNullException.ThrowIfNull(identifiers, nameof(identifiers));

        var entity = await _context.Set<TEntity>().FindAsync(identifiers)
                     ?? throw new EntityNotFoundException(typeof(TEntity), identifiers);

        _context.Set<TEntity>().Remove(entity);

        return entity;
    }
    public virtual async Task<TEntity> GetByIdentifiersAsync(params object[] identifiers)
    {
        ArgumentNullException.ThrowIfNull(identifiers);

        var entity = await _context.Set<TEntity>().FindAsync(identifiers)
                     ?? throw new EntityNotFoundException(typeof(TEntity), identifiers);

        return entity;
    }

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

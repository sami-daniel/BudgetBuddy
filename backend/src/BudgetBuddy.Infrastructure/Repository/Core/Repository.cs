using BudgetBuddy.Domain.Abstractions.Repository.Core;
using BudgetBuddy.Domain.Abstractions.Repository.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BudgetBuddy.Infrastructure.Repository.Core;

public abstract class Repository<TEntity>(DbContext context) : IRepository<TEntity> where TEntity : class
{
    private readonly DbContext _context = context;

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        var entityExists = await _context.Set<TEntity>().AnyAsync(e => e.Equals(entity));

        if (entityExists)
        {
            throw new DuplicatedEntityException(typeof(TEntity), GetEntityKey(entity));
        }
        
        try 
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }
        catch
        {
            throw new RepositoryException();
        }

        return entity;
    }
    public Task<TEntity> DeleteAsync(params object[] identifiers) => throw new NotImplementedException();
    public Task<TEntity> GetByIdentifiersAsync(params object[] identifiers) => throw new NotImplementedException();
    public Task<TEntity> UpdateAsync(TEntity entity, params object[] identifiers) => throw new NotImplementedException();

    private object? GetEntityKey(TEntity entity)
    {
        var keyNames = _context.Model.FindEntityType(typeof(TEntity))?.FindPrimaryKey()?.Properties
            .Select(x => x.Name).ToList();

        if (keyNames == null || keyNames.Count == 0)
        {
            return null;
        }

        var keyValues = new List<object>();
        foreach (var keyName in keyNames)
        {
            var keyValue = typeof(TEntity).GetProperty(keyName)?.GetValue(entity, null);
            if (keyValue != null)
            {
                keyValues.Add(keyValue);
            }
        }

        return keyValues.Count == 1 ? keyValues[0] : Array.Empty<object>();
    }
}

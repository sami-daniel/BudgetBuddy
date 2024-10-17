using BudgetBuddy.Domain.Abstractions.Repository.Core;
using Microsoft.EntityFrameworkCore;

namespace BudgetBuddy.Infrastructure.Repository.Core;

public abstract class Repository<TEntity>(DbContext context) : IRepository<TEntity> where TEntity : class
{
    private readonly DbContext _context = context;

    public Task<TEntity> AddAsync(TEntity entity) => throw new NotImplementedException();
    public Task<TEntity> DeleteAsync(params object[] identifiers) => throw new NotImplementedException();
    public Task<TEntity> GetByIdentifiersAsync(params object[] identifiers) => throw new NotImplementedException();
    public Task<TEntity> UpdateAsync(TEntity entity, params object[] identifiers) => throw new NotImplementedException();
}

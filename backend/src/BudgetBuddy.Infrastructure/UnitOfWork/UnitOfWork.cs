using BudgetBuddy.Domain.Abstractions.UnitOfWork;

namespace BudgetBuddy.Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    public Task BeginTransactionAsync() => throw new NotImplementedException();
    public Task CommitTransactionAsync() => throw new NotImplementedException();
    public Task<int> ExecuteAsync() => throw new NotImplementedException();
}

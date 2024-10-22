using BudgetBuddy.Domain.Abstractions.Repository.Specialized;
using BudgetBuddy.Domain.Abstractions.UnitOfWork;
using BudgetBuddy.Domain.Abstractions.UnitOfWork.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BudgetBuddy.Infrastructure.UnitOfWork;

public class UnitOfWork(DbContext context, IUserRepository userRepository) : IUnitOfWork
{
    private readonly DbContext _context = context;

    public IUserRepository UserRepository { get; } = userRepository;

    public async Task BeginTransactionAsync()
    {
        try
        {
            await _context.Database.BeginTransactionAsync();
        }
        catch (Exception ex)
        {
            throw new TransactionException("An error occurred while starting the transaction.", ex);
        }
    }

    public async Task CommitTransactionAsync()
    {
        var currentTransaction = _context.Database.CurrentTransaction
                                 ?? throw new TransactionException("Transaction has not been started.");

        try
        {
            await currentTransaction.CommitAsync();
        }
        catch (Exception ex)
        {
            throw new TransactionException("An error occurred while committing the transaction.", ex);
        }
    }

    public async Task<int> ExecuteAsync()
    {
        try
        {
            return await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new ExecuteException("An error occurred while saving changes.", ex);
        }
    }
}

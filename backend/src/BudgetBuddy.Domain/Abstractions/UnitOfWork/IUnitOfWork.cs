using BudgetBuddy.Domain.Abstractions.Repository.Specialized;

namespace BudgetBuddy.Domain.Abstractions.UnitOfWork;

/// <summary>
/// Represents a unit of work that encapsulates a series of data access operations and manages transactions,
/// acting like a single acess point to Data Acess.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Gets the user repository.
    /// </summary>
    IUserRepository UserRepository { get; }

    /// <summary>
    /// Executes the unit of work and returns the number of affected rows.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.
    /// The task result contains the number of affected rows.</returns>
    Task<int> ExecuteAsync();

    /// <summary>
    /// Begins a new transaction asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task BeginTransactionAsync();

    /// <summary>
    /// Commits the current transaction asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task CommitTransactionAsync();
}


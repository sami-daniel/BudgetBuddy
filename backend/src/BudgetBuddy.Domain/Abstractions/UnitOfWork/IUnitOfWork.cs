namespace BudgetBuddy.Domain.Abstractions.UnitOfWork;

/// <summary>
/// Represents a unit of work that encapsulates a series of operations
/// that should be executed as a single transaction.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Executes the unit of work and returns the number of affected rows.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.
    /// The task result contains the number of affected rows.</returns>
    Task<int> Execute();

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


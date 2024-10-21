namespace BudgetBuddy.Domain.Abstractions.UnitOfWork.Exceptions;

/// <summary>
/// Represents errors that occur during transaction operations within a unit of work.
/// </summary>
public class TransactionException : UnitOfWorkException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TransactionException"/> class.
    /// </summary>
    public TransactionException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TransactionException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public TransactionException(string message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TransactionException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public TransactionException(string message, Exception innerException) : base(message, innerException)
    {
    }
}

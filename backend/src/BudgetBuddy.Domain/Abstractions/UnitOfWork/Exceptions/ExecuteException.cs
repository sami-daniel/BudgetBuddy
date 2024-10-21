namespace BudgetBuddy.Domain.Abstractions.UnitOfWork.Exceptions;

/// <summary>
/// Represents errors that occur during the execution of stacked operations.
/// </summary>
public class ExecuteException : UnitOfWorkException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ExecuteException"/> class.
    /// </summary>
    public ExecuteException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ExecuteException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public ExecuteException(string message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ExecuteException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public ExecuteException(string message, Exception innerException) : base(message, innerException)
    {
    }
}

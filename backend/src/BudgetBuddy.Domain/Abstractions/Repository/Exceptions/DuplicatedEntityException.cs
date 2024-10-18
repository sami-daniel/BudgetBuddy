namespace BudgetBuddy.Domain.Abstractions.Repository.Exceptions;

/// <summary>
/// Exception thrown when an attempt is made to add an entity that already exists in the repository.
/// </summary>
public class DuplicatedEntityException : RepositoryException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DuplicatedEntityException"/> class with a default error message.
    /// </summary>
    public DuplicatedEntityException() : base("The entity already exists.")
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DuplicatedEntityException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    public DuplicatedEntityException(string? message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DuplicatedEntityException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
    public DuplicatedEntityException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DuplicatedEntityException"/> class with a specified entity type and key.
    /// </summary>
    /// <param name="entityType">The type of the entity that already exists.</param>
    /// <param name="entityKey">The key of the entity that already exists.</param>
    public DuplicatedEntityException(Type entityType, object? entityKey) : base($"The entity of type {entityType.Name} with key {entityKey} already exists.")
    {
    }
}
namespace BudgetBuddy.Domain.Abstractions.Repository.Exceptions;

/// <summary>
/// Exception thrown when an entity is not found in the repository.
/// </summary>
public class EntityNotFoundException : RepositoryException
{
    public EntityNotFoundException() : base("The entity was not found.")
    {
    }

    public EntityNotFoundException(string? message) : base(message)
    {
    }

    public EntityNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    public EntityNotFoundException(Type entityType, object? entityKey) : base($"The entity of type {entityType.Name} with key {entityKey} was not found.")
    {
    }
}
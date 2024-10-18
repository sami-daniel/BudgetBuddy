namespace BudgetBuddy.Domain.Abstractions.Repository.Exceptions;

public class DuplicatedEntityException : RepositoryException
{
    public DuplicatedEntityException() : base("The entity already exists.")
    {
    }

    public DuplicatedEntityException(string? message) : base(message)
    {
    }

    public DuplicatedEntityException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    public DuplicatedEntityException(Type entityType, object? entityKey) : base($"The entity of type {entityType.Name} with key {entityKey} already exists.")
    {
    }
}
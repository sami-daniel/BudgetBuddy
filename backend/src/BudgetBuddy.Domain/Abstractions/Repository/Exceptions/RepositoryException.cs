namespace BudgetBuddy.Domain.Abstractions.Repository.Exceptions;

/// <summary>
/// Represents errors that occur during repository operations.
/// </summary>
public class RepositoryException : Exception
{
    private const string DEFAULT_MESSAGE = "An error occurred while accessing the repository.";

    public RepositoryException() : base(DEFAULT_MESSAGE)
    {
    }
    public RepositoryException(string? message) : base(message ?? DEFAULT_MESSAGE)
    {
    }

    public RepositoryException(string? message, Exception? innerException) : base(message ?? DEFAULT_MESSAGE, innerException)
    {
    }
}
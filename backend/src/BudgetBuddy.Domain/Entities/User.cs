namespace BudgetBuddy.Domain.Entities;

/// <summary>
/// Represents a user entity in the BudgetBuddy application.
/// </summary>
public class User
{
    /// <summary>
    /// Gets or sets the unique identifier for the user.
    /// </summary>
    public virtual required Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the username of the user.
    /// </summary>
    public virtual required string Username { get; set; }

    /// <summary>
    /// Gets or sets the password of the user.
    /// </summary>
    public virtual required string UserPassword { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the user was created.
    /// </summary>
    public virtual required DateTime CreatedAt { get; set; }
}

namespace BudgetBuddy.Domain.Entities;

/// <summary>
/// Represents a user entity in the BudgetBuddy application.
/// </summary>
public class User
{
    /// <summary>
    /// Gets or sets the unique identifier for the user.
    /// </summary>
    public virtual Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the username of the user.
    /// </summary>
    public virtual string Username { get; set; } = null!;

    /// <summary>
    /// Gets or sets the password of the user.
    /// </summary>
    public virtual string UserPassword { get; set; } = null!;

    /// <summary>
    /// Gets or sets the date and time when the user was created.
    /// </summary>
    public virtual DateTime CreatedAt { get; set; }
}

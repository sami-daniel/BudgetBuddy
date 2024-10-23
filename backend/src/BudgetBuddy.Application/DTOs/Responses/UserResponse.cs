namespace BudgetBuddy.Application.DTOs.Responses;

/// <summary>
/// Represents a response containing information about a user in the BudgetBuddy application.
/// </summary>
public class UserResponse
{
    /// <summary>
    /// Gets or sets the unique identifier for the user.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the username of the user.
    /// </summary>
    public string Username { get; set; } = null!;

    /// <summary>
    /// Gets or sets the date and time when the user was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    public override string ToString()
    {
        return $"UserId: {UserId}, Username: {Username}, CreatedAt: {CreatedAt}";
    }

    public override bool Equals(object? obj)
    {
        if (obj is not UserResponse userResponse)
        {
            return false;
        }

        return UserId == userResponse.UserId
               && Username == userResponse.Username
               && CreatedAt == userResponse.CreatedAt;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(UserId, Username, CreatedAt);
    }
}

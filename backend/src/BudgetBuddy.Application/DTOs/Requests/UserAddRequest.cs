namespace BudgetBuddy.Application.DTOs.Requests;

/// <summary>
/// Represents a request to add a new user to the BudgetBuddy application.
/// </summary>
public class UserAddRequest
{
    /// <summary>
    /// Gets or sets the username of the user.
    /// </summary>
    public string Username { get; set; } = null!;

    /// <summary>
    /// Gets or sets the password of the user.
    /// </summary>
    public string UserPassword { get; set; } = null!;
}
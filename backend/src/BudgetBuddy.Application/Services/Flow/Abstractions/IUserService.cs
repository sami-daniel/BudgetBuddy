using BudgetBuddy.Application.DTOs.Requests;
using BudgetBuddy.Application.DTOs.Responses;

namespace BudgetBuddy.Application.Services.Flow.Abstractions;

/// <summary>
/// Interface for a service that provides user-related operations.
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Registers a new user.
    /// </summary>
    /// <param name="userAddRequest">The user to register.</param>
    /// <returns>The registered user.</returns>
    Task<UserResponse> RegisterAsync(UserAddRequest userAddRequest);

    /// <summary>
    /// Gets a user by their ID.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    /// <returns>The user with the correspondent ID. Otherwise, returns null.</returns>
    Task<UserResponse?> GetUserByIDAsync(Guid userId);
}

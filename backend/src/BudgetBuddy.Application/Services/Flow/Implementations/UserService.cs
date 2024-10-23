using BudgetBuddy.Application.DTOs.Requests;
using BudgetBuddy.Application.DTOs.Responses;
using BudgetBuddy.Application.Services.Flow.Abstractions;

namespace BudgetBuddy.Application.Services.Flow.Implementations;

public class UserService : IUserService
{
    public Task<UserResponse?> GetUserByIDAsync(Guid userId) => throw new NotImplementedException();
    public Task<UserResponse> RegisterAsync(UserAddRequest userAddRequest) => throw new NotImplementedException();
}

using AutoMapper;
using BudgetBuddy.Application.DTOs.Requests;
using BudgetBuddy.Application.DTOs.Responses;
using BudgetBuddy.Application.Services.Flow.Abstractions;
using BudgetBuddy.Domain.Abstractions.UnitOfWork;
using BudgetBuddy.Domain.Abstractions.Validator;
using BudgetBuddy.Domain.Entities;

namespace BudgetBuddy.Application.Services.Flow.Implementations;

public class UserService(IUnitOfWork unitOfWork, IMapper mapper, IValidatable<User> validatableUser) : IUserService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IValidatable<User> _validatableUser = validatableUser;

    public Task<UserResponse?> GetUserByIDAsync(Guid userId) => throw new NotImplementedException();
    public Task<UserResponse> RegisterUserAsync(UserAddRequest userAddRequest) => throw new NotImplementedException();
}

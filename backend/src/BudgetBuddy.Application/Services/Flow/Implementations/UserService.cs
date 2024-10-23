using AutoMapper;
using BudgetBuddy.Application.DTOs.Requests;
using BudgetBuddy.Application.DTOs.Responses;
using BudgetBuddy.Application.Services.Flow.Abstractions;
using BudgetBuddy.Domain.Abstractions.Repository.Exceptions;
using BudgetBuddy.Domain.Abstractions.Services.Exceptions;
using BudgetBuddy.Domain.Abstractions.UnitOfWork;
using BudgetBuddy.Domain.Abstractions.Validator;
using BudgetBuddy.Domain.Entities;
using ApplicationException = BudgetBuddy.Domain.Abstractions.Services.Exceptions.ApplicationException;

namespace BudgetBuddy.Application.Services.Flow.Implementations;

public class UserService(IUnitOfWork unitOfWork, IMapper mapper, IValidatable<User> validatableUser) : IUserService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IValidatable<User> _validatableUser = validatableUser;

    public async Task<UserResponse?> GetUserByIDAsync(Guid userId)
    {
        if (userId == Guid.Empty)
        {
            throw new ArgumentException("The user ID cannot be empty", nameof(userId));
        }

        try
        {
            var newlyAddedUser = await _unitOfWork.UserRepository.GetByIdentifiersAsync(userId);
            return _mapper.Map<UserResponse>(newlyAddedUser);
        }
        catch (EntityNotFoundException)
        {
            return null;
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while trying to get the user by ID", ex);
        }
    }

    public async Task<UserResponse> RegisterUserAsync(UserAddRequest userAddRequest)
    {
        ArgumentNullException.ThrowIfNull(userAddRequest, nameof(userAddRequest));

        try
        {
            var newUser = _mapper.Map<User>(userAddRequest);
            var validationResult = await _validatableUser.ValidateAsync(newUser);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            try
            {
                var newlyAddedUser = await _unitOfWork.UserRepository.AddAsync(newUser);
                await _unitOfWork.ExecuteAsync();
                await _unitOfWork.CommitTransactionAsync();
                return _mapper.Map<UserResponse>(newlyAddedUser);
            }
            catch (DuplicatedEntityException ex)
            {
                throw new ApplicationException("The user already exists", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while trying to register the user", ex);
            }
        }
        catch (Exception ex)
        when (ex is not ValidationException)
        {
            throw new ApplicationException("An error occurred while trying to register the user", ex);
        }
    }
}

using BudgetBuddy.Domain.Abstractions.Validator;
using BudgetBuddy.Domain.Entities;
using IUserFluentValidator = FluentValidation.IValidator<BudgetBuddy.Domain.Entities.User>;

namespace BudgetBuddy.Application.Validators.Implementations;

public class UserValidator(IUserFluentValidator userFluentValidator) : IValidator<User>
{
    private readonly IUserFluentValidator _userFluentValidator = userFluentValidator;

    public async Task<ValidationState> ValidateAsync(User entity)
    {
        var validationResult = await _userFluentValidator.ValidateAsync(entity);
        var validationErrors = validationResult.Errors;
        return new ValidationState(validationResult.IsValid, validationErrors.Select(e => e.ErrorMessage));
    }
}

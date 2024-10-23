using BudgetBuddy.Domain.Abstractions.Validator;
using BudgetBuddy.Domain.Entities;
using IUserFluentValidator = FluentValidation.IValidator<BudgetBuddy.Domain.Entities.User>;

namespace BudgetBuddy.Application.Validators.Implementations;

/// <summary>
/// Validates a <see cref="User"/> entity using the provided <see cref="IUserFluentValidator"/>.
/// </summary>
/// <param name="userFluentValidator">An instance of <see cref="IUserFluentValidator"/> used to perform the validation.</param>
public class ValidatableUser(IUserFluentValidator userFluentValidator) : IValidatable<User>
{
    private readonly IUserFluentValidator _userFluentValidator = userFluentValidator;

    /// <summary>
    /// Asynchronously validates the specified <see cref="User"/> entity.
    /// </summary>
    /// <param name="entity">The <see cref="User"/> entity to validate.</param>
    /// <returns>A <see cref="ValidationState"/> object containing the validation result and any validation errors.</returns>
    public async Task<ValidationState> ValidateAsync(User entity)
    {
        if (entity == null)
        {
            return new ValidationState(false, ["User is required."]);
        }

        var validationResult = await _userFluentValidator.ValidateAsync(entity);
        var validationErrors = validationResult.Errors;
        return new ValidationState(validationResult.IsValid, validationErrors.Select(e => e.ErrorMessage));
    }
}

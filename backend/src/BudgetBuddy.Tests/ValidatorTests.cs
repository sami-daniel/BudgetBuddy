using BudgetBuddy.Application.Validators.FluentValidations;
using BudgetBuddy.Application.Validators.Implementations;
using BudgetBuddy.Domain.Entities;

namespace BudgetBuddy.Tests;

public class ValidatorTests
{
    private readonly UserValidator _userValidator;

    public ValidatorTests()
    {
        _userValidator = new UserValidator(new UserFluentValidator());
    }

    [Fact]
    public async Task UserValidator_ValidateAsync_ValidUser_ReturnsSucessfulValidation()
    {
        // Arrange
        var user = new User
        {
            Username = "foo",
            UserPassword = "Password1!"
        };

        // Act
        var validationState = await _userValidator.ValidateAsync(user);

        // Assert
        Assert.True(validationState.IsValid);

    }
}

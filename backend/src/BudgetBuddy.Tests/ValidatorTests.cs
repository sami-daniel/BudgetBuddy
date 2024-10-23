using BudgetBuddy.Application.Validators.FluentValidations;
using BudgetBuddy.Application.Validators.Implementations;
using BudgetBuddy.Domain.Entities;
using FakeItEasy;
using Xunit.Abstractions;

namespace BudgetBuddy.Tests;

public class ValidatorTests(ITestOutputHelper outputHelper)
{
    private readonly UserValidator _userValidator = new UserValidator(new UserFluentValidator());
    private readonly ITestOutputHelper _outputWriter = outputHelper;

    #region UserValidator

    [Fact]
    public async Task UserValidator_ValidateAsync_ValidUser_ReturnsSucessfulValidation()
    {
        // Arrange
        var user = new User
        {
            Username = "foo",
            UserPassword = "Password1!"
        };
        _outputWriter.WriteLine(user.ToString());

        // Act
        var validationState = await _userValidator.ValidateAsync(user);

        // Assert
        Assert.True(validationState.IsValid);
    }

    [Fact]
    public async Task UserValidator_ValidateAsync_InvalidUser_ReturnsFailedValidation()
    {
        // Arrange
        var user = new User
        {
            Username = "f",
            UserPassword = "password"
        };
        _outputWriter.WriteLine(user.ToString());

        // Act
        var validationState = await _userValidator.ValidateAsync(user);

        // Assert
        Assert.False(validationState.IsValid);
    }

    [Fact]
    public async Task UserValidator_ValidateAsync_NullUser_ReturnsFailedValidation()
    {
        // Arrange
        User? user = null!;

        // Act
        var validationState = await _userValidator.ValidateAsync(user);

        // Assert
        Assert.False(validationState.IsValid);
    }

    [Fact]
    public async Task UserValidator_ValidateAsync_EmptyUsername_ReturnsFailedValidation()
    {
        // Arrange
        var user = new User
        {
            Username = string.Empty,
            UserPassword = "foo"
        };
        _outputWriter.WriteLine(user.ToString());

        // Act
        var validationState = await _userValidator.ValidateAsync(user);

        // Assert
        Assert.False(validationState.IsValid);
    }

    [Fact]
    public async Task UserValidator_ValidateAsync_EmptyPassword_ReturnsFailedValidation()
    {
        // Arrange
        var user = new User
        {
            Username = "foo",
            UserPassword = string.Empty
        };
        _outputWriter.WriteLine(user.ToString());

        // Act
        var validationState = await _userValidator.ValidateAsync(user);

        // Assert
        Assert.False(validationState.IsValid);
    }

    [Fact]
    public async Task UserValidator_ValidateAsync_NullUsername_ReturnsFailedValidation()
    {
        // Arrange
        var user = new User
        {
            Username = null!,
            UserPassword = "foo"
        };
        _outputWriter.WriteLine(user.ToString());

        // Act
        var validationState = await _userValidator.ValidateAsync(user);

        // Assert
        Assert.False(validationState.IsValid);
    }

    [Fact]
    public async Task UserValidator_ValidateAsync_NullPassword_ReturnsFailedValidation()
    {
        // Arrange
        var user = new User
        {
            Username = "foo",
            UserPassword = null!
        };
        _outputWriter.WriteLine(user.ToString());

        // Act
        var validationState = await _userValidator.ValidateAsync(user);

        // Assert
        Assert.False(validationState.IsValid);
    }

    [Fact]
    public async Task UserValidator_ValidateAsync_UsernameTooShort_ReturnsFailedValidation()
    {
        // Arrange
        var user = new User
        {
            Username = "f",
            UserPassword = "foo"
        };
        _outputWriter.WriteLine(user.ToString());

        // Act
        var validationState = await _userValidator.ValidateAsync(user);

        // Assert
        Assert.False(validationState.IsValid);
    }

    [Fact]
    public async Task UserValidator_ValidateAsync_PasswordTooShort_ReturnsFailedValidation()
    {
        // Arrange
        var user = new User
        {
            Username = "foo",
            UserPassword = "foo"
        };
        _outputWriter.WriteLine(user.ToString());

        // Act
        var validationState = await _userValidator.ValidateAsync(user);

        // Assert
        Assert.False(validationState.IsValid);
    }

    [Fact]
    public async Task UserValidator_ValidateAsync_PasswordNoLetter_ReturnsFailedValidation()
    {
        // Arrange
        var user = new User
        {
            Username = "foo",
            UserPassword = "12345678!"
        };
        _outputWriter.WriteLine(user.ToString());

        // Act
        var validationState = await _userValidator.ValidateAsync(user);

        // Assert
        Assert.False(validationState.IsValid);
    }

    [Fact]
    public async Task UserValidator_ValidateAsync_PasswordNoNumber_ReturnsFailedValidation()
    {
        // Arrange
        var user = new User
        {
            Username = "foo",
            UserPassword = "Password!"
        };
        _outputWriter.WriteLine(user.ToString());

        // Act
        var validationState = await _userValidator.ValidateAsync(user);

        // Assert
        Assert.False(validationState.IsValid);
    }

    [Fact]
    public async Task UserValidator_ValidateAsync_PasswordNoSpecialCharacter_ReturnsFailedValidation()
    {
        // Arrange
        var user = new User
        {
            Username = "foo",
            UserPassword = "Password1234"
        };
        _outputWriter.WriteLine(user.ToString());

        // Act
        var validationState = await _userValidator.ValidateAsync(user);

        // Assert
        Assert.False(validationState.IsValid);
    }

    #endregion
}

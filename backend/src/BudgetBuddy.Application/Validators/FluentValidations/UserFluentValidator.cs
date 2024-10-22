using BudgetBuddy.Domain.Entities;
using FluentValidation;

namespace BudgetBuddy.Application.Validators.FluentValidations;

public class UserFluentValidator : AbstractValidator<User>
{
    public UserFluentValidator()
    {
        RuleFor(u => u.Username)
            .Cascade(CascadeMode.Continue)
            .NotNull()
            .WithMessage("Username is required.")
            .NotEmpty()
            .WithMessage("Username is required.")
            .MaximumLength(255)
            .WithMessage("Username must be less than 255 characters.")
            .MinimumLength(3)
            .WithMessage("Username must be at least 3 characters.");

        RuleFor(u => u.UserPassword)
            .Cascade(CascadeMode.Continue)
            .NotNull()
            .WithMessage("Password is required.")
            .NotEmpty()
            .WithMessage("Password is required.")
            .MaximumLength(255)
            .WithMessage("Password must be less than 255 characters.")
            .MinimumLength(8)
            .WithMessage("Password must be at least 8 characters.")
            .Matches(@"[A-Za-z]")
            .WithMessage("Password must contain at least one letter.")
            .Matches(@"[0-9]")
            .WithMessage("Password must contain at least one number.")
            .Matches(@"[!@#$%^&*_ ]")
            .WithMessage("Password must contain at least one special character or space.");
    }
}

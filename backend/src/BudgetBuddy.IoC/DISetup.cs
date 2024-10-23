using BudgetBuddy.Application.Profiles;
using BudgetBuddy.Application.Services.Flow.Abstractions;
using BudgetBuddy.Application.Services.Flow.Implementations;
using BudgetBuddy.Application.Validators.FluentValidations;
using BudgetBuddy.Application.Validators.Implementations;
using BudgetBuddy.Domain.Abstractions.Repository.Specialized;
using BudgetBuddy.Domain.Abstractions.UnitOfWork;
using BudgetBuddy.Domain.Abstractions.Validator;
using BudgetBuddy.Domain.Entities;
using BudgetBuddy.Infrastructure.Data.Contexts;
using BudgetBuddy.Infrastructure.Repository.Specialized;
using BudgetBuddy.Infrastructure.UnitOfWork;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BudgetBuddy.IoC;

public class DISetup
{
    public static void Setup(IServiceCollection services)
    {
        // Regitering Data Acess
        services.AddDbContext<ApplicationDbContext>();
        services.AddScoped<DbContext, ApplicationDbContext>(); // Inject the DbContext as the current AppDbContext by default

        // Registering Repositories
        services.AddScoped<IUserRepository, UserRepository>();

        // Registering Unit of Work
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Registering Validators
        services.AddSingleton<IValidator<User>, UserFluentValidator>(); // This is the FluentValidation validator
        services.AddSingleton<IValidatable<User>, ValidatableUser>(); // This is the custom implementation of the
                                                                  // BudgetBuddy.Domain.Abstractions.Validator.IValidator that uses the FluentValidation validator

        // Registering Mapper profiles
        services.AddAutoMapper((configuration) =>
        {
            configuration.AddProfile<UserProfile>();
        });

        // Registering Services
        services.AddScoped<IUserService, UserService>();
    }
}

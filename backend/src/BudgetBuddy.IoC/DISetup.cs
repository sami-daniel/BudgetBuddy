using BudgetBuddy.Application.Profiles;
using BudgetBuddy.Application.Validators.Implementations;
using BudgetBuddy.Domain.Abstractions.Repository.Specialized;
using BudgetBuddy.Domain.Abstractions.UnitOfWork;
using BudgetBuddy.Domain.Abstractions.Validator;
using BudgetBuddy.Domain.Entities;
using BudgetBuddy.Infrastructure.Data.Contexts;
using BudgetBuddy.Infrastructure.Repository.Specialized;
using BudgetBuddy.Infrastructure.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace BudgetBuddy.IoC;

public class DISetup
{
    public static void Setup(IServiceCollection services)
    {
        // Regitering Data Acess
        services.AddDbContext<ApplicationDbContext>();

        // Registering Repositories
        services.AddScoped<IUserRepository, UserRepository>();

        // Registering Unit of Work
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Registering Validators
        services.AddSingleton<IValidator<User>, UserValidator>();

        // Registering Mapper profiles
        services.AddAutoMapper((configuration) =>
        {
            configuration.AddProfile<UserProfile>();
        });

    }
}

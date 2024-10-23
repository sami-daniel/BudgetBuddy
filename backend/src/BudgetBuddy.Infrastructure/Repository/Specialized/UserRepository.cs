using BudgetBuddy.Domain.Abstractions.Repository.Specialized;
using BudgetBuddy.Domain.Entities;
using BudgetBuddy.Infrastructure.Data.Contexts;
using BudgetBuddy.Infrastructure.Repository.Core;

namespace BudgetBuddy.Infrastructure.Repository.Specialized;

/// <summary>
/// The <c>BudgetBuddy.Infrastructure.Repository.Specialized</c> namespace contains specialized repository implementations.
/// </summary>
/// <param name="context">The application database context.</param>
public class UserRepository(ApplicationDbContext context) : Repository<User>(context), IUserRepository
{
}

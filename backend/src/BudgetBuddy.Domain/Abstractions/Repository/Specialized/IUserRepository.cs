using BudgetBuddy.Domain.Abstractions.Repository.Core;
using BudgetBuddy.Domain.Entities;

namespace BudgetBuddy.Domain.Abstractions.Repository.Specialized;

/// <summary>
/// The <c>BudgetBuddy.Domain.Abstractions.Repository.Specialized</c> namespace contains specialized repository interfaces
/// for the BudgetBuddy application domain. These interfaces define the contract for data access operations related to
/// specific entities within the domain, such as users, budgets, and transactions.
/// </summary>
public interface IUserRepository : IRepository<User>
{
}

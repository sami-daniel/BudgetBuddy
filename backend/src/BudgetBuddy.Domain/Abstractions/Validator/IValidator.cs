using BudgetBuddy.Domain.Entities;

namespace BudgetBuddy.Domain.Abstractions.Validator;

/// <summary>
/// Represents a validation process of an <see cref="{TEntity}"/> entity.
/// </summary>
/// <typeparam name="TEntity">The entity to be validated.</typeparam>
public interface IValidator<TEntity> where TEntity : class
{
    Task<ValidationState> ValidateAsync(TEntity entity);
}

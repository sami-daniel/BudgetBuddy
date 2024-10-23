using BudgetBuddy.Domain.Entities;

namespace BudgetBuddy.Domain.Abstractions.Validator;

/// <summary>
/// Represents a validation process of an <see cref="{TEntity}"/> entity.
/// </summary>
/// <typeparam name="TEntity">The entity to be validated.</typeparam>
public interface IValidatable<TEntity> where TEntity : class
{
    /// <summary>
    /// Validates the entity.
    /// </summary>
    /// <param name="entity">The entity to be validated.</param>
    /// <returns>A task that represents the asynchronous operation. The return type is a <see cref="ValidationState"/>
    /// containing the error messages caused by validation process.</returns>
    Task<ValidationState> ValidateAsync(TEntity entity);
}

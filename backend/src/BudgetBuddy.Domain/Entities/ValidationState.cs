namespace BudgetBuddy.Domain.Entities;

/// <summary>
/// Represents the actual state of an validation.
/// </summary>
public class ValidationState(bool isSucessfulValidation, IDictionary<string, string[]> errors)
{

    /// <summary>
    /// Gets the actual validation state
    /// </summary>
    public virtual bool IsValid { get; } = isSucessfulValidation;

    /// <summary>
    /// Gets the error messages with the property key provided by the validation process.
    /// </summary>
    public virtual IDictionary<string, string[]> Errors { get; } = errors;
}

namespace BudgetBuddy.Domain.Entities;

/// <summary>
/// Represents the actual state of an validation.
/// </summary>
public class ValidationState(bool isSucessfulValidation, IEnumerable<string> errorMessages)
{

    /// <summary>
    /// Gets the actual validation state
    /// </summary>
    public virtual bool IsSucessfulValidation { get; } = isSucessfulValidation;

    /// <summary>
    /// Gets the error messages provided by the validation process.
    /// </summary>
    public virtual IEnumerable<string> ErrorMessages { get; } = errorMessages;
}

using System.Collections.Generic;
using FluentValidation.Results;
using SimpleResults.Resources;

namespace SimpleResults;

/// <summary>
/// Defines extension methods for the <seealso href="https://www.nuget.org/packages/FluentValidation">Fluent Validation</seealso> package.
/// </summary>
public static class SRFluentValidationResultExtensions
{
    /// <summary>
    /// Checks if the validation result is failed.
    /// </summary>
    /// <param name="result">The result of running a validator.</param>
    /// <returns>
    /// <c>true</c> if the validation result is failed; otherwise <c>false</c>.
    /// </returns>
    public static bool IsFailed(this ValidationResult result)
        => !result.IsValid;

    /// <summary>
    /// Converts the validation result to a collection of error messages.
    /// </summary>
    /// <param name="result">The result of running a validator.</param>
    /// <returns>A collection that contains error messages.</returns>
    public static IEnumerable<string> AsErrors(this ValidationResult result)
        => result.Errors.Select(GetErrorMessage);

    private static string GetErrorMessage(ValidationFailure failure)
        => string.Format(
            ResponseMessages.PropertyFailedValidation, 
            failure.PropertyName, 
            failure.ErrorMessage);

    /// <summary>
    /// Represents a validation error that prevents the underlying service from completing.
    /// </summary>
    /// <param name="result">The result of running a validator.</param>
    /// <remarks>Used in situations where the consumer provides invalid data.</remarks>
    /// <returns>An instance of type <see cref="Result"/> that does not contain a value.</returns>
    public static Result Invalid(this ValidationResult result)
         => Result.Invalid(result.AsErrors());

    /// <summary>
    /// Represents a validation error that prevents the underlying service from completing.
    /// </summary>
    /// <param name="result">The result of running a validator.</param>
    /// <param name="message">An error message.</param>
    /// <remarks>Used in situations where the consumer provides invalid data.</remarks>
    /// <returns>An instance of type <see cref="Result"/> that does not contain a value.</returns>
    public static Result Invalid(this ValidationResult result, string message)
         => Result.Invalid(message, result.AsErrors());
}

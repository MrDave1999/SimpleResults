using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SimpleResults.Resources;

namespace SimpleResults;

/// <summary>
/// Contains extension methods that convert an instance of <see cref="ModelStateDictionary"/> 
/// into an instance of type <see cref="Result"/>.
/// </summary>
public static class SRModelStateDictionaryExtensions
{
    /// <summary>
    /// Checks if the model state is failed.
    /// </summary>
    /// <param name="modelState">The model state to be validated.</param>
    /// <returns>
    /// <c>true</c> if the model state is failed; otherwise <c>false</c>.
    /// </returns>
    public static bool IsFailed(this ModelStateDictionary modelState)
        => !modelState.IsValid;

    /// <summary>
    /// Represents a validation error that prevents the controller action from completing.
    /// </summary>
    /// <param name="modelState">
    /// The <see cref="ModelStateDictionary"/> containing errors to be returned to the client.
    /// </param>
    /// <remarks>Used in situations where the consumer provides invalid data.</remarks>
    /// <returns>An instance of type <see cref="Result"/> that does not contain a value.</returns>
    public static Result Invalid(this ModelStateDictionary modelState)
        => Result.Invalid(modelState.AsErrors());

    /// <summary>
    /// Represents a validation error that prevents the controller action from completing.
    /// </summary>
    /// <param name="modelState">
    /// The <see cref="ModelStateDictionary"/> containing errors to be returned to the client.
    /// </param>
    /// <param name="message">An error message.</param>
    /// <remarks>Used in situations where the consumer provides invalid data.</remarks>
    /// <returns>An instance of type <see cref="Result"/> that does not contain a value.</returns>
    public static Result Invalid(this ModelStateDictionary modelState, string message)
        => Result.Invalid(message, modelState.AsErrors());

    /// <summary>
    /// Represents a validation error that prevents the controller action from completing.
    /// </summary>
    /// <param name="modelState">
    /// The <see cref="ModelStateDictionary"/> containing errors to be returned to the client.
    /// </param>
    /// <remarks>Used in situations where the consumer provides invalid data.</remarks>
    /// <returns>
    /// An instance of type <see cref="BadRequestObjectResult"/> 
    /// that contains an instance of type <see cref="Result"/>.
    /// </returns>
    public static BadRequestObjectResult BadRequest(this ModelStateDictionary modelState)
        => modelState.BadRequest(ResponseMessages.ValidationErrors);

    /// <summary>
    /// Represents a validation error that prevents the controller action from completing.
    /// </summary>
    /// <param name="modelState">
    /// The <see cref="ModelStateDictionary"/> containing errors to be returned to the client.
    /// </param>
    /// <param name="message">An error message.</param>
    /// <remarks>Used in situations where the consumer provides invalid data.</remarks>
    /// <returns>
    /// An instance of type <see cref="BadRequestObjectResult"/> 
    /// that contains an instance of type <see cref="Result"/>.
    /// </returns>
    public static BadRequestObjectResult BadRequest(this ModelStateDictionary modelState, string message)
    {
        Result result = Result.Invalid(message, modelState.AsErrors());
        return new BadRequestObjectResult(result);
    }

    private static IEnumerable<string> AsErrors(this ModelStateDictionary modelState)
    {
        foreach (var(propertyName, value) in modelState)
        {
            var errors = value
                .Errors
                .Select(model => GetErrorMessage(propertyName, model.ErrorMessage));

            foreach (var error in errors)
                yield return error;
        }
    }

    private static string GetErrorMessage(string propertyName, string message)
        => string.Format(
            ResponseMessages.PropertyFailedValidation,
            propertyName,
            message);
}

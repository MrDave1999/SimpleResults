using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SimpleResults.Resources;

namespace SimpleResults;

public static class SRModelStateExtensions
{
    public static bool IsFailed(this ModelStateDictionary modelState)
        => !modelState.IsValid;

    public static Result Invalid(this ModelStateDictionary modelState)
        => Result.Invalid(modelState.AsErrors());

    public static Result Invalid(this ModelStateDictionary modelState, string message)
        => Result.Invalid(message, modelState.AsErrors());

    public static ActionResult BadRequest(this ModelStateDictionary modelState)
        => modelState.BadRequest(ResponseMessages.ValidationErrors);

    public static ActionResult BadRequest(this ModelStateDictionary modelState, string message)
    {
        Result result = Result.Invalid(message, modelState.AsErrors());
        return new BadRequestObjectResult(result);
    }

    private static IEnumerable<string> AsErrors(this ModelStateDictionary modelState)
    {
        foreach (var (propertyName, value) in modelState)
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

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SimpleResults;

/// <summary>
/// Translates the Result object to an <see cref="ActionResult"/>.
/// </summary>
public class TranslateResultToActionResultAttribute : ActionFilterAttribute
{
    /// <summary>
    /// Translates the Result object into a corresponding HTTP status code. 
    /// This translation occurs after a controller action is executed.
    /// </summary>
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Result is not ObjectResult objectResult) return;
        if (objectResult.Value is not ResultBase result) return;
        context.Result = result.TranslateToActionResult();
    }
}

﻿namespace SimpleResults;

/// <summary>
/// Translates the Result object to an <see cref="ActionResult"/>.
/// </summary>
public class TranslateResultToActionResultAttribute : ActionFilterAttribute
{
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Result is not ObjectResult objectResult) return;
        if (objectResult.Value is not ResultBase result) return;
        context.Result = result.TranslateToActionResult();
    }
}
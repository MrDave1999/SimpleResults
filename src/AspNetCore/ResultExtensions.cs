using System;
using Microsoft.AspNetCore.Mvc;
using SimpleResults.Resources;

namespace SimpleResults;

/// <summary>
/// Defines extension methods for the <c>Result</c> object.
/// </summary>
public static class ResultExtensions
{
    /// <summary>
    /// Converts the <see cref="PagedResult{T}" /> to <see cref="ActionResult{TValue}"/>
    /// where <c>TValue</c> is a <see cref="PagedResult{T}" />.
    /// </summary>
    /// <typeparam name="T">The type of objects to enumerate.</typeparam>
    /// <param name="result">
    /// An instance of type <see cref="PagedResult{T}"/>.
    /// </param>
    /// <returns>
    /// An instance of type <see cref="ActionResult{TValue}"/> 
    /// where <c>TValue</c> is a <see cref="PagedResult{T}"/>.
    /// </returns>
    /// <exception cref="NotSupportedException">
    /// <see cref="ResultBase.Status"/> is invalid.
    /// </exception>
    public static ActionResult<PagedResult<T>> ToActionResult<T>(this PagedResult<T> result) 
        => TranslateToActionResult(result);

    /// <summary>
    /// Converts the <see cref="ListedResult{T}" /> to <see cref="ActionResult{TValue}"/>
    /// where <c>TValue</c> is a <see cref="ListedResult{T}" />.
    /// </summary>
    /// <typeparam name="T">The type of objects to enumerate.</typeparam>
    /// <param name="result">
    /// An instance of type <see cref="ListedResult{T}"/>.
    /// </param>
    /// <returns>
    /// An instance of type <see cref="ActionResult{TValue}"/> 
    /// where <c>TValue</c> is a <see cref="ListedResult{T}"/>.
    /// </returns>
    /// <exception cref="NotSupportedException">
    /// <see cref="ResultBase.Status"/> is invalid.
    /// </exception>
    public static ActionResult<ListedResult<T>> ToActionResult<T>(this ListedResult<T> result) 
        => TranslateToActionResult(result);

    /// <summary>
    /// Converts the <see cref="Result{T}" /> to <see cref="ActionResult{TValue}"/>
    /// where <c>TValue</c> is a <see cref="Result{T}" />.
    /// </summary>
    /// <typeparam name="T">A value associated to the result.</typeparam>
    /// <param name="result">
    /// An instance of type <see cref="Result{T}"/>.
    /// </param>
    /// <returns>
    /// An instance of type <see cref="ActionResult{TValue}"/> 
    /// where <c>TValue</c> is a <see cref="Result{T}"/>.
    /// </returns>
    /// <exception cref="NotSupportedException">
    /// <see cref="ResultBase.Status"/> is invalid.
    /// </exception>
    public static ActionResult<Result<T>> ToActionResult<T>(this Result<T> result) 
        => TranslateToActionResult(result);

    /// <summary>
    /// Converts the <see cref="Result" /> to <see cref="ActionResult{TValue}"/> 
    /// where <c>TValue</c> is a <see cref="Result" />.
    /// </summary>
    /// <param name="result">An instance of type <see cref="Result"/>.</param>
    /// <returns>
    /// An instance of type <see cref="ActionResult{TValue}"/> 
    /// where <c>TValue</c> is a <see cref="Result"/>.
    /// </returns>
    /// <exception cref="NotSupportedException">
    /// <see cref="ResultBase.Status"/> is invalid.
    /// </exception>
    public static ActionResult<Result> ToActionResult(this Result result) 
        => TranslateToActionResult(result);

    internal static ActionResult TranslateToActionResult(this ResultBase result) => result.Status switch
    {
        ResultStatus.Ok            => new OkObjectResult(result),
        ResultStatus.Created       => new CreatedResult(nameof(TranslateToActionResult), result),
        ResultStatus.Invalid       => new BadRequestObjectResult(result),
        ResultStatus.NotFound      => new NotFoundObjectResult(result),
        ResultStatus.Unauthorized  => new UnauthorizedObjectResult(result),
        ResultStatus.Conflict      => new ConflictObjectResult(result),
        ResultStatus.Failure       => new UnprocessableContentResult(result),
        ResultStatus.CriticalError => new InternalServerErrorResult(result),
        ResultStatus.Forbidden     => new ForbiddenResult(result),
        _ => throw new NotSupportedException(string.Format(ResponseMessages.UnsupportedStatus, result.Status))
    };
}

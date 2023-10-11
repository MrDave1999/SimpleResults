namespace SimpleResults;

public static class ResultExtensions
{
    /// <summary>
    /// Converts the <c>Result&lt;T&gt;</c> object to <c>ActionResult&lt;Result&lt;T&gt;&gt;</c>.
    /// </summary>
    public static ActionResult<Result<T>> ToActionResult<T>(this Result<T> result)
    {
        return TranslateToActionResult(result);
    }

    /// <summary>
    /// Converts the <c>Result</c> object to <c>ActionResult&lt;Result&gt;</c>.
    /// </summary>
    public static ActionResult<Result> ToActionResult(this Result result)
    {
        return TranslateToActionResult(result);
    }

    private static ActionResult TranslateToActionResult(this ResultBase result)
    {
        return result.Status switch
        {
            ResultStatus.Ok                  => new OkObjectResult(result),
            ResultStatus.Created             => new CreatedResult(nameof(ToActionResult), result),
            ResultStatus.Invalid             => new BadRequestObjectResult(result),
            ResultStatus.NotFound            => new NotFoundObjectResult(result),
            ResultStatus.Unauthorized        => new UnauthorizedObjectResult(result),
            ResultStatus.Conflict            => new ConflictObjectResult(result),
            ResultStatus.Failure             => new UnprocessableContentResult(result),
            ResultStatus.CriticalError       => new InternalServerErrorResult(result),
            _ => throw new NotSupportedException($"Result {result.Status} conversion is not supported.")
        };
    }
}

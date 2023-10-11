namespace SimpleResults;

internal class UnprocessableContentResult : ObjectResult
{
    public UnprocessableContentResult(object value) : base(value)
    {
        StatusCode = StatusCodes.Status422UnprocessableEntity;
    }
}

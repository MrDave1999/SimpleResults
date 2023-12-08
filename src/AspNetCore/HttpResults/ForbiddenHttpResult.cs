namespace SimpleResults;

internal class ForbiddenHttpResult : IResult
{
    public object Value { get; }
    public int StatusCode => StatusCodes.Status403Forbidden;

    public ForbiddenHttpResult(object value)
    {
        Value = value;
    }

    public Task ExecuteAsync(HttpContext httpContext)
    {
        httpContext.Response.StatusCode = StatusCode;
        return httpContext.Response.WriteAsJsonAsync(Value);
    }
}

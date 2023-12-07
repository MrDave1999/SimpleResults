namespace SimpleResults;

internal class ForbiddenHttpResult : IResult
{
    private readonly object _value;

    public ForbiddenHttpResult(object value)
    {
        _value = value;
    }

    public Task ExecuteAsync(HttpContext httpContext)
    {
        httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
        return httpContext.Response.WriteAsJsonAsync(_value);
    }
}

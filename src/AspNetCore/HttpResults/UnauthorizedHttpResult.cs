namespace SimpleResults;

internal class UnauthorizedHttpResult : IResult
{
    private readonly object _value;

    public UnauthorizedHttpResult(object value)
    {
        _value = value;
    }

    public Task ExecuteAsync(HttpContext httpContext)
    {
        httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
        return httpContext.Response.WriteAsJsonAsync(_value);
    }
}

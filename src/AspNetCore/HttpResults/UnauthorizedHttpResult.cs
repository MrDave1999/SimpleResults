namespace SimpleResults;

internal class UnauthorizedHttpResult : IResult
{
    public object Value { get; }
    public int StatusCode => StatusCodes.Status401Unauthorized;

    public UnauthorizedHttpResult(object value)
    {
        Value = value;
    }

    public Task ExecuteAsync(HttpContext httpContext)
    {
        httpContext.Response.StatusCode = StatusCode;
        return httpContext.Response.WriteAsJsonAsync(Value);
    }
}

namespace SimpleResults;

internal class InternalServerErrorHttpResult : IResult
{
    public object Value { get; }
    public int StatusCode => StatusCodes.Status500InternalServerError;

    public InternalServerErrorHttpResult(object value)
    {
        Value = value;
    }

    public Task ExecuteAsync(HttpContext httpContext)
    {
        httpContext.Response.StatusCode = StatusCode;
        return httpContext.Response.WriteAsJsonAsync(Value);
    }
}

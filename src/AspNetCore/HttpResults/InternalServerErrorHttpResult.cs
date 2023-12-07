namespace SimpleResults;

internal class InternalServerErrorHttpResult : IResult
{
    private readonly object _value;

    public InternalServerErrorHttpResult(object value)
    {
        _value = value;
    }

    public Task ExecuteAsync(HttpContext httpContext)
    {
        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        return httpContext.Response.WriteAsJsonAsync(_value);
    }
}

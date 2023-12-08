using Microsoft.AspNetCore.Mvc;

namespace SimpleResults;

internal class InternalServerErrorResult : ObjectResult
{
    public InternalServerErrorResult(object value) : base(value)
    {
        StatusCode = StatusCodes.Status500InternalServerError;
    }
}

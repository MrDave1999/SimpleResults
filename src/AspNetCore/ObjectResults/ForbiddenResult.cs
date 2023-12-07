using Microsoft.AspNetCore.Mvc;

namespace SimpleResults;

internal class ForbiddenResult : ObjectResult
{
    public ForbiddenResult(object value) : base(value)
    {
        StatusCode = StatusCodes.Status403Forbidden;
    }
}

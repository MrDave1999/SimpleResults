#if NET7_0_OR_GREATER
using Microsoft.AspNetCore.Http;

namespace SimpleResults;

/// <summary>
/// Translates the Result object to an implementation of <see cref="IResult"/>.
/// <para>Result object can be:</para>
/// <list type="bullet">
/// <item><see cref="Result{T}"/></item>
/// <item><see cref="ListedResult{T}"/></item>
/// <item><see cref="PagedResult{T}"/></item>
/// <item><see cref="Result"/></item>
/// <item>A subtype of <see cref="ResultBase"/>.</item>
/// </list>
/// </summary>
public class TranslateResultToHttpResultFilter : IEndpointFilter
{
    /// <summary>
    /// Translates the Result object into a corresponding HTTP status code. 
    /// This translation occurs after an endpoint handler is executed.
    /// </summary>
    public async ValueTask<object> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        object endpointHandlerResult = await next(context);
        return endpointHandlerResult is ResultBase result ? 
            result.TranslateToHttpResult() :
            endpointHandlerResult;
    }
}
#endif

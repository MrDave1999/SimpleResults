namespace SimpleResults;

/// <summary>
/// Represents the paged result of an operation.
/// </summary>
/// <typeparam name="T">The type of objects to enumerate.</typeparam>
public sealed class PagedResult<T> : ResultBase
{
    /// <summary>
    /// Gets the data from a page.
    /// </summary>
    public IEnumerable<T> Data { get; init; }

    /// <summary>
    /// Gets information about the page.
    /// </summary>
    public PagedInfo PagedInfo { get; init; }

    public static implicit operator PagedResult<T>(Result result)
    {
        return new PagedResult<T>
        {
            Data        = Enumerable.Empty<T>(),
            PagedInfo   = default,
            IsSuccess   = result.IsSuccess,
            Message     = result.Message,
            Errors      = result.Errors,
            Status      = result.Status
        };
    }
}

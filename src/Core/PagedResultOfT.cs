namespace SimpleResults;

/// <summary>
/// Represents the paged result of an operation.
/// </summary>
/// <typeparam name="T">The type of objects to enumerate.</typeparam>
public sealed class PagedResult<T> : ResultBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PagedResult{T}"/> class.
    /// </summary>
    public PagedResult() { }

    /// <summary>
    /// Gets the data from a page.
    /// </summary>
    public IEnumerable<T> Data { get; init; } = Enumerable.Empty<T>();

    /// <summary>
    /// Gets information about the page.
    /// </summary>
    public PagedInfo PagedInfo { get; init; }

    /// <summary>
    /// Converts an instance of type <see cref="Result"/> to <see cref="PagedResult{T}"/>.
    /// </summary>
    /// <param name="result">An instance of type <see cref="Result"/>.</param>
    public static implicit operator PagedResult<T>(Result result) => new()
    {
        Data = Enumerable.Empty<T>(),
        PagedInfo = default,
        IsSuccess = result.IsSuccess,
        Message = result.Message,
        Errors = result.Errors,
        Status = result.Status
    };
}

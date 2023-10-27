namespace SimpleResults;

/// <summary>
/// Represents an enumerated result of an operation.
/// </summary>
/// <typeparam name="T">The type of objects to enumerate.</typeparam>
public sealed class ListedResult<T> : ResultBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ListedResult{T}"/> class.
    /// </summary>
    public ListedResult() { }

    /// <summary>
    /// Gets a list of data associated with the result.
    /// </summary>
    public IEnumerable<T> Data { get; init; } = Enumerable.Empty<T>();

    private static ListedResult<T> CreateInstance(ResultBase result, IEnumerable<T> data) => new()
    {
        Data = data,
        IsSuccess = result.IsSuccess,
        Message = result.Message,
        Errors = result.Errors,
        Status = result.Status
    };

    public static implicit operator ListedResult<T>(Result result)
        => CreateInstance(result, Enumerable.Empty<T>());

    public static implicit operator ListedResult<T>(Result<IEnumerable<T>> result)
        => CreateInstance(result, result.Data);

    public static implicit operator ListedResult<T>(Result<List<T>> result)
        => CreateInstance(result, result.Data);

    public static implicit operator ListedResult<T>(Result<T[]> result)
        => CreateInstance(result, result.Data);
}

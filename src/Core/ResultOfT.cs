namespace SimpleResults;

/// <typeparam name="T">A value associated to the result.</typeparam>
/// <inheritdoc cref="ResultBase" />
public sealed class Result<T> : ResultBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Result{T}"/> class.
    /// </summary>
    public Result() { }

    /// <summary>
    /// Gets the data associated with the result.
    /// </summary>
    public T Data { get; init; }

    public static implicit operator Result<T>(Result result) => new()
    {
        Data = default,
        IsSuccess = result.IsSuccess,
        Message = result.Message,
        Errors = result.Errors,
        Status = result.Status
    };
}

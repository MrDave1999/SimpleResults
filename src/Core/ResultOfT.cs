namespace SimpleResults;

/// <typeparam name="T">A value associated to the result.</typeparam>
/// <inheritdoc cref="ResultBase" />
public sealed class Result<T> : ResultBase
{
    internal Result() { }

    /// <summary>
    /// Gets the data associated with the result.
    /// </summary>
    public T Data { get; init; }

    public static implicit operator Result<T>(Result result)
    {
        return new Result<T>
        {
            Data        = default,
            IsSuccess   = result.IsSuccess,
            Message     = result.Message,
            Errors      = result.Errors,
            Status      = result.Status
        };
    }
}

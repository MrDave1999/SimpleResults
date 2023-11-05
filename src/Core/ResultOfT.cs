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

    /// <summary>
    /// Converts an instance of type <see cref="Result"/> to <see cref="Result{T}"/>.
    /// </summary>
    /// <param name="result">An instance of type <see cref="Result"/>.</param>
    public static implicit operator Result<T>(Result result) 
        => result.ToResult(default(T));
}

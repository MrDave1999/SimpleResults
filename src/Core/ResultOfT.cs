using SimpleResults.Resources;

namespace SimpleResults;

/// <summary>
/// Represents the result of an operation that returns a value.
/// </summary>
/// <typeparam name="T">A value associated to the result.</typeparam>
public sealed class Result<T> : ResultBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Result{T}"/> class.
    /// </summary>
    public Result() { }

    /// <summary>
    /// Gets the data associated with the result.
    /// </summary>
    /// <value>
    /// The data associated with the result.
    /// Its default value is <c>null</c>.
    /// </value>
    public T Data { get; init; }

    /// <summary>
    /// Converts an instance of type <see cref="Result"/> to <see cref="Result{T}"/>.
    /// </summary>
    /// <param name="result">An instance of type <see cref="Result"/>.</param>
    public static implicit operator Result<T>(Result result) 
        => result.ToResult(default(T));

    /// <summary>
    /// Converts the value of type <typeparamref name="T"/> to an instance of type <see cref="Result{T}"/>.
    /// </summary>
    /// <param name="value">
    /// An instance of type <typeparamref name="T"/> that represents the value.
    /// </param>
    public static implicit operator Result<T>(T value) => new() 
    { 
        IsSuccess = true,
        Data = value,
        Message = ResponseMessages.Success,
        Status = ResultStatus.Ok
    };

    /// <summary>
    /// Converts an instance of type <see cref="Result{T}"/> to the value of type <typeparamref name="T"/>.
    /// </summary>
    /// <param name="result">An instance of type <see cref="Result{T}"/>.</param>
    public static implicit operator T(Result<T> result) 
        => result.Data;
}

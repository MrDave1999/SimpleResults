using System;
using System.Collections.Generic;
using System.Linq;

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
    /// <value>
    /// A list of data associated with the result.
    /// Its default value is never a null value.
    /// </value>
    public IEnumerable<T> Data { get; init; } = Enumerable.Empty<T>();

    private static ListedResult<T> CreateInstance(ResultBase result, IEnumerable<T> data) => new()
    {
        Data = data,
        IsSuccess = result.IsSuccess,
        Message = result.Message,
        Errors = result.Errors,
        Status = result.Status
    };

    /// <summary>
    /// Converts an instance of type <see cref="Result"/> to <see cref="ListedResult{T}"/>.
    /// </summary>
    /// <param name="result">An instance of type <see cref="Result"/>.</param>
    public static implicit operator ListedResult<T>(Result result)
        => result.ToListedResult(Enumerable.Empty<T>());

    /// <summary>
    /// Converts an instance of type <see cref="Result{T}"/> to <see cref="ListedResult{T}"/>.
    /// </summary>
    /// <param name="result">
    /// An instance of type <see cref="Result{T}"/> where <c>T</c> is of type <see cref="IEnumerable{T}"/>.
    /// </param>
    public static implicit operator ListedResult<T>(Result<IEnumerable<T>> result)
        => CreateInstance(result, result.Data);

    /// <summary>
    /// Converts an instance of type <see cref="Result{T}"/> to <see cref="ListedResult{T}"/>.
    /// </summary>
    /// <param name="result">
    /// An instance of type <see cref="Result{T}"/> where <c>T</c> is of type <see cref="List{T}"/>.
    /// </param>
    public static implicit operator ListedResult<T>(Result<List<T>> result)
        => CreateInstance(result, result.Data);

    /// <summary>
    /// Converts an instance of type <see cref="Result{T}"/> to <see cref="ListedResult{T}"/>.
    /// </summary>
    /// <param name="result">
    /// An instance of type <see cref="Result{T}"/> where <c>T</c> is of type <see cref="Array"/>.
    /// </param>
    public static implicit operator ListedResult<T>(Result<T[]> result)
        => CreateInstance(result, result.Data);
}

﻿namespace SimpleResults;

/// <summary>
/// Represents an enumerated result of an operation.
/// </summary>
/// <typeparam name="T">The type of objects to enumerate.</typeparam>
public sealed class ListedResult<T> : ResultBase
{
    public ListedResult() { }

    /// <summary>
    /// Gets a list of data associated with the result.
    /// </summary>
    public IEnumerable<T> Data { get; init; }

    private static ListedResult<T> CreateInstance(ResultBase result, IEnumerable<T> data)
    {
        return new ListedResult<T>
        {
            Data        = data,
            IsSuccess   = result.IsSuccess,
            Message     = result.Message,
            Errors      = result.Errors,
            Status      = result.Status
        };
    }

    public static implicit operator ListedResult<T>(Result result)
    { 
        return CreateInstance(result, Enumerable.Empty<T>());
    }

    public static implicit operator ListedResult<T>(Result<IEnumerable<T>> result)
    { 
        return CreateInstance(result, result.Data);
    }

    public static implicit operator ListedResult<T>(Result<List<T>> result)
    {
        return CreateInstance(result, result.Data);
    }

    public static implicit operator ListedResult<T>(Result<T[]> result)
    {
        return CreateInstance(result, result.Data);
    }
}
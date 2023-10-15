namespace SimpleResults;

/// <summary>
/// Represents the set of results of an operation.
/// </summary>
/// <typeparam name="T">The type of objects to enumerate.</typeparam>
public sealed class ResultSet<T> : ResultBase
{
    public ResultSet() { }

    /// <summary>
    /// Gets a list of data associated with the result.
    /// </summary>
    public IEnumerable<T> Data { get; init; }

    private static ResultSet<T> CreateInstance(ResultBase result, IEnumerable<T> data)
    {
        return new ResultSet<T>
        {
            Data        = data,
            IsSuccess   = result.IsSuccess,
            Message     = result.Message,
            Errors      = result.Errors,
            Status      = result.Status
        };
    }

    public static implicit operator ResultSet<T>(Result result)
    { 
        return CreateInstance(result, Enumerable.Empty<T>());
    }

    public static implicit operator ResultSet<T>(Result<IEnumerable<T>> result)
    { 
        return CreateInstance(result, result.Data);
    }

    public static implicit operator ResultSet<T>(Result<List<T>> result)
    {
        return CreateInstance(result, result.Data);
    }

    public static implicit operator ResultSet<T>(Result<T[]> result)
    {
        return CreateInstance(result, result.Data);
    }
}

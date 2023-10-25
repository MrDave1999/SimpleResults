namespace SimpleResults;

/// <summary>
/// Represents the result of an operation.
/// </summary>
public abstract class ResultBase
{
    /// <summary>
    /// A value indicating that the result was successful.
    /// </summary>
    [JsonPropertyName("success")]
    public bool IsSuccess { get; init; }

    /// <summary>
    /// A value that indicates that the result was a failure.
    /// </summary>
    [JsonIgnore]
    public bool IsFailed { get => !IsSuccess; }

    /// <summary>
    /// Gets the description of a result.
    /// </summary>
    public string Message { get; init; } = ResponseMessages.Error;

    /// <summary>
    /// Gets a collection of errors.
    /// </summary>
    public IEnumerable<string> Errors { get; init; } = Enumerable.Empty<string>();

    /// <summary>
    /// Gets the current status of a result.
    /// </summary>
    [JsonIgnore]
    public ResultStatus Status { get; init; } = ResultStatus.Failure;
}

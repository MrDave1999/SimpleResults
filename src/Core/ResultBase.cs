using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using SimpleResults.Resources;

namespace SimpleResults;

/// <summary>
/// Represents the result of an operation.
/// </summary>
public abstract class ResultBase
{
    /// <summary>
    /// Gets the current status of a result.
    /// </summary>
    /// <value>
    /// The current status of a result.
    /// Its default value is a <see cref="ResultStatus.Failure"/>.
    /// </value>
    [JsonConverter(typeof(JsonStringEnumConverter<ResultStatus>))]
    public ResultStatus Status { get; init; } = ResultStatus.Failure;

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
    /// <value>
    /// The description of a result.
    /// Its default value is never a null value.
    /// </value>
    public string Message { get; init; } = ResponseMessages.Error;

    /// <summary>
    /// Gets a collection of errors.
    /// </summary>
    /// <value>
    /// A collection that contains error messages.
    /// Its default value is never a null value.
    /// </value>
    public IEnumerable<string> Errors { get; init; } = Enumerable.Empty<string>();
}

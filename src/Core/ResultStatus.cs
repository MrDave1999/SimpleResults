namespace SimpleResults;

/// <summary>
/// Represents the status of a Result object.
/// </summary>
public enum ResultStatus
{
    /// <summary>
    /// Represents a successful status.
    /// </summary>
    Ok,
    /// <summary>
    /// Represents a status in which the service successfully creates a resource.
    /// </summary>
    Created,
    /// <summary>
    /// Represents a status in which the consumer has provided invalid data.
    /// </summary>
    Invalid,
    /// <summary>
    /// Represents a status where a service was unable to find a requested resource.
    /// </summary>
    NotFound,
    /// <summary>
    /// Represents a status where a user does not have valid authentication credentials for the target resource.
    /// </summary>
    Unauthorized,
    /// <summary>
    /// Represents a status where a service is in conflict due to the current state of a resource.
    /// </summary>
    Conflict,
    /// <summary>
    /// Represents a failed status.
    /// </summary>
    Failure,
    /// <summary>
    /// Represents a status where unexpected behavior has occurred and 
    /// the service does not know how to handle it (e.g., database connection error).
    /// </summary>
    CriticalError,
    /// <summary>
    /// Represents a status where the user does not have permission to perform some action.
    /// </summary>
    Forbidden,
    /// <summary>
    /// Represents a status where the service returns the contents of a file as an array of bytes.
    /// </summary>
    ByteArrayFile,
    /// <summary>
    /// Represents a status where the service returns the contents of a file as a stream.
    /// </summary>
    StreamFile
}

namespace SimpleResults;

/// <summary>
/// Represents the status of a result object.
/// </summary>
public enum ResultStatus
{
    Ok,
    Created,
    Invalid,
    NotFound,
    Unauthorized,
    Conflict,
    Failure,
    CriticalError
}

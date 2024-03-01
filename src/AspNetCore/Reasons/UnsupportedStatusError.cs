using SimpleResults.Resources;

namespace SimpleResults;

internal readonly ref struct UnsupportedStatusError
{
    public string Message { get; }
    public UnsupportedStatusError(ResultStatus status)
        => Message = string.Format(ResponseMessages.UnsupportedStatus, status);
}

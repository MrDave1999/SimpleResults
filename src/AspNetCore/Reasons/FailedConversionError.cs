using SimpleResults.Resources;

namespace SimpleResults;

internal readonly ref struct FailedConversionError
{
    public string Message { get; }
    public FailedConversionError(string typeName)
        => Message = string.Format(ResponseMessages.FailedConversion, typeName ?? string.Empty);
}

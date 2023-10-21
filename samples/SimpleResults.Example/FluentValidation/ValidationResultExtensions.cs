namespace SimpleResults.Example.FluentValidation;

public static class ValidationResultExtensions
{
    public static bool IsFailed(this ValidationResult result) => !result.IsValid;
    public static IEnumerable<string> AsErrors(this ValidationResult result)
        => result.Errors.Select(failure => failure.ErrorMessage);
}

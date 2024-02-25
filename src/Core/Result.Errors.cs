using System.Collections.Generic;
using System.Linq;
using SimpleResults.Resources;

namespace SimpleResults;

public partial class Result
{
    /// <inheritdoc cref="Failure(string)" />
    public static Result Failure() 
        => Failure(ResponseMessages.Failure);

    /// <param name="message">An error message.</param>
    /// <inheritdoc cref="Failure(string, IEnumerable{string})" />
    public static Result Failure(string message) 
        => Failure(message, Enumerable.Empty<string>());

    /// <param name="errors">A collection of string that contains error messages.</param>
    /// <inheritdoc cref="Failure(string, IEnumerable{string})" />
    public static Result Failure(IEnumerable<string> errors) 
        => Failure(string.Empty, errors);

    /// <summary>
    /// Represents an error that occurred during the execution of a service.
    /// </summary>
    /// <param name="message">A general description of the error.</param>
    /// <param name="errors">A collection of errors.</param>
    public static Result Failure(string message, IEnumerable<string> errors) 
        => CreateError(message, ResultStatus.Failure, errors);

    /// <inheritdoc cref="Invalid(string)" />
    public static Result Invalid() 
        => Invalid(ResponseMessages.Invalid);

    /// <param name="message">An error message.</param>
    /// <inheritdoc cref="Invalid(string, IEnumerable{string})" />
    public static Result Invalid(string message) 
        => Invalid(message, Enumerable.Empty<string>());

    /// <param name="errors">A collection of string that contains error messages.</param>
    /// <inheritdoc cref="Invalid(string, IEnumerable{string})" />
    public static Result Invalid(IEnumerable<string> errors) 
        => Invalid(ResponseMessages.ValidationErrors, errors);

    /// <summary>
    /// Represents a validation error that prevents the underlying service from completing.
    /// </summary>
    /// <remarks>Used in situations where the consumer provides invalid data.</remarks>
    /// <param name="message">A general description of the error.</param>
    /// <param name="errors">A collection of errors.</param>
    public static Result Invalid(string message, IEnumerable<string> errors)
        => CreateError(message, ResultStatus.Invalid, errors);

    /// <inheritdoc cref="NotFound(string)" />
    public static Result NotFound() 
        => NotFound(ResponseMessages.NotFound);

    /// <param name="message">An error message.</param>
    /// <inheritdoc cref="NotFound(string, IEnumerable{string})" />
    public static Result NotFound(string message) 
        => NotFound(message, Enumerable.Empty<string>());

    /// <param name="errors">A collection of string that contains error messages.</param>
    /// <inheritdoc cref="NotFound(string, IEnumerable{string})" />
    public static Result NotFound(IEnumerable<string> errors) 
        => NotFound(string.Empty, errors);

    /// <summary>
    /// Represents the situation where a service was unable to find a requested resource.
    /// </summary>
    /// <param name="message">A general description of the error.</param>
    /// <param name="errors">A collection of errors.</param>
    public static Result NotFound(string message, IEnumerable<string> errors)
        => CreateError(message, ResultStatus.NotFound, errors);

    /// <inheritdoc cref="Unauthorized(string)" />
    public static Result Unauthorized() 
        => Unauthorized(ResponseMessages.Unauthorized);

    /// <param name="message">An error message.</param>
    /// <inheritdoc cref="Unauthorized(string, IEnumerable{string})" />
    public static Result Unauthorized(string message) 
        => Unauthorized(message, Enumerable.Empty<string>());

    /// <param name="errors">A collection of string that contains error messages.</param>
    /// <inheritdoc cref="Unauthorized(string, IEnumerable{string})" />
    public static Result Unauthorized(IEnumerable<string> errors) 
        => Unauthorized(string.Empty, errors);

    /// <summary>
    /// Represents a situation where a user does not have valid authentication credentials for the target resource.
    /// </summary>
    /// <remarks>
    /// Similar to <see cref="Forbidden()"/>, but specifically for use when authentication 
    /// is required and has failed or has not yet been provided. 
    /// </remarks>
    /// <param name="message">A general description of the error.</param>
    /// <param name="errors">A collection of errors.</param>
    public static Result Unauthorized(string message, IEnumerable<string> errors) 
        => CreateError(message, ResultStatus.Unauthorized, errors);

    /// <inheritdoc cref="Conflict(string)" />
    public static Result Conflict() 
        => Conflict(ResponseMessages.Conflict);

    /// <param name="message">An error message.</param>
    /// <inheritdoc cref="Conflict(string, IEnumerable{string})" />
    public static Result Conflict(string message) 
        => Conflict(message, Enumerable.Empty<string>());

    /// <param name="errors">A collection of string that contains error messages.</param>
    /// <inheritdoc cref="Conflict(string, IEnumerable{string})" />
    public static Result Conflict(IEnumerable<string> errors) 
        => Conflict(string.Empty, errors);

    /// <summary>
    ///  Represents a situation where a service is in conflict due to the current state of a resource.
    /// </summary>
    /// <param name="message">A general description of the error.</param>
    /// <param name="errors">A collection of errors.</param>
    public static Result Conflict(string message, IEnumerable<string> errors) 
        => CreateError(message, ResultStatus.Conflict, errors);

    /// <inheritdoc cref="CriticalError(string)" />
    public static Result CriticalError() 
        => CriticalError(ResponseMessages.CriticalError);

    /// <param name="message">An error message.</param>
    /// <inheritdoc cref="CriticalError(string, IEnumerable{string})" />
    public static Result CriticalError(string message) 
        => CriticalError(message, Enumerable.Empty<string>());

    /// <param name="errors">A collection of string that contains error messages.</param>
    /// <inheritdoc cref="CriticalError(string, IEnumerable{string})" />
    public static Result CriticalError(IEnumerable<string> errors)
        => CriticalError(string.Empty, errors);

    /// <summary>
    /// Represents a situation where a service detects unexpected behavior (critical error) and does not know how to handle it.
    /// </summary>
    /// <remarks>
    /// Everything provided by the user was valid, but the service was unable to complete due to an exception.
    /// </remarks>
    /// <param name="message">A general description of the error.</param>
    /// <param name="errors">A collection of errors.</param>
    public static Result CriticalError(string message, IEnumerable<string> errors) 
        => CreateError(message, ResultStatus.CriticalError, errors);

    /// <inheritdoc cref="Forbidden(string)" />
    public static Result Forbidden()
        => Forbidden(ResponseMessages.Forbidden);

    /// <param name="message">An error message.</param>
    /// <inheritdoc cref="Forbidden(string, IEnumerable{string})" />
    public static Result Forbidden(string message)
        => Forbidden(message, Enumerable.Empty<string>());

    /// <param name="errors">A collection of string that contains error messages.</param>
    /// <inheritdoc cref="Forbidden(string, IEnumerable{string})" />
    public static Result Forbidden(IEnumerable<string> errors)
        => Forbidden(string.Empty, errors);

    /// <summary>
    /// Represents a situation where the user does not have permission to perform some action.
    /// </summary>
    /// <param name="message">A general description of the error.</param>
    /// <param name="errors">A collection of errors.</param>
    public static Result Forbidden(string message, IEnumerable<string> errors) 
        => CreateError(message, ResultStatus.Forbidden, errors);

    private static Result CreateError(
        string message, 
        ResultStatus status, 
        IEnumerable<string> errors) => new() 
        { 
            IsSuccess = false, 
            Message = message, 
            Status = status, 
            Errors = errors 
        };
}

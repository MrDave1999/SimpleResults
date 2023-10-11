namespace SimpleResults;

/// <summary>
/// Represents the result of an operation. 
/// </summary>
/// <remarks>This class defines different types of results for an operation.</remarks>
public sealed class Result : ResultBase
{
    internal Result() { }

    /// <inheritdoc cref="Success{T}(T, string)" />
    public static Result<T> Success<T>(T data)
    {
        return Success(data, ResponseMessages.Success);
    }

    /// <summary>
    /// Represents a successful operation and accepts a values as the result of the operation.
    /// </summary>
    /// <param name="data">The value to be set.</param>
    /// <param name="message">A message of success.</param>
    public static Result<T> Success<T>(T data, string message)
    {
        return new Result<T>
        {
            Data        = data,
            IsSuccess   = true,
            Message     = message,
            Status      = ResultStatus.Ok
        };
    }

    /// <summary>
    /// Represents a successful operation.
    /// </summary>
    public static Result Success()
    {
        return Success(ResponseMessages.Success);
    }

    /// <summary>
    /// Represents a successful operation and accepts a messages that describes the result.
    /// </summary>
    /// <param name="message">A message of success.</param>
    public static Result Success(string message)
    {
        return new Result
        {
            IsSuccess   = true,
            Message     = message,
            Status      = ResultStatus.Ok
        };
    }

    /// <inheritdoc cref="CreatedResource(string)" />
    public static Result CreatedResource()
    {
        return CreatedResource(ResponseMessages.CreatedResource);
    }

    /// <summary>
    /// Represents a situation in which the service successfully creates a resource.
    /// </summary>
    /// <param name="message">A message of success.</param>
    public static Result CreatedResource(string message)
    {
        return new Result
        {
            IsSuccess   = true,
            Message     = message,
            Status      = ResultStatus.Created
        };
    }

    /// <inheritdoc cref="CreatedResource(int, string)" />
    public static Result<CreatedId> CreatedResource(int id)
    {
        return CreatedResource(id, ResponseMessages.CreatedResource);
    }

    /// <summary>
    /// Represents a situation in which the service successfully creates a resource.
    /// </summary>
    /// <param name="id">The ID of the created resource.</param>
    /// <param name="message">A message of success.</param>
    public static Result<CreatedId> CreatedResource(int id, string message)
    {
        return new Result<CreatedId>
        {
            Data        = new CreatedId { Id = id },
            IsSuccess   = true,
            Message     = message,
            Status      = ResultStatus.Created
        };
    }

    /// <inheritdoc cref="CreatedResource(Guid, string)" />
    public static Result<CreatedGuid> CreatedResource(Guid guid)
    {
        return CreatedResource(guid, ResponseMessages.CreatedResource);
    }

    /// <summary>
    /// Represents a situation in which the service successfully creates a resource.
    /// </summary>
    /// <param name="guid">The GUID assigned to the resource.</param>
    /// <param name="message">A message of success.</param>
    public static Result<CreatedGuid> CreatedResource(Guid guid, string message)
    {
        return new Result<CreatedGuid>
        {
            Data      = new CreatedGuid { Id = guid.ToString() },
            IsSuccess = true,
            Message   = message,
            Status    = ResultStatus.Created
        };
    }

    /// <summary>
    /// Represents a situation in which the service successfully updates a resource.
    /// </summary>
    public static Result UpdatedResource()
    {
        return new Result
        {
            IsSuccess   = true,
            Message     = ResponseMessages.UpdatedResource,
            Status      = ResultStatus.Ok
        };
    }

    /// <summary>
    /// Represents a situation in which the service successfully deletes a resource.
    /// </summary>
    public static Result DeletedResource()
    {
        return new Result
        {
            IsSuccess   = true,
            Message     = ResponseMessages.DeletedResource,
            Status      = ResultStatus.Ok
        };
    }

    /// <summary>
    /// Represents a situation in which the service successfully obtains a resource.
    /// </summary>
    public static Result ObtainedResource()
    {
        return new Result
        {
            IsSuccess = true,
            Message   = ResponseMessages.ObtainedResource,
            Status    = ResultStatus.Ok
        };
    }

    /// <inheritdoc cref="Failure(string)" />
    public static Result Failure()
    {
        return Failure(ResponseMessages.Failure);
    }

    /// <param name="message">An error message.</param>
    /// <inheritdoc cref="Failure(string, IEnumerable{string})" />
    public static Result Failure(string message)
    {
        return Failure(message, Enumerable.Empty<string>());
    }

    /// <summary>
    /// Represents an error that occurred during the execution of a service.
    /// </summary>
    /// <param name="message">A general description of the error.</param>
    /// <param name="errors">A collection of errors.</param>
    public static Result Failure(string message, IEnumerable<string> errors)
    {
        return new Result
        {
            IsSuccess   = false,
            Message     = message,
            Status      = ResultStatus.Failure,
            Errors      = errors
        };
    }

    /// <inheritdoc cref="Invalid(string)" />
    public static Result Invalid()
    {
        return Invalid(ResponseMessages.Invalid);
    }

    /// <param name="message">An error message.</param>
    /// <inheritdoc cref="Invalid(string, IEnumerable{string})" />
    public static Result Invalid(string message)
    {
        return Invalid(message, Enumerable.Empty<string>());
    }

    /// <summary>
    /// Represents a validation error that prevents the underlying service from completing.
    /// </summary>
    /// <param name="message">A general description of the error.</param>
    /// <param name="errors">A collection of errors.</param>
    public static Result Invalid(string message, IEnumerable<string> errors)
    {
        return new Result
        {
            IsSuccess   = false,
            Message     = message,
            Status      = ResultStatus.Invalid,
            Errors      = errors
        };
    }

    /// <inheritdoc cref="NotFound(string)" />
    public static Result NotFound()
    {
        return NotFound(ResponseMessages.NotFound);
    }

    /// <param name="message">An error message.</param>
    /// <inheritdoc cref="NotFound(string, IEnumerable{string})" />
    public static Result NotFound(string message)
    {
        return NotFound(message, Enumerable.Empty<string>());
    }

    /// <summary>
    /// Represents the situation where a service was unable to find a requested resource.
    /// </summary>
    /// <param name="message">A general description of the error.</param>
    /// <param name="errors">A collection of errors.</param>
    public static Result NotFound(string message, IEnumerable<string> errors)
    {
        return new Result
        {
            IsSuccess   = false,
            Message     = message,
            Status      = ResultStatus.NotFound,
            Errors      = errors
        };
    }

    /// <inheritdoc cref="Unauthorized(string)" />
    public static Result Unauthorized()
    {
        return Unauthorized(ResponseMessages.Unauthorized);
    }

    /// <param name="message">An error message.</param>
    /// <inheritdoc cref="Unauthorized(string, IEnumerable{string})" />
    public static Result Unauthorized(string message)
    {
        return Unauthorized(message, Enumerable.Empty<string>());
    }

    /// <summary>
    /// Represents that a user does not have valid authentication credentials for the target resource.
    /// </summary>
    /// <param name="message">A general description of the error.</param>
    /// <param name="errors">A collection of errors.</param>
    /// <returns></returns>
    public static Result Unauthorized(string message, IEnumerable<string> errors)
    {
        return new Result
        {
            IsSuccess   = false,
            Message     = message,
            Status      = ResultStatus.Unauthorized,
            Errors      = errors
        };
    }

    /// <inheritdoc cref="Conflict(string)" />
    public static Result Conflict()
    {
        return Conflict(ResponseMessages.Conflict);
    }

    /// <param name="message">An error message.</param>
    /// <inheritdoc cref="Conflict(string, IEnumerable{string})" />
    public static Result Conflict(string message)
    {
        return Conflict(message, Enumerable.Empty<string>());
    }

    /// <summary>
    ///  Represents a situation where a service is in conflict due to the current state of a resource.
    /// </summary>
    /// <param name="message">A general description of the error.</param>
    /// <param name="errors">A collection of errors.</param>
    public static Result Conflict(string message, IEnumerable<string> errors)
    {
        return new Result
        {
            IsSuccess   = false,
            Message     = message,
            Status      = ResultStatus.Conflict,
            Errors      = errors
        };
    }

    /// <inheritdoc cref="CriticalError(string)" />
    public static Result CriticalError()
    {
        return CriticalError(ResponseMessages.CriticalError);
    }

    /// <param name="message">An error message.</param>
    /// <inheritdoc cref="CriticalError(string, IEnumerable{string})" />
    public static Result CriticalError(string message)
    {
        return CriticalError(message, Enumerable.Empty<string>());
    }

    /// <summary>
    /// Represents a critical error that occurred during the execution of the service.
    /// </summary>
    /// <remarks>
    /// Everything provided by the user was valid, but the service was unable to complete due to an exception.
    /// </remarks>
    /// <param name="message">A general description of the error.</param>
    /// <param name="errors">A collection of errors.</param>
    public static Result CriticalError(string message, IEnumerable<string> errors)
    {
        return new Result
        {
            IsSuccess   = false,
            Message     = message,
            Status      = ResultStatus.CriticalError,
            Errors      = errors
        };
    }
}

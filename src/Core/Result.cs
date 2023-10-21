﻿namespace SimpleResults;

/// <summary>
/// Represents the result of an operation. 
/// </summary>
/// <remarks>This class defines different types of results for an operation.</remarks>
public sealed partial class Result : ResultBase
{
    public Result() { }

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

    /// <inheritdoc cref="Success{T}(IEnumerable{T}, PagedInfo, string)" />
    public static PagedResult<T> Success<T>(IEnumerable<T> data, PagedInfo pagedInfo)
    {
        return Success(data, pagedInfo, ResponseMessages.ObtainedResources);
    }

    /// <summary>
    /// Represents a successful operation and accepts a set of data and paged information as arguments.
    /// </summary>
    /// <param name="data">A set of data.</param>
    /// <param name="pagedInfo">Some information about the page.</param>
    /// <param name="message">A message of success.</param>
    public static PagedResult<T> Success<T>(IEnumerable<T> data, PagedInfo pagedInfo, string message)
    {
        return new PagedResult<T>
        {
            Data      = data,
            PagedInfo = pagedInfo,
            IsSuccess = true,
            Message   = message,
            Status    = ResultStatus.Ok
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
        return Success(ResponseMessages.UpdatedResource);
    }

    /// <summary>
    /// Represents a situation in which the service successfully deletes a resource.
    /// </summary>
    public static Result DeletedResource()
    {
        return Success(ResponseMessages.DeletedResource);
    }

    /// <summary>
    /// Represents a situation in which the service successfully obtains a resource.
    /// </summary>
    /// <param name="data">The value to be set.</param>
    public static Result<T> ObtainedResource<T>(T data)
    {
        return Success(data, ResponseMessages.ObtainedResource);
    }

    /// <summary>
    /// Represents a situation in which the service successfully obtains multiple resources.
    /// </summary>
    /// <param name="data">A set of data.</param>
    public static ListedResult<T> ObtainedResources<T>(IEnumerable<T> data)
    {
        return Success(data, ResponseMessages.ObtainedResources);
    }
}

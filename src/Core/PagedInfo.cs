using System;
using SimpleResults.Resources;

namespace SimpleResults;

/// <summary>
/// Represents paged information.
/// </summary>
public class PagedInfo
{
    /// <summary>
    /// Gets the current page number.
    /// </summary>
    public int PageNumber { get; private set; }

    /// <summary>
    /// Gets the size per page.
    /// </summary>
    public int PageSize { get; private set; }

    /// <summary>
    /// Gets the total records of a resource.
    /// </summary>
    public int TotalRecords { get; private set; }

    /// <summary>
    /// Gets the total number of pages.
    /// </summary>
    public int TotalPages { get; }

    /// <summary>
    /// Checks if it has previous page.
    /// </summary>
    public bool HasPrevious => PageNumber > 1;

    /// <summary>
    /// Checks if it has next page.
    /// </summary>
    public bool HasNext => PageNumber < TotalPages;

    /// <summary>
    /// Initializes a new instance of the <see cref="PagedInfo"/> class 
    /// with the page number, size and total records.
    /// </summary>
    /// <param name="pageNumber">The current page number.</param>
    /// <param name="pageSize">The size per page.</param>
    /// <param name="totalRecords">The total records of a resource.</param>
    /// <exception cref="DivideByZeroException">
    /// <c>pageSize</c> is equal to 0.
    /// </exception>
    public PagedInfo(int pageNumber, int pageSize, int totalRecords)
    {
        if (pageSize == 0)
            throw new DivideByZeroException(new DivideByZeroError(nameof(pageSize)).Message);

        PageNumber   = pageNumber;
        PageSize     = pageSize;
        TotalRecords = totalRecords;
        TotalPages   = (int)Math.Ceiling(totalRecords / (double)pageSize);
    }

    internal readonly ref struct DivideByZeroError
    {
        public string Message { get; }
        public DivideByZeroError(string parameterName)
            => Message = string.Format(ResponseMessages.DivideByZero, parameterName ?? string.Empty);
    }
}

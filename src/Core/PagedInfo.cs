namespace SimpleResults;

/// <summary>
/// Represents paged information.
/// </summary>
public sealed class PagedInfo
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

    public PagedInfo(int pageNumber, int pageSize, int totalRecords)
    {
        if (pageSize == 0)
        {
            var message = string.Format(ResponseMessages.DivideByZero, nameof(pageSize));
            throw new DivideByZeroException(message);
        }

        PageNumber   = pageNumber;
        PageSize     = pageSize;
        TotalRecords = totalRecords;
        TotalPages   = (int)Math.Ceiling(totalRecords / (double)pageSize);
    }
}

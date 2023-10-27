namespace SimpleResults;

/// <summary>
/// Represents the numerical identification (ID) created for a specific resource.
/// </summary>
public sealed class CreatedId
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreatedId"/> class.
    /// </summary>
    public CreatedId() { }

    /// <summary>
    /// Gets or sets the identification of a resource.
    /// </summary>
    public int Id { get; init; }
}

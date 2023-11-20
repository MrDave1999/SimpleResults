namespace SimpleResults;

/// <summary>
/// Represents the globally unique identifier (GUID) created for a specific resource.
/// </summary>
public sealed class CreatedGuid
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreatedGuid"/> class.
    /// </summary>
    public CreatedGuid() { }

    /// <summary>
    /// Gets or sets the identification of a resource.
    /// </summary>
    /// <value>
    /// The identification of a resource.
    /// Its default value is an empty string.
    /// </value> 
    public string Id { get; init; } = string.Empty;
}

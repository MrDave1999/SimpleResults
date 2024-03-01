using System.IO;

namespace SimpleResults;

/// <summary>
/// Represents the content of a file using a stream.
/// </summary>
public class StreamFileContent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StreamFileContent"/> class 
    /// with the provided <paramref name="content"/>.
    /// </summary>
    /// <param name="content">The stream that represent the file contents.</param>
    public StreamFileContent(Stream content) => Content = content;

    /// <summary>
    /// Gets the stream that represent the file contents.
    /// </summary>
    public Stream Content { get; }

    /// <summary>
    /// Gets or sets the content type. Its default value is an empty string.
    /// </summary>
    public string ContentType { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the file name. Its default value is an empty string.
    /// </summary>
    public string FileName { get; init; } = string.Empty;
}

/// <summary>
/// Represents the content of a file using an array of bytes.
/// </summary>
public class ByteArrayFileContent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ByteArrayFileContent"/> class 
    /// with the provided <paramref name="content"/>.
    /// </summary>
    /// <param name="content">The bytes that represent the file contents.</param>
    public ByteArrayFileContent(byte[] content) => Content = content;

    /// <summary>
    /// Gets the bytes that represent the file contents.
    /// </summary>
    public byte[] Content { get; }

    /// <summary>
    /// Gets or sets the content type. Its default value is an empty string.
    /// </summary>
    public string ContentType { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the file name. Its default value is an empty string.
    /// </summary>
    public string FileName { get; init; } = string.Empty;
}

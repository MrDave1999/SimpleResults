namespace SimpleResults.Example;

public class FileResultService
{
    public Result<ByteArrayFileContent> GetByteArray(string fileName)
    {
        if(string.IsNullOrWhiteSpace(fileName))
        {
            return Result.Invalid("FileName is required");
        }

        byte[] content = [1, 1, 0, 0];
        var byteArrayFileContent = new ByteArrayFileContent(content)
        {
            ContentType = MediaTypeNames.Application.Pdf,
            FileName    = fileName
        };
        return Result.File(byteArrayFileContent);
    }

    public Result<StreamFileContent> GetStream(string fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName))
        {
            return Result.Invalid("FileName is required");
        }

        byte[] buffer = [1, 1, 0, 0];
        Stream content = new MemoryStream(buffer);
        var streamFileContent = new StreamFileContent(content)
        {
            ContentType = MediaTypeNames.Application.Pdf,
            FileName    = fileName
        };
        return Result.File(streamFileContent);
    }
}

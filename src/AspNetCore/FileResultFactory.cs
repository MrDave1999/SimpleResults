using Microsoft.AspNetCore.Mvc;
using SimpleResults.Resources;

namespace SimpleResults;

internal class FileResultFactory
{
    public static FileContentResult CreateFileContentResult(ResultBase resultBase)
    {
        var result = resultBase as Result<ByteArrayFileContent> ?? 
            throw new InvalidOperationException(ResponseMessages.FailedConversion);

        var byteArrayFile = result.Data;
        var fileContent = new FileContentResult(byteArrayFile.Content, byteArrayFile.ContentType)
        {
            FileDownloadName = byteArrayFile.FileName
        };
        return fileContent;
    }

    public static FileStreamResult CreateFileStreamResult(ResultBase resultBase)
    {
        var result = resultBase as Result<StreamFileContent> ??
            throw new InvalidOperationException(ResponseMessages.FailedConversion);

        var streamFile = result.Data;
        var fileContent = new FileStreamResult(streamFile.Content, streamFile.ContentType)
        {
            FileDownloadName = streamFile.FileName
        };
        return fileContent;
    }

    public static IResult CreateFileContentHttpResult(ResultBase resultBase)
    {
        var result = resultBase as Result<ByteArrayFileContent> ??
            throw new InvalidOperationException(ResponseMessages.FailedConversion);

        var byteArrayFile = result.Data;
        var fileContent = Results.File(
            byteArrayFile.Content, 
            byteArrayFile.ContentType, 
            byteArrayFile.FileName);

        return fileContent;
    }

    public static IResult CreateFileStreamHttpResult(ResultBase resultBase)
    {
        var result = resultBase as Result<StreamFileContent> ??
            throw new InvalidOperationException(ResponseMessages.FailedConversion);

        var streamFile = result.Data;
        var fileContent = Results.File(
            streamFile.Content,
            streamFile.ContentType,
            streamFile.FileName);

        return fileContent;
    }
}

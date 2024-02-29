using Microsoft.AspNetCore.Mvc;

namespace SimpleResults;

internal class FileResultConverter
{
    public static FileContentResult ConvertToFileContentResult(ResultBase resultBase)
    {
        var result = resultBase as Result<ByteArrayFileContent>;
        if (result is null)
        {
            var typeName = typeof(FileContentResult).FullName;
            throw new InvalidOperationException(new FailedConversionError(typeName).Message);
        }

        var byteArrayFile = result.Data;
        var fileContent = new FileContentResult(byteArrayFile.Content, byteArrayFile.ContentType)
        {
            FileDownloadName = byteArrayFile.FileName
        };
        return fileContent;
    }

    public static FileStreamResult ConvertToFileStreamResult(ResultBase resultBase)
    {
        var result = resultBase as Result<StreamFileContent>;
        if (result is null)
        {
            var typeName = typeof(FileStreamResult).FullName;
            throw new InvalidOperationException(new FailedConversionError(typeName).Message);
        }

        var streamFile = result.Data;
        var fileContent = new FileStreamResult(streamFile.Content, streamFile.ContentType)
        {
            FileDownloadName = streamFile.FileName
        };
        return fileContent;
    }

    public static IResult ConvertToFileContentHttpResult(ResultBase resultBase)
    {
        var result = resultBase as Result<ByteArrayFileContent>;
        if (result is null)
        {
            var typeName = typeof(IResult).FullName;
            throw new InvalidOperationException(new FailedConversionError(typeName).Message);
        }

        var byteArrayFile = result.Data;
        var fileContent = Results.File(
            byteArrayFile.Content, 
            byteArrayFile.ContentType, 
            byteArrayFile.FileName);

        return fileContent;
    }

    public static IResult ConvertToFileStreamHttpResult(ResultBase resultBase)
    {
        var result = resultBase as Result<StreamFileContent>;
        if (result is null)
        {
            var typeName = typeof(IResult).FullName;
            throw new InvalidOperationException(new FailedConversionError(typeName).Message);
        }

        var streamFile = result.Data;
        var fileContent = Results.File(
            streamFile.Content,
            streamFile.ContentType,
            streamFile.FileName);

        return fileContent;
    }
}

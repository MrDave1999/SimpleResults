namespace SimpleResults.Example.AspNetCore.Controllers;

[Tags("FileResult WebApi")]
[Route("FileResult-WebApi")]
public class FileResultController
{
    private readonly FileResultService _fileResultService;

    public FileResultController(FileResultService fileResultService)
    {
        _fileResultService = fileResultService;
    }

    [SwaggerResponse(type: typeof(byte[]), statusCode: StatusCodes.Status200OK, contentTypes: MediaTypeNames.Application.Pdf)]
    [SwaggerResponse(type: typeof(Result), statusCode: StatusCodes.Status400BadRequest, contentTypes: MediaTypeNames.Application.Json)]
    [HttpGet("byte-array")]
    public Result<ByteArrayFileContent> GetByteArray([FromQuery]FileResultRequest request)
    {
        return _fileResultService.GetByteArray(request.FileName);
    }

    [SwaggerResponse(type: typeof(byte[]), statusCode: StatusCodes.Status200OK, contentTypes: MediaTypeNames.Application.Pdf)]
    [SwaggerResponse(type: typeof(Result), statusCode: StatusCodes.Status400BadRequest, contentTypes: MediaTypeNames.Application.Json)]
    [HttpGet("stream")]
    public Result<StreamFileContent> GetStream([FromQuery]FileResultRequest request)
    {
        return _fileResultService.GetStream(request.FileName);
    }
}

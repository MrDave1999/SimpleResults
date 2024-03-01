namespace SimpleResults.Example.AspNetCore.MinimalApi;

public static class FileResultEndpoint
{
    public static void AddFileResultRoutes(this WebApplication app)
    {
        var fileGroup = app
            .MapGroup("/FileResult-MinimalApi")
            .WithTags("FileResult MinimalApi")
            .AddEndpointFilter<TranslateResultToHttpResultFilter>();

        fileGroup.MapGet("/byte-array", ([AsParameters]FileResultRequest request, FileResultService service) =>
        {
            return service.GetByteArray(request.FileName);
        })
        .Produces<byte[]>(StatusCodes.Status200OK, MediaTypeNames.Application.Pdf)
        .Produces<Result>(StatusCodes.Status400BadRequest);

        fileGroup.MapGet("/stream", ([AsParameters]FileResultRequest request, FileResultService service) =>
        {
            return service.GetStream(request.FileName);
        })
        .Produces<byte[]>(StatusCodes.Status200OK, MediaTypeNames.Application.Pdf)
        .Produces<Result>(StatusCodes.Status400BadRequest);
    }
}

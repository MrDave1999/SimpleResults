namespace SimpleResults.Example.AspNetCore.MinimalApi;

public static class MessageEndpoint
{
    public static void AddMessageRoutes(this WebApplication app)
    {
        var messageGroup = app
            .MapGroup("/Message-MinimalApi")
            .WithTags("Message MinimalApi");

        messageGroup
            .MapGet("/ban", () => Result.Forbidden().ToHttpResult())
            .Produces<Result>();

        messageGroup
            .MapGet("/error", () => Result.CriticalError().ToHttpResult())
            .Produces<Result>();

        messageGroup
            .MapGet("/false-identity", () => Result.Unauthorized().ToHttpResult())
            .Produces<Result>();
    }
}

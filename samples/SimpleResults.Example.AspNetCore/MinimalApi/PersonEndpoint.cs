namespace SimpleResults.Example.AspNetCore.MinimalApi;

public static class PersonEndpoint
{
    public static void AddPersonRoutes(this WebApplication app)
    {
        var personGroup = app
            .MapGroup("/Person-MinimalApi")
            .WithTags("Person MinimalApi")
            .AddEndpointFilter<TranslateResultToHttpResultFilter>();

        personGroup.MapPost("/", async ([FromBody]Person person, PersonService service) =>
        {
            await Task.Delay(100);
            return service.Create(person);
        })
        .Produces<Result>();

        personGroup.MapGet("/", async (PersonService service) =>
        {
            await Task.Delay(100);
            return service.GetAll();
        })
        .Produces<ListedResult<Person>>();
    }
}

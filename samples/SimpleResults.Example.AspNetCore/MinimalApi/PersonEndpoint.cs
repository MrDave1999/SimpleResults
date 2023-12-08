namespace SimpleResults.Example.AspNetCore.MinimalApi;

public static class PersonEndpoint
{
    public static void AddPersonRoutes(this WebApplication app)
    {
        var userGroup = app
            .MapGroup("/Person-MinimalApi")
            .WithTags("Person MinimalApi");

        userGroup.MapPost("/", async ([FromBody]Person person, PersonService service) =>
        {
            await Task.Delay(100);
            return service
                .Create(person)
                .ToHttpResult();
        })
        .Produces<Result>();

        userGroup.MapGet("/", async (PersonService service) =>
        {
            await Task.Delay(100);
            return service
                .GetAll()
                .ToHttpResult();
        })
        .Produces<ListedResult<Person>>();
    }
}

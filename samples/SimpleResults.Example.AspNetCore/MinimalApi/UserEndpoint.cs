namespace SimpleResults.Example.AspNetCore.MinimalApi;

public static class UserEndpoint
{
    public static void AddUserRoutes(this WebApplication app)
    {
        var userGroup = app
            .MapGroup("/User-MinimalApi")
            .WithTags("User MinimalApi");

        userGroup.MapGet("/paged", ([AsParameters]PagedRequest request, UserService service) =>
        {
            return service
                .GetPagedList(request.PageNumber, request.PageSize)
                .ToHttpResult();
        })
        .Produces<PagedResult<User>>();

        userGroup.MapGet("/", (UserService service) =>
        {
            return service
                .GetAll()
                .ToHttpResult();
        })
        .Produces<ListedResult<User>>();

        userGroup.MapGet("/{id}", (string id, UserService service) =>
        {
            return service
                .GetById(id)
                .ToHttpResult();
        })
        .Produces<Result<User>>();

        userGroup.MapPost("/", ([FromBody]UserRequest request, UserService service) =>
        {
            return service
                .Create(request.Name)
                .ToHttpResult();
        })
        .Produces<Result<CreatedGuid>>();

        userGroup.MapPut("/{id}", (string id, [FromBody]UserRequest request, UserService service) =>
        {
            return service
                .Update(id, request.Name)
                .ToHttpResult();
        })
        .Produces<Result>();

        userGroup.MapDelete("/{id}", (string id, UserService service) =>
        {
            return service
                .Delete(id)
                .ToHttpResult();
        })
        .Produces<Result>();
    }
}

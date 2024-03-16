namespace SimpleResults.Example.Web.Tests.Features;

public class GetUsersTests
{
    [TestCase(Routes.User.WebApi)]
    [TestCase(Routes.User.MinimalApi)]
    public async Task Get_WhenUsersAreObtained_ShouldReturnsHttpStatusCodeOk(string requestUri)
    {
        // Arrange
        using var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();

        // Act
        var httpResponse = await client.GetAsync(requestUri);
        var result = await httpResponse
            .Content
            .ReadFromJsonAsync<ListedResult<User>>();

        // Asserts
        httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        result.Data.Should().NotBeNullOrEmpty();
        result.IsSuccess.Should().BeTrue();
        result.Message.Should().NotBeNullOrEmpty();
        result.Errors.Should().BeEmpty();
        result.Status.Should().Be(ResultStatus.Ok);
    }

    [TestCase(Routes.User.WebApi)]
    [TestCase(Routes.User.MinimalApi)]
    public async Task Get_WhenThereAreNoUsers_ShouldReturnsHttpStatusCodeUnprocessableEntity(string requestUri)
    {
        // Arrange
        using var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();
        var users = factory.Services.GetService<List<User>>();
        users.Clear();

        // Act
        var httpResponse = await client.GetAsync(requestUri);
        var result = await httpResponse
            .Content
            .ReadFromJsonAsync<ListedResult<User>>();

        // Asserts
        httpResponse.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        result.Data.Should().BeEmpty();
        result.IsSuccess.Should().BeFalse();
        result.Message.Should().NotBeNullOrEmpty();
        result.Errors.Should().BeEmpty();
        result.Status.Should().Be(ResultStatus.Failure);
    }
}

namespace SimpleResults.Example.Web.Tests.Features;

public class DeleteUserTests
{
    [TestCase(Routes.User.WebApi)]
    [TestCase(Routes.User.MinimalApi)]
    public async Task Delete_WhenUserIsDeleted_ShouldReturnsHttpStatusCodeOk(string requestUri)
    {
        // Arrange
        using var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();
        var users = factory.Services.GetService<List<User>>();
        var guid = users[0].Id;

        // Act
        var httpResponse = await client.DeleteAsync($"{requestUri}/{guid}");
        var result = await httpResponse
            .Content
            .ReadFromJsonAsync<Result>();

        // Asserts
        httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        result.IsSuccess.Should().BeTrue();
        result.Message.Should().NotBeNullOrEmpty();
        result.Errors.Should().BeEmpty();
        result.Status.Should().Be(ResultStatus.Ok);
    }

    [TestCase(Routes.User.WebApi)]
    [TestCase(Routes.User.MinimalApi)]
    public async Task Delete_WhenUserIsNotFound_ShouldReturnsHttpStatusCodeNotFound(string requestUri)
    {
        // Arrange
        using var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();

        // Act
        var httpResponse = await client.DeleteAsync($"{requestUri}/5000");
        var result = await httpResponse
            .Content
            .ReadFromJsonAsync<Result>();

        // Asserts
        httpResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        result.IsSuccess.Should().BeFalse();
        result.Message.Should().NotBeNullOrEmpty();
        result.Errors.Should().BeEmpty();
        result.Status.Should().Be(ResultStatus.NotFound);
    }
}

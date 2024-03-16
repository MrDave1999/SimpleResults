namespace SimpleResults.Example.Web.Tests.Features;

public class UpdateUserTests
{
    [TestCase(Routes.User.WebApi)]
    [TestCase(Routes.User.MinimalApi)]
    public async Task Put_WhenUserIsUpdated_ShouldReturnsHttpStatusCodeOk(string requestUri)
    {
        // Arrange
        using var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();
        var request = new UserRequest { Name = "Alice" };
        var users = factory.Services.GetService<List<User>>();
        var guid = users[0].Id;

        // Act
        var httpResponse = await client.PutAsJsonAsync($"{requestUri}/{guid}", request);
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
    public async Task Put_WhenUserIsNotFound_ShouldReturnsHttpStatusCodeNotFound(string requestUri)
    {
        // Arrange
        using var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();
        var request = new UserRequest { Name = "Alice" };

        // Act
        var httpResponse = await client.PutAsJsonAsync($"{requestUri}/5000", request);
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

    [TestCase(Routes.User.WebApi)]
    [TestCase(Routes.User.MinimalApi)]
    public async Task Put_WhenNameIsEmpty_ShouldReturnsHttpStatusCodeBadRequest(string requestUri)
    {
        // Arrange
        using var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();
        var request = new UserRequest { Name = string.Empty };

        // Act
        var httpResponse = await client.PutAsJsonAsync($"{requestUri}/5000", request);
        var result = await httpResponse
            .Content
            .ReadFromJsonAsync<Result>();

        // Asserts
        httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        result.IsSuccess.Should().BeFalse();
        result.Message.Should().NotBeNullOrEmpty();
        result.Errors.Should().BeEmpty();
        result.Status.Should().Be(ResultStatus.Invalid);
    }
}

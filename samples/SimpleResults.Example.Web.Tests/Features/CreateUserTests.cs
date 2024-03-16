namespace SimpleResults.Example.Web.Tests.Features;

public class CreateUserTests
{
    [TestCase(Routes.User.WebApi)]
    [TestCase(Routes.User.MinimalApi)]
    public async Task Post_WhenUserIsCreated_ShouldReturnsHttpStatusCodeCreated(string requestUri)
    {
        // Arrange
        using var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();
        var request = new UserRequest { Name = "Bob" };

        // Act
        var httpResponse = await client.PostAsJsonAsync(requestUri, request);
        var result = await httpResponse
            .Content
            .ReadFromJsonAsync<Result<CreatedGuid>>();

        // Asserts
        httpResponse.StatusCode.Should().Be(HttpStatusCode.Created);
        result.Data.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Message.Should().NotBeNullOrEmpty();
        result.Errors.Should().BeEmpty();
        result.Status.Should().Be(ResultStatus.Created);
    }

    [TestCase(Routes.User.WebApi)]
    [TestCase(Routes.User.MinimalApi)]
    public async Task Post_WhenNameIsEmpty_ShouldReturnsHttpStatusCodeBadRequest(string requestUri)
    {
        // Arrange
        using var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();
        var request = new UserRequest { Name = string.Empty };

        // Act
        var httpResponse = await client.PostAsJsonAsync(requestUri, request);
        var result = await httpResponse
            .Content
            .ReadFromJsonAsync<Result<CreatedGuid>>();

        // Asserts
        httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        result.Data.Should().BeNull();
        result.IsSuccess.Should().BeFalse();
        result.Message.Should().NotBeNullOrEmpty();
        result.Errors.Should().BeEmpty();
        result.Status.Should().Be(ResultStatus.Invalid);
    }
}

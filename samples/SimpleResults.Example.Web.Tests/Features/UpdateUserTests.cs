namespace SimpleResults.Example.Web.Tests.Features;

public class UpdateUserTests
{
    [Test]
    public async Task Put_WhenUserIsUpdated_ShouldReturnsHttpStatusCodeOk()
    {
        // Arrange
        using var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();
        var request = new UserRequest { Name = "Alice" };
        var users = factory.Services.GetService<List<User>>();
        var guid = users[0].Id;
        var requestUri = $"/User/{guid}";

        // Act
        var httpResponse = await client.PutAsJsonAsync(requestUri, request);
        var result = await httpResponse
            .Content
            .ReadFromJsonAsync<Result>();

        // Asserts
        httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        result.IsSuccess.Should().BeTrue();
        result.Message.Should().NotBeNullOrEmpty();
        result.Errors.Should().BeEmpty();
    }

    [Test]
    public async Task Put_WhenUserIsNotFound_ShouldReturnsHttpStatusCodeNotFound()
    {
        // Arrange
        using var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();
        var request = new UserRequest { Name = "Alice" };

        // Act
        var httpResponse = await client.PutAsJsonAsync("/User/5000", request);
        var result = await httpResponse
            .Content
            .ReadFromJsonAsync<Result>();

        // Asserts
        httpResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        result.IsSuccess.Should().BeFalse();
        result.Message.Should().NotBeNullOrEmpty();
        result.Errors.Should().BeEmpty();
    }

    [Test]
    public async Task Put_WhenNameIsEmpty_ShouldReturnsHttpStatusCodeBadRequest()
    {
        // Arrange
        using var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();
        var request = new UserRequest { Name = string.Empty };

        // Act
        var httpResponse = await client.PutAsJsonAsync("/User/5000", request);
        var result = await httpResponse
            .Content
            .ReadFromJsonAsync<Result>();

        // Asserts
        httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        result.IsSuccess.Should().BeFalse();
        result.Message.Should().NotBeNullOrEmpty();
        result.Errors.Should().BeEmpty();
    }
}

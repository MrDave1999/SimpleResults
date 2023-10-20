namespace SimpleResults.Example.Web.Tests.Features;

public class DeleteUserTests
{
    [Test]
    public async Task Delete_WhenUserIsDeleted_ShouldReturnsHttpStatusCodeOk()
    {
        // Arrange
        using var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();
        var users = factory.Services.GetService<List<User>>();
        var guid = users[0].Id;
        var requestUri = $"/User/{guid}";

        // Act
        var httpResponse = await client.DeleteAsync(requestUri);
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
    public async Task Delete_WhenUserIsNotFound_ShouldReturnsHttpStatusCodeNotFound()
    {
        // Arrange
        using var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();

        // Act
        var httpResponse = await client.DeleteAsync("/User/5000");
        var result = await httpResponse
            .Content
            .ReadFromJsonAsync<Result>();

        // Asserts
        httpResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        result.IsSuccess.Should().BeFalse();
        result.Message.Should().NotBeNullOrEmpty();
        result.Errors.Should().BeEmpty();
    }
}

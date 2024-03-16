namespace SimpleResults.Example.Web.Tests.Features;

public class GetMessageTests
{
    [TestCase(Routes.Message.WebApi)]
    [TestCase(Routes.Message.MinimalApi)]
    public async Task Get_WhenMessageIsBan_ShouldReturnsHttpStatusCodeForbidden(string requestUri)
    {
        // Arrange
        using var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();

        // Act
        var httpResponse = await client.GetAsync($"{requestUri}/ban");
        var result = await httpResponse
            .Content
            .ReadFromJsonAsync<Result>();

        // Asserts
        httpResponse.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        result.IsSuccess.Should().BeFalse();
        result.Message.Should().NotBeNullOrEmpty();
        result.Errors.Should().BeEmpty();
        result.Status.Should().Be(ResultStatus.Forbidden);
    }

    [TestCase(Routes.Message.WebApi)]
    [TestCase(Routes.Message.MinimalApi)]
    public async Task Get_WhenMessageIsError_ShouldReturnsHttpStatusCodeServerError(string requestUri)
    {
        // Arrange
        using var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();

        // Act
        var httpResponse = await client.GetAsync($"{requestUri}/error");
        var result = await httpResponse
            .Content
            .ReadFromJsonAsync<Result>();

        // Asserts
        httpResponse.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        result.IsSuccess.Should().BeFalse();
        result.Message.Should().NotBeNullOrEmpty();
        result.Errors.Should().BeEmpty();
        result.Status.Should().Be(ResultStatus.CriticalError);
    }

    [TestCase(Routes.Message.WebApi)]
    [TestCase(Routes.Message.MinimalApi)]
    public async Task Get_WhenMessageIsFalseIdentity_ShouldReturnsHttpStatusCodeUnauthorized(string requestUri)
    {
        // Arrange
        using var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();

        // Act
        var httpResponse = await client.GetAsync($"{requestUri}/false-identity");
        var result = await httpResponse
            .Content
            .ReadFromJsonAsync<Result>();

        // Asserts
        httpResponse.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        result.IsSuccess.Should().BeFalse();
        result.Message.Should().NotBeNullOrEmpty();
        result.Errors.Should().BeEmpty();
        result.Status.Should().Be(ResultStatus.Unauthorized);
    }
}

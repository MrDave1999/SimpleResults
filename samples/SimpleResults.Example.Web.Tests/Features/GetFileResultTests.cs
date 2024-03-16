namespace SimpleResults.Example.Web.Tests.Features;

public class GetFileResultTests
{
    [TestCase(Routes.File.ByteArrayController)]
    [TestCase(Routes.File.StreamController)]
    [TestCase(Routes.File.ByteArrayMinimalApi)]
    [TestCase(Routes.File.StreamMinimalApi)]
    public async Task Get_WhenBytesAreObtained_ShouldReturnsHttpStatusCodeOk(string route)
    {
        // Arrange
        using var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();
        byte[] expected = [1, 1, 0, 0];
        var fileName = "Report.pdf";
        var requestUri = $"{route}?fileName={fileName}";

        // Act
        var httpResponse = await client.GetAsync(requestUri);
        byte[] result = await httpResponse
            .Content
            .ReadAsByteArrayAsync();

        // Asserts
        httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        result.Should().BeEquivalentTo(expected);
    }

    [TestCase(Routes.File.ByteArrayController)]
    [TestCase(Routes.File.StreamController)]
    [TestCase(Routes.File.ByteArrayMinimalApi)]
    [TestCase(Routes.File.StreamMinimalApi)]
    public async Task Get_WhenFileNameIsEmpty_ShouldReturnsHttpStatusCodeBadRequest(string requestUri)
    {
        // Arrange
        using var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();

        // Act
        var httpResponse = await client.GetAsync(requestUri);
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

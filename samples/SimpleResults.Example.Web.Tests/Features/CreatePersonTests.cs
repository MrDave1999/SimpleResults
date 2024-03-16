namespace SimpleResults.Example.Web.Tests.Features;

public class CreatePersonTests
{
    [TestCase(Routes.Person.WebApi)]
    [TestCase(Routes.Person.MinimalApi)]
    public async Task Post_WhenPersonIsCreated_ShouldReturnsHttpStatusCodeCreated(string requestUri)
    {
        // Arrange
        using var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();
        var request = new Person 
        { 
            FirstName = "Alice",
            LastName = "Smith"
        };

        // Act
        var httpResponse = await client.PostAsJsonAsync(requestUri, request);
        var result = await httpResponse
            .Content
            .ReadFromJsonAsync<Result>();

        // Asserts
        httpResponse.StatusCode.Should().Be(HttpStatusCode.Created);
        result.IsSuccess.Should().BeTrue();
        result.Message.Should().NotBeNullOrEmpty();
        result.Errors.Should().BeEmpty();
        result.Status.Should().Be(ResultStatus.Created);
    }

    [TestCase(Routes.Person.WebApi)]
    [TestCase(Routes.Person.MinimalApi)]
    public async Task Post_WhenPropertiesAreEmpty_ShouldReturnsHttpStatusCodeBadRequest(string requestUri)
    {
        // Arrange
        using var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();
        var request = new Person
        {
            FirstName = string.Empty,
            LastName = default
        };

        // Act
        var httpResponse = await client.PostAsJsonAsync(requestUri, request);
        var result = await httpResponse
            .Content
            .ReadFromJsonAsync<Result>();

        // Asserts
        httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        result.IsSuccess.Should().BeFalse();
        result.Message.Should().NotBeNullOrEmpty();
        result.Errors.Should().NotBeNullOrEmpty();
        result.Status.Should().Be(ResultStatus.Invalid);
    }
}

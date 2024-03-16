namespace SimpleResults.Example.Web.Tests.Features;

public class GetPersonsTests
{
    [TestCase(Routes.Person.WebApi)]
    [TestCase(Routes.Person.MinimalApi)]
    public async Task Get_WhenPersonsAreObtained_ShouldReturnsHttpStatusCodeOk(string requestUri)
    {
        // Arrange
        using var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();
        var persons = factory.Services.GetService<List<Person>>();
        persons.Add(new Person { FirstName = "Alice", LastName = "Smith" });

        // Act
        var httpResponse = await client.GetAsync(requestUri);
        var result = await httpResponse
            .Content
            .ReadFromJsonAsync<ListedResult<Person>>();

        // Asserts
        httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        result.Data.Should().NotBeNullOrEmpty();
        result.IsSuccess.Should().BeTrue();
        result.Message.Should().NotBeNullOrEmpty();
        result.Errors.Should().BeEmpty();
        result.Status.Should().Be(ResultStatus.Ok);
    }

    [TestCase(Routes.Person.WebApi)]
    [TestCase(Routes.Person.MinimalApi)]
    public async Task Get_WhenThereAreNoPersons_ShouldReturnsHttpStatusCodeUnprocessableEntity(string requestUri)
    {
        // Arrange
        using var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();
        var persons = factory.Services.GetService<List<Person>>();
        persons.Clear();

        // Act
        var httpResponse = await client.GetAsync(requestUri);
        var result = await httpResponse
            .Content
            .ReadFromJsonAsync<ListedResult<Person>>();

        // Asserts
        httpResponse.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        result.Data.Should().BeEmpty();
        result.IsSuccess.Should().BeFalse();
        result.Message.Should().NotBeNullOrEmpty();
        result.Errors.Should().BeEmpty();
        result.Status.Should().Be(ResultStatus.Failure);
    }
}

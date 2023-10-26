namespace SimpleResults.Example.Web.Tests.Features;

public class GetPersonsTests
{
    [Test]
    public async Task Get_WhenPersonsAreObtained_ShouldReturnsHttpStatusCodeOk()
    {
        // Arrange
        using var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();
        var persons = factory.Services.GetService<List<Person>>();
        persons.Add(new Person { FirstName = "Alice", LastName = "Smith" });

        // Act
        var httpResponse = await client.GetAsync("/Person");
        var result = await httpResponse
            .Content
            .ReadFromJsonAsync<ListedResult<Person>>();

        // Asserts
        httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        result.Data.Should().NotBeNullOrEmpty();
        result.IsSuccess.Should().BeTrue();
        result.Message.Should().NotBeNullOrEmpty();
        result.Errors.Should().BeEmpty();
    }

    [Test]
    public async Task Get_WhenThereAreNoPersons_ShouldReturnsHttpStatusCodeUnprocessableEntity()
    {
        // Arrange
        using var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();
        var persons = factory.Services.GetService<List<Person>>();
        persons.Clear();

        // Act
        var httpResponse = await client.GetAsync("/Person");
        var result = await httpResponse
            .Content
            .ReadFromJsonAsync<ListedResult<Person>>();

        // Asserts
        httpResponse.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        result.Data.Should().BeEmpty();
        result.IsSuccess.Should().BeFalse();
        result.Message.Should().NotBeNullOrEmpty();
        result.Errors.Should().BeEmpty();
    }
}

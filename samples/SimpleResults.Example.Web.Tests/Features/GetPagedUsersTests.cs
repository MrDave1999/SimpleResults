namespace SimpleResults.Example.Web.Tests.Features;

public class GetPagedUsersTests
{
    [Test]
    public async Task Get_WhenPaginatedListIsObtained_ShouldReturnsHttpStatusCodeOk()
    {
        // Arrange
        using var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();
        var requestUri = "/User/paged?pageNumber=1&pageSize=3";

        // Act
        var httpResponse = await client.GetAsync(requestUri);
        var result = await httpResponse
            .Content
            .ReadFromJsonAsync<PagedResult<User>>();

        // Asserts
        httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        result.Data.Should().NotBeNullOrEmpty();
        result.PagedInfo.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Message.Should().NotBeNullOrEmpty();
        result.Errors.Should().BeEmpty();
    }

    [Test]
    public async Task Get_WhenNoResultsFound_ShouldReturnsHttpStatusCodeUnprocessableEntity()
    {
        // Arrange
        using var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();
        var requestUri = "/User/paged?pageNumber=10&pageSize=3";

        // Act
        var httpResponse = await client.GetAsync(requestUri);
        var result = await httpResponse
            .Content
            .ReadFromJsonAsync<PagedResult<User>>();

        // Asserts
        httpResponse.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        result.Data.Should().BeEmpty();
        result.PagedInfo.Should().BeNull();
        result.IsSuccess.Should().BeFalse();
        result.Message.Should().NotBeNullOrEmpty();
        result.Errors.Should().BeEmpty();
    }

    [Test]
    public async Task Get_WhenPageNumberIsZero_ShouldReturnsHttpStatusCodeBadRequest()
    {
        // Arrange
        using var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();
        var requestUri = "/User/paged?pageNumber=0&pageSize=3";

        // Act
        var httpResponse = await client.GetAsync(requestUri);
        var result = await httpResponse
            .Content
            .ReadFromJsonAsync<PagedResult<User>>();

        // Asserts
        httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        result.Data.Should().BeEmpty();
        result.PagedInfo.Should().BeNull();
        result.IsSuccess.Should().BeFalse();
        result.Message.Should().NotBeNullOrEmpty();
        result.Errors.Should().BeEmpty();
    }
}

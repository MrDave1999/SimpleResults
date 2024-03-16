namespace SimpleResults.Example.Web.Tests.Features;

public class GetPagedUsersTests
{
    [TestCase(Routes.User.WebApi)]
    [TestCase(Routes.User.MinimalApi)]
    public async Task Get_WhenPaginatedListIsObtained_ShouldReturnsHttpStatusCodeOk(string requestUri)
    {
        // Arrange
        using var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();
        requestUri = $"{requestUri}/paged?pageNumber=1&pageSize=3";

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
        result.Status.Should().Be(ResultStatus.Ok);
    }

    [TestCase(Routes.User.WebApi)]
    [TestCase(Routes.User.MinimalApi)]
    public async Task Get_WhenNoResultsFound_ShouldReturnsHttpStatusCodeUnprocessableEntity(string requestUri)
    {
        // Arrange
        using var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();
        requestUri = $"{requestUri}/paged?pageNumber=10&pageSize=3";

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
        result.Status.Should().Be(ResultStatus.Failure);
    }

    [TestCase(Routes.User.WebApi)]
    [TestCase(Routes.User.MinimalApi)]
    public async Task Get_WhenPageNumberIsZero_ShouldReturnsHttpStatusCodeBadRequest(string requestUri)
    {
        // Arrange
        using var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();
        requestUri = $"{requestUri}/paged?pageNumber=0&pageSize=3";

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
        result.Status.Should().Be(ResultStatus.Invalid);
    }
}

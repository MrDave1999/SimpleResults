namespace SimpleResults.Tests.AspNetCore;

public partial class ToActionResultTests
{
    [Test]
    public void ToActionResult_WhenOperationResultIsSuccess_ShouldReturnsOkObjectResult()
    {
        // Arrange
        var expectedData = new Person { Name = "Bob" };
        int expectedHttpCode = 200;
        Result<Person> result = Result.Success(expectedData);

        // Act
        ActionResult<Result<Person>> actionResult = result.ToActionResult();
        var contentResult = actionResult.Result as OkObjectResult;
        var actualValue = (Result<Person>)contentResult.Value;

        // Asserts
        contentResult.StatusCode.Should().Be(expectedHttpCode);
        actualValue.IsSuccess.Should().BeTrue();
        actualValue.IsFailed.Should().BeFalse();
        actualValue.Message.Should().Be(ResponseMessages.Success);
        actualValue.Errors.Should().BeEmpty();
        actualValue.Data.Should().BeEquivalentTo(expectedData);
        actualValue.Status.Should().Be(ResultStatus.Ok);
    }

    [Test]
    public void ToActionResult_WhenOperationResultIsCreatedWithId_ShouldReturnsCreatedResult()
    {
        // Arrange
        var expectedData = new CreatedId { Id = 1 };
        int expectedHttpCode = 201;
        Result<CreatedId> result = Result.CreatedResource(expectedData.Id);

        // Act
        ActionResult<Result<CreatedId>> actionResult = result.ToActionResult();
        var contentResult = actionResult.Result as CreatedResult;
        var actualValue = (Result<CreatedId>)contentResult.Value;

        // Asserts
        contentResult.StatusCode.Should().Be(expectedHttpCode);
        actualValue.IsSuccess.Should().BeTrue();
        actualValue.IsFailed.Should().BeFalse();
        actualValue.Message.Should().Be(ResponseMessages.CreatedResource);
        actualValue.Errors.Should().BeEmpty();
        actualValue.Data.Should().BeEquivalentTo(expectedData);
        actualValue.Status.Should().Be(ResultStatus.Created);
    }

    [Test]
    public void ToActionResult_WhenOperationResultIsCreatedWithGuid_ShouldReturnsCreatedResult()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var expectedData = new CreatedGuid { Id = guid.ToString() };
        int expectedHttpCode = 201;
        Result<CreatedGuid> result = Result.CreatedResource(guid);

        // Act
        ActionResult<Result<CreatedGuid>> actionResult = result.ToActionResult();
        var contentResult = actionResult.Result as CreatedResult;
        var actualValue = (Result<CreatedGuid>)contentResult.Value;

        // Asserts
        contentResult.StatusCode.Should().Be(expectedHttpCode);
        actualValue.IsSuccess.Should().BeTrue();
        actualValue.IsFailed.Should().BeFalse();
        actualValue.Message.Should().Be(ResponseMessages.CreatedResource);
        actualValue.Errors.Should().BeEmpty();
        actualValue.Data.Should().BeEquivalentTo(expectedData);
        actualValue.Status.Should().Be(ResultStatus.Created);
    }

    [Test]
    public void ToActionResult_WhenOperationResultIsUpdated_ShouldReturnsOkObjectResult()
    {
        // Arrange
        int expectedHttpCode = 200;
        Result result = Result.UpdatedResource();

        // Act
        ActionResult<Result> actionResult = result.ToActionResult();
        var contentResult = actionResult.Result as OkObjectResult;
        var actualValue = (Result)contentResult.Value;

        // Asserts
        contentResult.StatusCode.Should().Be(expectedHttpCode);
        actualValue.IsSuccess.Should().BeTrue();
        actualValue.IsFailed.Should().BeFalse();
        actualValue.Message.Should().Be(ResponseMessages.UpdatedResource);
        actualValue.Errors.Should().BeEmpty();
        actualValue.Status.Should().Be(ResultStatus.Ok);
    }

    [Test]
    public void ToActionResult_WhenOperationResultIsDeleted_ShouldReturnsOkObjectResult()
    {
        // Arrange
        int expectedHttpCode = 200;
        Result result = Result.DeletedResource();

        // Act
        ActionResult<Result> actionResult = result.ToActionResult();
        var contentResult = actionResult.Result as OkObjectResult;
        var actualValue = (Result)contentResult.Value;

        // Asserts
        contentResult.StatusCode.Should().Be(expectedHttpCode);
        actualValue.IsSuccess.Should().BeTrue();
        actualValue.IsFailed.Should().BeFalse();
        actualValue.Message.Should().Be(ResponseMessages.DeletedResource);
        actualValue.Errors.Should().BeEmpty();
        actualValue.Status.Should().Be(ResultStatus.Ok);
    }

    [Test]
    public void ToActionResult_WhenOperationResultIsObtained_ShouldReturnsOkObjectResult()
    {
        // Arrange
        var expectedData = new Person { Name = "Bob" };
        int expectedHttpCode = 200;
        Result<Person> result = Result.ObtainedResource(expectedData);

        // Act
        ActionResult<Result<Person>> actionResult = result.ToActionResult();
        var contentResult = actionResult.Result as OkObjectResult;
        var actualValue = (Result<Person>)contentResult.Value;

        // Asserts
        contentResult.StatusCode.Should().Be(expectedHttpCode);
        actualValue.IsSuccess.Should().BeTrue();
        actualValue.IsFailed.Should().BeFalse();
        actualValue.Message.Should().Be(ResponseMessages.ObtainedResource);
        actualValue.Errors.Should().BeEmpty();
        actualValue.Data.Should().Be(expectedData);
        actualValue.Status.Should().Be(ResultStatus.Ok);
    }

    [Test]
    public void ToActionResult_WhenOperationResultIsNotSupported_ShouldThrowNotSupportedException()
    {
        // Arrange
        var result = new Result<Person> { Status = (ResultStatus)5000 };
        var expectedMessage = new UnsupportedStatusError(result.Status).Message;

        // Act
        Action act = () => result.ToActionResult();

        // Assert
        act.Should()
           .Throw<NotSupportedException>()
           .WithMessage(expectedMessage);
    }

    [Test]
    public void ToActionResult_WhenTypeIsListedResultOfT_ShouldReturnsActionResultObject()
    {
        // Arrange
        int expectedHttpCode = 200;
        var expectedData = new List<Person>();
        ListedResult<Person> result = Result.Success(expectedData);

        // Act
        ActionResult<ListedResult<Person>> actionResult = result.ToActionResult();
        var contentResult = actionResult.Result as OkObjectResult;
        var actualValue = (ListedResult<Person>)contentResult.Value;

        // Asserts
        contentResult.StatusCode.Should().Be(expectedHttpCode);
        actualValue.IsSuccess.Should().BeTrue();
        actualValue.IsFailed.Should().BeFalse();
        actualValue.Message.Should().Be(ResponseMessages.Success);
        actualValue.Errors.Should().BeEmpty();
        actualValue.Data.Should().BeEquivalentTo(expectedData);
        actualValue.Status.Should().Be(ResultStatus.Ok);
    }

    [Test]
    public void ToActionResult_WhenTypeIsPagedResultOfT_ShouldReturnsActionResultObject()
    {
        // Arrange
        int expectedHttpCode = 200;
        var expectedData = new List<Person>();
        var expectedPagedInfo = new PagedInfo(1, 10, 5);
        PagedResult<Person> result = Result.Success(expectedData, expectedPagedInfo);

        // Act
        ActionResult<PagedResult<Person>> actionResult = result.ToActionResult();
        var contentResult = actionResult.Result as OkObjectResult;
        var actualValue = (PagedResult<Person>)contentResult.Value;

        // Asserts
        contentResult.StatusCode.Should().Be(expectedHttpCode);
        actualValue.IsSuccess.Should().BeTrue();
        actualValue.IsFailed.Should().BeFalse();
        actualValue.Message.Should().Be(ResponseMessages.ObtainedResources);
        actualValue.Errors.Should().BeEmpty();
        actualValue.Data.Should().BeEquivalentTo(expectedData);
        actualValue.PagedInfo.Should().BeEquivalentTo(expectedPagedInfo);
        actualValue.Status.Should().Be(ResultStatus.Ok);
    }
}

namespace SimpleResults.Tests;

public class ResultExtensionsTests
{
    public ResultExtensionsTests()
    {
        Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");
    }

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
    public void ToActionResult_WhenOperationResultIsInvalid_ShouldReturnsBadRequestObjectResult()
    {
        // Arrange
        int expectedHttpCode = 400;
        Result<Person> result = Result.Invalid();

        // Act
        ActionResult<Result<Person>> actionResult = result.ToActionResult();
        var contentResult = actionResult.Result as BadRequestObjectResult;
        var actualValue = (Result<Person>)contentResult.Value;

        // Asserts
        contentResult.StatusCode.Should().Be(expectedHttpCode);
        actualValue.IsSuccess.Should().BeFalse();
        actualValue.IsFailed.Should().BeTrue();
        actualValue.Message.Should().Be(ResponseMessages.Invalid);
        actualValue.Errors.Should().BeEmpty();
        actualValue.Data.Should().BeNull();
        actualValue.Status.Should().Be(ResultStatus.Invalid);
    }

    [Test]
    public void ToActionResult_WhenOperationResultIsNotFound_ShouldReturnsNotFoundObjectResult()
    {
        // Arrange
        int expectedHttpCode = 404;
        Result<Person> result = Result.NotFound();

        // Act
        ActionResult<Result<Person>> actionResult = result.ToActionResult();
        var contentResult = actionResult.Result as NotFoundObjectResult;
        var actualValue = (Result<Person>)contentResult.Value;

        // Asserts
        contentResult.StatusCode.Should().Be(expectedHttpCode);
        actualValue.IsSuccess.Should().BeFalse();
        actualValue.IsFailed.Should().BeTrue();
        actualValue.Message.Should().Be(ResponseMessages.NotFound);
        actualValue.Errors.Should().BeEmpty();
        actualValue.Data.Should().BeNull();
        actualValue.Status.Should().Be(ResultStatus.NotFound);
    }

    [Test]
    public void ToActionResult_WhenOperationResultIsUnauthorized_ShouldReturnsUnauthorizedObjectResult()
    {
        // Arrange
        int expectedHttpCode = 401;
        Result<Person> result = Result.Unauthorized();

        // Act
        ActionResult<Result<Person>> actionResult = result.ToActionResult();
        var contentResult = actionResult.Result as UnauthorizedObjectResult;
        var actualValue = (Result<Person>)contentResult.Value;

        // Asserts
        contentResult.StatusCode.Should().Be(expectedHttpCode);
        actualValue.IsSuccess.Should().BeFalse();
        actualValue.IsFailed.Should().BeTrue();
        actualValue.Message.Should().Be(ResponseMessages.Unauthorized);
        actualValue.Errors.Should().BeEmpty();
        actualValue.Data.Should().BeNull();
        actualValue.Status.Should().Be(ResultStatus.Unauthorized);
    }

    [Test]
    public void ToActionResult_WhenOperationResultIsConflict_ShouldReturnsConflictObjectResult()
    {
        // Arrange
        int expectedHttpCode = 409;
        Result<Person> result = Result.Conflict();

        // Act
        ActionResult<Result<Person>> actionResult = result.ToActionResult();
        var contentResult = actionResult.Result as ConflictObjectResult;
        var actualValue = (Result<Person>)contentResult.Value;

        // Asserts
        contentResult.StatusCode.Should().Be(expectedHttpCode);
        actualValue.IsSuccess.Should().BeFalse();
        actualValue.IsFailed.Should().BeTrue();
        actualValue.Message.Should().Be(ResponseMessages.Conflict);
        actualValue.Errors.Should().BeEmpty();
        actualValue.Data.Should().BeNull();
        actualValue.Status.Should().Be(ResultStatus.Conflict);
    }

    [Test]
    public void ToActionResult_WhenOperationResultIsFailure_ShouldReturnsUnprocessableContentResult()
    {
        // Arrange
        int expectedHttpCode = 422;
        Result<Person> result = Result.Failure();

        // Act
        ActionResult<Result<Person>> actionResult = result.ToActionResult();
        var contentResult = actionResult.Result as UnprocessableContentResult;
        var actualValue = (Result<Person>)contentResult.Value;

        // Asserts
        contentResult.StatusCode.Should().Be(expectedHttpCode);
        actualValue.IsSuccess.Should().BeFalse();
        actualValue.IsFailed.Should().BeTrue();
        actualValue.Message.Should().Be(ResponseMessages.Failure);
        actualValue.Errors.Should().BeEmpty();
        actualValue.Data.Should().BeNull();
        actualValue.Status.Should().Be(ResultStatus.Failure);
    }

    [Test]
    public void ToActionResult_WhenOperationResultIsCriticalError_ShouldReturnsInternalServerErrorResult()
    {
        // Arrange
        int expectedHttpCode = 500;
        Result<Person> result = Result.CriticalError();

        // Act
        ActionResult<Result<Person>> actionResult = result.ToActionResult();
        var contentResult = actionResult.Result as InternalServerErrorResult;
        var actualValue = (Result<Person>)contentResult.Value;

        // Asserts
        contentResult.StatusCode.Should().Be(expectedHttpCode);
        actualValue.IsSuccess.Should().BeFalse();
        actualValue.IsFailed.Should().BeTrue();
        actualValue.Message.Should().Be(ResponseMessages.CriticalError);
        actualValue.Errors.Should().BeEmpty();
        actualValue.Data.Should().BeNull();
        actualValue.Status.Should().Be(ResultStatus.CriticalError);
    }

    [Test]
    public void ToActionResult_WhenOperationResultIsNotSupported_ShouldThrowNotSupportedException()
    {
        // Arrange
        var result = new Result<Person> { Status = (ResultStatus)5000 };

        // Act
        Action act = () => result.ToActionResult();

        // Assert
        act.Should().Throw<NotSupportedException>();
    }
}

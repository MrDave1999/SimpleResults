namespace SimpleResults.Tests.AspNetCore;

public partial class ToActionResultTests
{
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
    public void ToActionResult_WhenOperationResultIsForbidden_ShouldReturnsForbiddenResult()
    {
        // Arrange
        int expectedHttpCode = 403;
        Result<Person> result = Result.Forbidden();

        // Act
        ActionResult<Result<Person>> actionResult = result.ToActionResult();
        var contentResult = actionResult.Result as ForbiddenResult;
        var actualValue = (Result<Person>)contentResult.Value;

        // Asserts
        contentResult.StatusCode.Should().Be(expectedHttpCode);
        actualValue.IsSuccess.Should().BeFalse();
        actualValue.IsFailed.Should().BeTrue();
        actualValue.Message.Should().Be(ResponseMessages.Forbidden);
        actualValue.Errors.Should().BeEmpty();
        actualValue.Data.Should().BeNull();
        actualValue.Status.Should().Be(ResultStatus.Forbidden);
    }
}

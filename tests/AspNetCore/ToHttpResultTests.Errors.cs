namespace SimpleResults.Tests.AspNetCore;

public partial class ToHttpResultTests
{
    [Test]
    public void ToHttpResult_WhenOperationResultIsInvalid_ShouldReturnsHttpResultsBadRequest()
    {
        // Arrange
        int expectedHttpCode = 400;
        Result<Person> result = Result.Invalid();

        // Act
        IResult httpResult = result.ToHttpResult();
        var contentResult = httpResult as BadRequest<ResultBase>;
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
    public void ToHttpResult_WhenOperationResultIsNotFound_ShouldReturnsHttpResultsNotFound()
    {
        // Arrange
        int expectedHttpCode = 404;
        Result<Person> result = Result.NotFound();

        // Act
        IResult httpResult = result.ToHttpResult();
        var contentResult = httpResult as NotFound<ResultBase>;
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
    public void ToHttpResult_WhenOperationResultIsUnauthorized_ShouldReturnsHttpResultsUnauthorized()
    {
        // Arrange
        int expectedHttpCode = 401;
        Result<Person> result = Result.Unauthorized();

        // Act
        IResult httpResult = result.ToHttpResult();
        var contentResult = httpResult as UnauthorizedHttpResult;
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
    public void ToHttpResult_WhenOperationResultIsConflict_ShouldReturnsHttpResultsConflict()
    {
        // Arrange
        int expectedHttpCode = 409;
        Result<Person> result = Result.Conflict();

        // Act
        IResult httpResult = result.ToHttpResult();
        var contentResult = httpResult as Conflict<ResultBase>;
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
    public void ToHttpResult_WhenOperationResultIsFailure_ShouldReturnsHttpResultsUnprocessableEntity()
    {
        // Arrange
        int expectedHttpCode = 422;
        Result<Person> result = Result.Failure();

        // Act
        IResult httpResult = result.ToHttpResult();
        var contentResult = httpResult as UnprocessableEntity<ResultBase>;
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
    public void ToHttpResult_WhenOperationResultIsCriticalError_ShouldReturnsHttpResultsInternalServerError()
    {
        // Arrange
        int expectedHttpCode = 500;
        Result<Person> result = Result.CriticalError();

        // Act
        IResult httpResult = result.ToHttpResult();
        var contentResult = httpResult as InternalServerErrorHttpResult;
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
    public void ToHttpResult_WhenOperationResultIsForbidden_ShouldReturnsHttpResultsForbidden()
    {
        // Arrange
        int expectedHttpCode = 403;
        Result<Person> result = Result.Forbidden();

        // Act
        IResult httpResult = result.ToHttpResult();
        var contentResult = httpResult as ForbiddenHttpResult;
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

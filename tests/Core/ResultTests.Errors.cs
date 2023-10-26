namespace SimpleResults.Tests.Core;

public partial class ResultTests
{
    [Test]
    public void Failure_WhenResultIsFailure_ShouldReturnsResultObject()
    {
        // Arrange
        var expectedMessage = "Failure";
        var expectedErrors = new[] { "error" };

        // Act
        Result actual = Result.Failure(expectedMessage, expectedErrors);

        // Asserts
        actual.IsSuccess.Should().BeFalse();
        actual.IsFailed.Should().BeTrue();
        actual.Message.Should().Be(expectedMessage);
        actual.Errors.Should().BeEquivalentTo(expectedErrors);
        actual.Status.Should().Be(ResultStatus.Failure);
    }

    [Test]
    public void Invalid_WhenResultIsInvalid_ShouldReturnsResultObject()
    {
        // Arrange
        var expectedMessage = "Invalid";
        var expectedErrors = new[] { "error" };

        // Act
        Result actual = Result.Invalid(expectedMessage, expectedErrors);

        // Asserts
        actual.IsSuccess.Should().BeFalse();
        actual.IsFailed.Should().BeTrue();
        actual.Message.Should().Be(expectedMessage);
        actual.Errors.Should().BeEquivalentTo(expectedErrors);
        actual.Status.Should().Be(ResultStatus.Invalid);
    }

    [Test]
    public void NotFound_WhenResultIsNotFound_ShouldReturnsResultObject()
    {
        // Arrange
        var expectedMessage = "NotFound";
        var expectedErrors = new[] { "error" };

        // Act
        Result actual = Result.NotFound(expectedMessage, expectedErrors);

        // Asserts
        actual.IsSuccess.Should().BeFalse();
        actual.IsFailed.Should().BeTrue();
        actual.Message.Should().Be(expectedMessage);
        actual.Errors.Should().BeEquivalentTo(expectedErrors);
        actual.Status.Should().Be(ResultStatus.NotFound);
    }

    [Test]
    public void Unauthorized_WhenResultIsUnauthorized_ShouldReturnsResultObject()
    {
        // Arrange
        var expectedMessage = "Unauthorized";
        var expectedErrors = new[] { "error" };

        // Act
        Result actual = Result.Unauthorized(expectedMessage, expectedErrors);

        // Asserts
        actual.IsSuccess.Should().BeFalse();
        actual.IsFailed.Should().BeTrue();
        actual.Message.Should().Be(expectedMessage);
        actual.Errors.Should().BeEquivalentTo(expectedErrors);
        actual.Status.Should().Be(ResultStatus.Unauthorized);
    }

    [Test]
    public void Conflict_WhenResultIsConflict_ShouldReturnsResultObject()
    {
        // Arrange
        var expectedMessage = "Conflict";
        var expectedErrors = new[] { "error" };

        // Act
        Result actual = Result.Conflict(expectedMessage, expectedErrors);

        // Asserts
        actual.IsSuccess.Should().BeFalse();
        actual.IsFailed.Should().BeTrue();
        actual.Message.Should().Be(expectedMessage);
        actual.Errors.Should().BeEquivalentTo(expectedErrors);
        actual.Status.Should().Be(ResultStatus.Conflict);
    }

    [Test]
    public void CriticalError_WhenResultIsCriticalError_ShouldReturnsResultObject()
    {
        // Arrange
        var expectedMessage = "CriticalError";
        var expectedErrors = new[] { "error" };

        // Act
        Result actual = Result.CriticalError(expectedMessage, expectedErrors);

        // Asserts
        actual.IsSuccess.Should().BeFalse();
        actual.IsFailed.Should().BeTrue();
        actual.Message.Should().Be(expectedMessage);
        actual.Errors.Should().BeEquivalentTo(expectedErrors);
        actual.Status.Should().Be(ResultStatus.CriticalError);
     }

    [Test]
    public void Forbidden_WhenResultIsForbidden_ShouldReturnsResultObject()
    {
        // Arrange
        var expectedMessage = "Forbidden";
        var expectedErrors = new[] { "error" };

        // Act
        Result actual = Result.Forbidden(expectedMessage, expectedErrors);

        // Asserts
        actual.IsSuccess.Should().BeFalse();
        actual.IsFailed.Should().BeTrue();
        actual.Message.Should().Be(expectedMessage);
        actual.Errors.Should().BeEquivalentTo(expectedErrors);
        actual.Status.Should().Be(ResultStatus.Forbidden);
    }
}

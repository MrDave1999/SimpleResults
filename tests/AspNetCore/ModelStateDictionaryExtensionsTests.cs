namespace SimpleResults.Tests.AspNetCore;

public class ModelStateDictionaryExtensionsTests
{
    [Test]
    public void IsFailed_WhenModelStateIsInvalid_ShouldReturnsTrue()
    {
        // Arrange
        var modelState = new ModelStateDictionary();
        modelState.AddModelError("Id", "Id is required.");

        // Act
        bool actual = modelState.IsFailed();

        // Assert
        actual.Should().BeTrue();
    }

    [Test]
    public void IsFailed_WhenModelStateIsValid_ShouldReturnsFalse()
    {
        // Arrange
        var modelState = new ModelStateDictionary();

        // Act
        bool actual = modelState.IsFailed();

        // Assert
        actual.Should().BeFalse();
    }

    [Test]
    public void Invalid_WhenResultIsInvalidWithoutMessage_ShouldReturnsResultObject()
    {
        // Arrange
        var expectedMessage = ResponseMessages.ValidationErrors;
        var modelState = new ModelStateDictionary();
        modelState.AddModelError("Id",   "Id is required.");
        modelState.AddModelError("Id",   "Id must be greater than 0.");
        modelState.AddModelError("Name", "Name is required.");
        modelState.AddModelError("Name", "Name must have a minimum of 10 characters.");
        var expectedErrors = new[]
        {
            "'Id' property failed validation. Error was: Id is required.",
            "'Id' property failed validation. Error was: Id must be greater than 0.",
            "'Name' property failed validation. Error was: Name is required.",
            "'Name' property failed validation. Error was: Name must have a minimum of 10 characters."
        };

        // Act
        Result actual = modelState.Invalid();

        // Asserts
        actual.IsSuccess.Should().BeFalse();
        actual.IsFailed.Should().BeTrue();
        actual.Message.Should().Be(expectedMessage);
        actual.Errors.Should().BeEquivalentTo(expectedErrors);
        actual.Status.Should().Be(ResultStatus.Invalid);
    }

    [Test]
    public void Invalid_WhenResultIsInvalidWithMessage_ShouldReturnsResultObject()
    {
        // Arrange
        var expectedMessage = "Error";
        var modelState = new ModelStateDictionary();
        modelState.AddModelError("Id",   "Id is required.");
        modelState.AddModelError("Id",   "Id must be greater than 0.");
        modelState.AddModelError("Name", "Name is required.");
        modelState.AddModelError("Name", "Name must have a minimum of 10 characters.");
        var expectedErrors = new[]
        {
            "'Id' property failed validation. Error was: Id is required.",
            "'Id' property failed validation. Error was: Id must be greater than 0.",
            "'Name' property failed validation. Error was: Name is required.",
            "'Name' property failed validation. Error was: Name must have a minimum of 10 characters."
        };

        // Act
        Result actual = modelState.Invalid(expectedMessage);

        // Asserts
        actual.IsSuccess.Should().BeFalse();
        actual.IsFailed.Should().BeTrue();
        actual.Message.Should().Be(expectedMessage);
        actual.Errors.Should().BeEquivalentTo(expectedErrors);
        actual.Status.Should().Be(ResultStatus.Invalid);
    }

    [Test]
    public void BadRequest_WhenResultIsInvalidWithoutMessage_ShouldReturnsBadRequestObjectResult()
    {
        // Arrange
        int expectedStatus = StatusCodes.Status400BadRequest;
        var expectedMessage = ResponseMessages.ValidationErrors;
        var modelState = new ModelStateDictionary();
        modelState.AddModelError("Id",   "Id is required.");
        modelState.AddModelError("Id",   "Id must be greater than 0.");
        modelState.AddModelError("Name", "Name is required.");
        modelState.AddModelError("Name", "Name must have a minimum of 10 characters.");
        var expectedErrors = new[]
        {
            "'Id' property failed validation. Error was: Id is required.",
            "'Id' property failed validation. Error was: Id must be greater than 0.",
            "'Name' property failed validation. Error was: Name is required.",
            "'Name' property failed validation. Error was: Name must have a minimum of 10 characters."
        };

        // Act
        BadRequestObjectResult objectResult = modelState.BadRequest();
        Result actual = objectResult.Value as Result;

        // Asserts
        objectResult.StatusCode.Should().Be(expectedStatus);
        actual.IsSuccess.Should().BeFalse();
        actual.IsFailed.Should().BeTrue();
        actual.Message.Should().Be(expectedMessage);
        actual.Errors.Should().BeEquivalentTo(expectedErrors);
        actual.Status.Should().Be(ResultStatus.Invalid);
    }

    [Test]
    public void BadRequest_WhenResultIsInvalidWithMessage_ShouldReturnsBadRequestObjectResult()
    {
        // Arrange
        int expectedStatus = StatusCodes.Status400BadRequest;
        var expectedMessage = "Error";
        var modelState = new ModelStateDictionary();
        modelState.AddModelError("Id",   "Id is required.");
        modelState.AddModelError("Id",   "Id must be greater than 0.");
        modelState.AddModelError("Name", "Name is required.");
        modelState.AddModelError("Name", "Name must have a minimum of 10 characters.");
        var expectedErrors = new[]
        {
            "'Id' property failed validation. Error was: Id is required.",
            "'Id' property failed validation. Error was: Id must be greater than 0.",
            "'Name' property failed validation. Error was: Name is required.",
            "'Name' property failed validation. Error was: Name must have a minimum of 10 characters."
        };

        // Act
        BadRequestObjectResult objectResult = modelState.BadRequest(expectedMessage);
        Result actual = objectResult.Value as Result;

        // Asserts
        objectResult.StatusCode.Should().Be(expectedStatus);
        actual.IsSuccess.Should().BeFalse();
        actual.IsFailed.Should().BeTrue();
        actual.Message.Should().Be(expectedMessage);
        actual.Errors.Should().BeEquivalentTo(expectedErrors);
        actual.Status.Should().Be(ResultStatus.Invalid);
    }
}

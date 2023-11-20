namespace SimpleResults.Tests.FluentValidation;

public class ValidationResultExtensionsTests
{
    [Test]
    public void IsFailed_WhenValidationResultIsFailed_ShouldReturnsTrue()
    {
        // Arrange
        var person = new Person { Name = string.Empty };
        var validator = new PersonValidator();
        ValidationResult result = validator.Validate(person);

        // Act
        bool actual = result.IsFailed();

        // Assert
        actual.Should().BeTrue();
    }

    [Test]
    public void IsFailed_WhenValidationResultIsSuccess_ShouldReturnsFalse()
    {
        // Arrange
        var person = new Person { Name = "Bob" };
        var validator = new PersonValidator();
        ValidationResult result = validator.Validate(person);

        // Act
        bool actual = result.IsFailed();

        // Assert
        actual.Should().BeFalse();
    }

    [Test]
    public void AsErrors_WhenValidationResultIsReceived_ShouldReturnsCollectionOfErrorMessages()
    {
        // Arrange
        var person = new Person { Name = string.Empty };
        var validator = new PersonValidator();
        ValidationResult result = validator.Validate(person);
        var expectedCollection = new[]
        {
            "'Name' must not be empty."
        };

        // Act
        IEnumerable<string> actual = result.AsErrors();

        // Assert
        actual.Should().BeEquivalentTo(expectedCollection);
    }

    [Test]
    public void AsErrors_WhenThereAreNoErrors_ShouldReturnsEmptyCollection()
    {
        // Arrange
        var person = new Person { Name = "Alice" };
        var validator = new PersonValidator();
        ValidationResult result = validator.Validate(person);

        // Act
        IEnumerable<string> actual = result.AsErrors();

        // Assert
        actual.Should().BeEmpty();
    }

    [Test]
    public void Invalid_WhenResultIsInvalidWithoutMessage_ShouldReturnsResultObject()
    {
        // Arrange
        var person = new Person { Name = string.Empty };
        var validator = new PersonValidator();
        ValidationResult result = validator.Validate(person);
        var expectedMessage = ResponseMessages.ValidationErrors;
        var expectedErrors = new[]
        {
            "'Name' must not be empty."
        };

        // Act
        Result actual = result.Invalid();

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
        var person = new Person { Name = string.Empty };
        var validator = new PersonValidator();
        ValidationResult result = validator.Validate(person);
        var expectedMessage = "Error";
        var expectedErrors = new[]
        {
            "'Name' must not be empty."
        };

        // Act
        Result actual = result.Invalid(expectedMessage);

        // Asserts
        actual.IsSuccess.Should().BeFalse();
        actual.IsFailed.Should().BeTrue();
        actual.Message.Should().Be(expectedMessage);
        actual.Errors.Should().BeEquivalentTo(expectedErrors);
        actual.Status.Should().Be(ResultStatus.Invalid);
    }
}

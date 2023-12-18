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

    [TestCaseSource(typeof(FailedValidationTestCases))]
    public void AsErrors_WhenValidationResultIsReceived_ShouldReturnsCollectionOfErrorMessages(
        Order order,
        string[] expectedErrors)
    {
        // Arrange
        var validator = new OrderValidator();
        ValidationResult result = validator.Validate(order);

        // Act
        IEnumerable<string> actual = result.AsErrors();

        // Assert
        actual.Should().BeEquivalentTo(expectedErrors);
    }

    [Test]
    public void AsErrors_WhenThereAreNoErrors_ShouldReturnsEmptyCollection()
    {
        // Arrange
        var order = new Order
        {
            Customer = "Bob",
            Description = "Test",
            DeliveryAddress = new Address { Description = "D", PostCode = "P", Country = "C" },
            Details = new List<OrderDetail>
            {
                new()
                {
                    Product = "P",
                    Price = 5000,
                    Amount = 2
                }
            }
        };
        var validator = new OrderValidator();
        ValidationResult result = validator.Validate(order);

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
            "'Name' property failed validation. Error was: 'Name' must not be empty."
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
            "'Name' property failed validation. Error was: 'Name' must not be empty."
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

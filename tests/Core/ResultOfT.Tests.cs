namespace SimpleResults.Tests.Core;

public class ResultOfTTests
{
    [Test]
    public void ResultOfT_ShouldCreateInstanceWithDefaultValues()
    {
        // Arrange
        var expectedMessage = ResponseMessages.Error;

        // Act
        var actual = new Result<Person>();

        // Asserts
        actual.Data.Should().BeNull();
        actual.IsSuccess.Should().BeFalse();
        actual.IsFailed.Should().BeTrue();
        actual.Message.Should().Be(expectedMessage);
        actual.Errors.Should().BeEmpty();
        actual.Status.Should().Be(ResultStatus.Failure);
    }

    [Test]
    public void ImplicitOperator_WhenConvertedFromResultType_ShouldReturnsResultOfT()
    {
        // Arrange
        var expectedMessage = ResponseMessages.NotFound;

        // Act
        Result<List<Person>> actual = Result.NotFound();

        // Asserts
        actual.Data.Should().BeNull();
        actual.IsSuccess.Should().BeFalse();
        actual.IsFailed.Should().BeTrue();
        actual.Message.Should().Be(expectedMessage);
        actual.Errors.Should().BeEmpty();
        actual.Status.Should().Be(ResultStatus.NotFound);
    }

    [Test]
    public void ImplicitOperator_WhenConvertedFromValueOfT_ShouldReturnsResultOfT()
    {
        // Arrange
        var expectedValue = new List<Person>
        {
            new() { Name = "Bob" },
            new() { Name = "Alice" }
        };
        var expectedMessage = ResponseMessages.Success;

        // Act
        Result<List<Person>> actual = expectedValue;

        // Asserts
        actual.Data.Should().BeEquivalentTo(expectedValue);
        actual.IsSuccess.Should().BeTrue();
        actual.IsFailed.Should().BeFalse();
        actual.Message.Should().Be(expectedMessage);
        actual.Errors.Should().BeEmpty();
        actual.Status.Should().Be(ResultStatus.Ok);
    }

    [Test]
    public void ImplicitOperator_WhenConvertedFromResultOfT_ShouldReturnsValueOfT()
    {
        // Arrange
        var expectedValue = new List<Person>
        {
            new() { Name = "Bob" },
            new() { Name = "Alice" }
        };
        Result<List<Person>> result = Result.Success(expectedValue);

        // Act
        List<Person> actual = result;

        // Assert
        actual.Should().BeEquivalentTo(expectedValue);
    }
}

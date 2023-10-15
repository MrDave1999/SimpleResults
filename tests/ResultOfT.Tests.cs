namespace SimpleResults.Tests;

public class ResultOfTTests
{
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
}

namespace SimpleResults.Tests;

public class PagedResultOfTTests
{
    [Test]
    public void ImplicitOperator_WhenConvertedFromResultType_ShouldReturnsPagedResultOfT()
    {
        // Arrange
        var expectedMessage = ResponseMessages.NotFound;

        // Act
        PagedResult<Person> actual = Result.NotFound();

        // Asserts
        actual.Data.Should().BeEmpty();
        actual.PagedInfo.Should().BeNull();
        actual.IsSuccess.Should().BeFalse();
        actual.IsFailed.Should().BeTrue();
        actual.Message.Should().Be(expectedMessage);
        actual.Errors.Should().BeEmpty();
        actual.Status.Should().Be(ResultStatus.NotFound);
    }
}

namespace SimpleResults.Tests.Core;

public partial class ResultTests
{
    [Test]
    public void ToResult_WhenTypeIsResult_ShouldReturnsInstanceOfTypeResultOfT()
    {
        // Arrange
        var expectedMessage = ResponseMessages.Invalid;
        var expectedData = new Person { Name = "Bob" };
        Result invalidResult = Result.Invalid();

        // Act
        Result<Person> actual = invalidResult.ToResult(expectedData);

        // Asserts
        actual.Data.Should().Be(expectedData);
        actual.IsSuccess.Should().BeFalse();
        actual.IsFailed.Should().BeTrue();
        actual.Message.Should().Be(expectedMessage);
        actual.Errors.Should().BeEmpty();
        actual.Status.Should().Be(ResultStatus.Invalid);
    }

    [Test]
    public void ToListedResult_WhenTypeIsResult_ShouldReturnsInstanceOfTypeListedResultOfT()
    {
        // Arrange
        var expectedMessage = ResponseMessages.Invalid;
        var expectedData = new List<Person>
        {
            new() { Name = "Bob" }
        };
        Result invalidResult = Result.Invalid();

        // Act
        ListedResult<Person> actual = invalidResult.ToListedResult(expectedData);

        // Asserts
        actual.Data.Should().BeEquivalentTo(expectedData);
        actual.IsSuccess.Should().BeFalse();
        actual.IsFailed.Should().BeTrue();
        actual.Message.Should().Be(expectedMessage);
        actual.Errors.Should().BeEmpty();
        actual.Status.Should().Be(ResultStatus.Invalid);
    }

    [Test]
    public void ToPagedResult_WhenTypeIsResult_ShouldReturnsInstanceOfTypePagedResultOfT()
    {
        // Arrange
        var expectedMessage = ResponseMessages.Invalid;
        var expectedData = new List<Person>
        {
            new() { Name = "Bob" }
        };
        var expectedPagedInfo = new PagedInfo(pageNumber: 1, pageSize: 10, totalRecords: 100);
        Result invalidResult = Result.Invalid();

        // Act
        PagedResult<Person> actual = invalidResult.ToPagedResult(expectedData, expectedPagedInfo);

        // Asserts
        actual.Data.Should().BeEquivalentTo(expectedData);
        actual.PagedInfo.Should().Be(expectedPagedInfo);
        actual.IsSuccess.Should().BeFalse();
        actual.IsFailed.Should().BeTrue();
        actual.Message.Should().Be(expectedMessage);
        actual.Errors.Should().BeEmpty();
        actual.Status.Should().Be(ResultStatus.Invalid);
    }
}

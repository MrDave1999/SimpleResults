namespace SimpleResults.Tests.Core;

public class ListedResultOfTTests
{
    [Test]
    public void ListedResultOfT_ShouldCreateInstanceWithDefaultValues()
    {
        // Arrange
        var expectedMessage = ResponseMessages.Error;

        // Act
        var actual = new ListedResult<Person>();

        // Asserts
        actual.Data.Should().BeEmpty();
        actual.IsSuccess.Should().BeFalse();
        actual.IsFailed.Should().BeTrue();
        actual.Message.Should().Be(expectedMessage);
        actual.Errors.Should().BeEmpty();
        actual.Status.Should().Be(ResultStatus.Failure);
    }

    [Test]
    public void ImplicitOperator_WhenConvertedFromResultType_ShouldReturnsListedResultOfT()
    {
        // Arrange
        var expectedMessage = ResponseMessages.NotFound;

        // Act
        ListedResult<Person> actual = Result.NotFound();

        // Asserts
        actual.Data.Should().BeEmpty();
        actual.IsSuccess.Should().BeFalse();
        actual.IsFailed.Should().BeTrue();
        actual.Message.Should().Be(expectedMessage);
        actual.Errors.Should().BeEmpty();
        actual.Status.Should().Be(ResultStatus.NotFound);
    }

    [Test]
    public void ImplicitOperator_WhenConvertedFromResultOfEnumerableType_ShouldReturnsListedResultOfT()
    {
        // Arrange
        IEnumerable<Person> expectedData = new List<Person>();
        Result<IEnumerable<Person>> result = Result.Success(expectedData);
        var expectedMessage = ResponseMessages.Success;

        // Act
        ListedResult<Person> actual = result;

        // Asserts
        actual.Data.Should().BeEquivalentTo(expectedData);
        actual.IsSuccess.Should().BeTrue();
        actual.IsFailed.Should().BeFalse();
        actual.Message.Should().Be(expectedMessage);
        actual.Errors.Should().BeEmpty();
        actual.Status.Should().Be(ResultStatus.Ok);
    }

    [Test]
    public void ImplicitOperator_WhenConvertedFromResultOfListType_ShouldReturnsListedResultOfT()
    {
        // Arrange
        var expectedData = new List<Person>();
        var expectedMessage = ResponseMessages.Success;
        Result<List<Person>> result = Result.Success(expectedData);

        // Act
        ListedResult<Person> actual = result;

        // Asserts
        actual.Data.Should().BeEquivalentTo(expectedData);
        actual.IsSuccess.Should().BeTrue();
        actual.IsFailed.Should().BeFalse();
        actual.Message.Should().Be(expectedMessage);
        actual.Errors.Should().BeEmpty();
        actual.Status.Should().Be(ResultStatus.Ok);
    }

    [Test]
    public void ImplicitOperator_WhenConvertedFromResultOfArrayType_ShouldReturnsListedResultOfT()
    {
        // Arrange
        var expectedData = new Person[]
        {
            new() { Name = "Test" }
        };
        var expectedMessage = ResponseMessages.Success;
        Result<Person[]> result = Result.Success(expectedData);

        // Act
        ListedResult<Person> actual = result;

        // Asserts
        actual.Data.Should().BeEquivalentTo(expectedData);
        actual.IsSuccess.Should().BeTrue();
        actual.IsFailed.Should().BeFalse();
        actual.Message.Should().Be(expectedMessage);
        actual.Errors.Should().BeEmpty();
        actual.Status.Should().Be(ResultStatus.Ok);
    }
}

namespace SimpleResults.Tests;

public class ResultSetOfTTests
{
    [Test]
    public void ImplicitOperator_WhenConvertedFromResultType_ShouldReturnsResultSetObject()
    {
        // Arrange
        var expectedMessage = ResponseMessages.NotFound;

        // Act
        ResultSet<Person> actual = Result.NotFound();

        // Asserts
        actual.Data.Should().BeEmpty();
        actual.IsSuccess.Should().BeFalse();
        actual.IsFailed.Should().BeTrue();
        actual.Message.Should().Be(expectedMessage);
        actual.Errors.Should().BeEmpty();
        actual.Status.Should().Be(ResultStatus.NotFound);
    }

    [Test]
    public void ImplicitOperator_WhenConvertedFromResultOfEnumerableType_ShouldReturnsResultSetObject()
    {
        // Arrange
        IEnumerable<Person> expectedData = new List<Person>();
        Result<IEnumerable<Person>> result = Result.Success(expectedData);
        var expectedMessage = ResponseMessages.Success;

        // Act
        ResultSet<Person> actual = result;

        // Asserts
        actual.Data.Should().BeEquivalentTo(expectedData);
        actual.IsSuccess.Should().BeTrue();
        actual.IsFailed.Should().BeFalse();
        actual.Message.Should().Be(expectedMessage);
        actual.Errors.Should().BeEmpty();
        actual.Status.Should().Be(ResultStatus.Ok);
    }

    [Test]
    public void ImplicitOperator_WhenConvertedFromResultOfListType_ShouldReturnsResultSetObject()
    {
        // Arrange
        var expectedData = new List<Person>();
        var expectedMessage = ResponseMessages.Success;
        Result<List<Person>> result = Result.Success(expectedData);

        // Act
        ResultSet<Person> actual = result;

        // Asserts
        actual.Data.Should().BeEquivalentTo(expectedData);
        actual.IsSuccess.Should().BeTrue();
        actual.IsFailed.Should().BeFalse();
        actual.Message.Should().Be(expectedMessage);
        actual.Errors.Should().BeEmpty();
        actual.Status.Should().Be(ResultStatus.Ok);
    }

    [Test]
    public void ImplicitOperator_WhenConvertedFromResultOfArrayType_ShouldReturnsResultSetObject()
    {
        // Arrange
        var expectedData = new Person[]
        {
            new() { Name = "Test" }
        };
        var expectedMessage = ResponseMessages.Success;
        Result<Person[]> result = Result.Success(expectedData);

        // Act
        ResultSet<Person> actual = result;

        // Asserts
        actual.Data.Should().BeEquivalentTo(expectedData);
        actual.IsSuccess.Should().BeTrue();
        actual.IsFailed.Should().BeFalse();
        actual.Message.Should().Be(expectedMessage);
        actual.Errors.Should().BeEmpty();
        actual.Status.Should().Be(ResultStatus.Ok);
    }
}

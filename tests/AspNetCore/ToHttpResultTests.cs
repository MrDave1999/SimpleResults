namespace SimpleResults.Tests.AspNetCore;

public partial class ToHttpResultTests
{
    [Test]
    public void ToHttpResult_WhenOperationResultIsSuccess_ShouldReturnsHttpResultsOK()
    {
        // Arrange
        var expectedData = new Person { Name = "Bob" };
        int expectedHttpCode = 200;
        Result<Person> result = Result.Success(expectedData);

        // Act
        IResult httpResult = result.ToHttpResult();
        var contentResult = httpResult as Ok<ResultBase>;
        var actualValue = (Result<Person>)contentResult.Value;

        // Asserts
        contentResult.StatusCode.Should().Be(expectedHttpCode);
        actualValue.IsSuccess.Should().BeTrue();
        actualValue.IsFailed.Should().BeFalse();
        actualValue.Message.Should().Be(ResponseMessages.Success);
        actualValue.Errors.Should().BeEmpty();
        actualValue.Data.Should().BeEquivalentTo(expectedData);
        actualValue.Status.Should().Be(ResultStatus.Ok);
    }

    [Test]
    public void ToHttpResult_WhenOperationResultIsCreatedWithId_ShouldReturnsHttpResultsCreated()
    {
        // Arrange
        var expectedData = new CreatedId { Id = 1 };
        int expectedHttpCode = 201;
        Result<CreatedId> result = Result.CreatedResource(expectedData.Id);

        // Act
        IResult httpResult = result.ToHttpResult();
        var contentResult = httpResult as Created<ResultBase>;
        var actualValue = (Result<CreatedId>)contentResult.Value;

        // Asserts
        contentResult.StatusCode.Should().Be(expectedHttpCode);
        actualValue.IsSuccess.Should().BeTrue();
        actualValue.IsFailed.Should().BeFalse();
        actualValue.Message.Should().Be(ResponseMessages.CreatedResource);
        actualValue.Errors.Should().BeEmpty();
        actualValue.Data.Should().BeEquivalentTo(expectedData);
        actualValue.Status.Should().Be(ResultStatus.Created);
    }

    [Test]
    public void ToHttpResult_WhenOperationResultIsCreatedWithGuid_ShouldReturnsHttpResultsCreated()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var expectedData = new CreatedGuid { Id = guid.ToString() };
        int expectedHttpCode = 201;
        Result<CreatedGuid> result = Result.CreatedResource(guid);

        // Act
        IResult httpResult = result.ToHttpResult();
        var contentResult = httpResult as Created<ResultBase>;
        var actualValue = (Result<CreatedGuid>)contentResult.Value;

        // Asserts
        contentResult.StatusCode.Should().Be(expectedHttpCode);
        actualValue.IsSuccess.Should().BeTrue();
        actualValue.IsFailed.Should().BeFalse();
        actualValue.Message.Should().Be(ResponseMessages.CreatedResource);
        actualValue.Errors.Should().BeEmpty();
        actualValue.Data.Should().BeEquivalentTo(expectedData);
        actualValue.Status.Should().Be(ResultStatus.Created);
    }

    [Test]
    public void ToHttpResult_WhenOperationResultIsUpdated_ShouldReturnsHttpResultsOK()
    {
        // Arrange
        int expectedHttpCode = 200;
        Result result = Result.UpdatedResource();

        // Act
        IResult httpResult = result.ToHttpResult();
        var contentResult = httpResult as Ok<ResultBase>;
        var actualValue = (Result)contentResult.Value;

        // Asserts
        contentResult.StatusCode.Should().Be(expectedHttpCode);
        actualValue.IsSuccess.Should().BeTrue();
        actualValue.IsFailed.Should().BeFalse();
        actualValue.Message.Should().Be(ResponseMessages.UpdatedResource);
        actualValue.Errors.Should().BeEmpty();
        actualValue.Status.Should().Be(ResultStatus.Ok);
    }

    [Test]
    public void ToHttpResult_WhenOperationResultIsDeleted_ShouldReturnsHttpResultsOK()
    {
        // Arrange
        int expectedHttpCode = 200;
        Result result = Result.DeletedResource();

        // Act
        IResult httpResult = result.ToHttpResult();
        var contentResult = httpResult as Ok<ResultBase>;
        var actualValue = (Result)contentResult.Value;

        // Asserts
        contentResult.StatusCode.Should().Be(expectedHttpCode);
        actualValue.IsSuccess.Should().BeTrue();
        actualValue.IsFailed.Should().BeFalse();
        actualValue.Message.Should().Be(ResponseMessages.DeletedResource);
        actualValue.Errors.Should().BeEmpty();
        actualValue.Status.Should().Be(ResultStatus.Ok);
    }

    [Test]
    public void ToHttpResult_WhenOperationResultIsObtained_ShouldReturnsHttpResultsOK()
    {
        // Arrange
        var expectedData = new Person { Name = "Bob" };
        int expectedHttpCode = 200;
        Result<Person> result = Result.ObtainedResource(expectedData);

        // Act
        IResult httpResult = result.ToHttpResult();
        var contentResult = httpResult as Ok<ResultBase>;
        var actualValue = (Result<Person>)contentResult.Value;

        // Asserts
        contentResult.StatusCode.Should().Be(expectedHttpCode);
        actualValue.IsSuccess.Should().BeTrue();
        actualValue.IsFailed.Should().BeFalse();
        actualValue.Message.Should().Be(ResponseMessages.ObtainedResource);
        actualValue.Errors.Should().BeEmpty();
        actualValue.Data.Should().Be(expectedData);
        actualValue.Status.Should().Be(ResultStatus.Ok);
    }

    [Test]
    public void ToHttpResult_WhenOperationResultIsNotSupported_ShouldThrowNotSupportedException()
    {
        // Arrange
        var result = new Result<Person> { Status = (ResultStatus)5000 };
        var expectedMessage = new UnsupportedStatusError(result.Status).Message;

        // Act
        Action act = () => result.ToHttpResult();

        // Assert
        act.Should()
           .Throw<NotSupportedException>()
           .WithMessage(expectedMessage);
    }

    [Test]
    public void ToHttpResult_WhenTypeIsListedResultOfT_ShouldReturnsHttpResultsOK()
    {
        // Arrange
        int expectedHttpCode = 200;
        var expectedData = new List<Person>();
        ListedResult<Person> result = Result.Success(expectedData);

        // Act
        IResult httpResult = result.ToHttpResult();
        var contentResult = httpResult as Ok<ResultBase>;
        var actualValue = (ListedResult<Person>)contentResult.Value;

        // Asserts
        contentResult.StatusCode.Should().Be(expectedHttpCode);
        actualValue.IsSuccess.Should().BeTrue();
        actualValue.IsFailed.Should().BeFalse();
        actualValue.Message.Should().Be(ResponseMessages.Success);
        actualValue.Errors.Should().BeEmpty();
        actualValue.Data.Should().BeEquivalentTo(expectedData);
        actualValue.Status.Should().Be(ResultStatus.Ok);
    }

    [Test]
    public void ToHttpResult_WhenTypeIsPagedResultOfT_ShouldReturnsHttpResultsOK()
    {
        // Arrange
        int expectedHttpCode = 200;
        var expectedData = new List<Person>();
        var expectedPagedInfo = new PagedInfo(1, 10, 5);
        PagedResult<Person> result = Result.Success(expectedData, expectedPagedInfo);

        // Act
        IResult httpResult = result.ToHttpResult();
        var contentResult = httpResult as Ok<ResultBase>;
        var actualValue = (PagedResult<Person>)contentResult.Value;

        // Asserts
        contentResult.StatusCode.Should().Be(expectedHttpCode);
        actualValue.IsSuccess.Should().BeTrue();
        actualValue.IsFailed.Should().BeFalse();
        actualValue.Message.Should().Be(ResponseMessages.ObtainedResources);
        actualValue.Errors.Should().BeEmpty();
        actualValue.Data.Should().BeEquivalentTo(expectedData);
        actualValue.PagedInfo.Should().BeEquivalentTo(expectedPagedInfo);
        actualValue.Status.Should().Be(ResultStatus.Ok);
    }
}

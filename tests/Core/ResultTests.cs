namespace SimpleResults.Tests.Core;

public partial class ResultTests
{
    [Test]
    public void Result_ShouldCreateInstanceWithDefaultValues()
    {
        // Arrange
        var expectedMessage = ResponseMessages.Error;

        // Act
        var actual = new Result();

        // Asserts
        actual.IsSuccess.Should().BeFalse();
        actual.IsFailed.Should().BeTrue();
        actual.Message.Should().Be(expectedMessage);
        actual.Errors.Should().BeEmpty();
        actual.Status.Should().Be(ResultStatus.Failure);
    }

    [Test]
    public void Success_WhenResultIsSuccessWithMessage_ShouldReturnsResultOfT()
    {
        // Arrange
        var expectedData = new Person { Name = "Test" };
        var expectedMessage = "Success";

        // Act
        Result<Person> actual = Result.Success(expectedData, expectedMessage);

        // Asserts
        actual.IsSuccess.Should().BeTrue();
        actual.IsFailed.Should().BeFalse();
        actual.Message.Should().Be(expectedMessage);
        actual.Errors.Should().BeEmpty();
        actual.Data.Should().BeEquivalentTo(expectedData);
        actual.Status.Should().Be(ResultStatus.Ok);
    }

    [Test]
    public void Success_WhenResultIsSuccessWithoutMessage_ShouldReturnsResultOfT()
    {
        // Arrange
        var expectedData = new Person { Name = "Test" };

        // Act
        Result<Person> actual = Result.Success(expectedData);

        // Asserts
        actual.IsSuccess.Should().BeTrue();
        actual.IsFailed.Should().BeFalse();
        actual.Message.Should().Be(ResponseMessages.Success);
        actual.Errors.Should().BeEmpty();
        actual.Data.Should().BeEquivalentTo(expectedData);
        actual.Status.Should().Be(ResultStatus.Ok);
    }

    [Test]
    public void Success_WhenResultIsSuccessWithMessage_ShouldReturnsPagedResultOfT()
    {
        // Arrange
        var expectedData = new List<Person>
        {
            new() { Name = "Test" }
        };
        var expectedPagedInfo = new PagedInfo(1, 10, 1);
        var expectedMessage = "Success";

        // Act
        PagedResult<Person> actual = Result.Success(expectedData, expectedPagedInfo, expectedMessage);

        // Asserts
        actual.IsSuccess.Should().BeTrue();
        actual.IsFailed.Should().BeFalse();
        actual.Message.Should().Be(expectedMessage);
        actual.Errors.Should().BeEmpty();
        actual.Data.Should().BeEquivalentTo(expectedData);
        actual.PagedInfo.Should().BeEquivalentTo(expectedPagedInfo);
        actual.Status.Should().Be(ResultStatus.Ok);
    }

    [Test]
    public void Success_WhenResultIsSuccessWithoutMessage_ShouldReturnsPagedResultOfT()
    {
        // Arrange
        var expectedData = new List<Person>
        {
            new() { Name = "Test" }
        };
        var expectedPagedInfo = new PagedInfo(1, 10, 1);

        // Act
        PagedResult<Person> actual = Result.Success(expectedData, expectedPagedInfo);

        // Asserts
        actual.IsSuccess.Should().BeTrue();
        actual.IsFailed.Should().BeFalse();
        actual.Message.Should().Be(ResponseMessages.ObtainedResources);
        actual.Errors.Should().BeEmpty();
        actual.Data.Should().BeEquivalentTo(expectedData);
        actual.PagedInfo.Should().BeEquivalentTo(expectedPagedInfo);
        actual.Status.Should().Be(ResultStatus.Ok);
    }

    [Test]
    public void Success_WhenResultIsSuccessWithMessage_ShouldReturnsResultObject()
    {
        // Arrange
        var expectedMessage = "Success";

        // Act
        Result actual = Result.Success(expectedMessage);

        // Asserts
        actual.IsSuccess.Should().BeTrue();
        actual.IsFailed.Should().BeFalse();
        actual.Message.Should().Be(expectedMessage);
        actual.Errors.Should().BeEmpty();
        actual.Status.Should().Be(ResultStatus.Ok);
    }

    [Test]
    public void Success_WhenResultIsSuccessWithoutMessage_ShouldReturnsResultObject()
    {
        // Arrange
        var expectedMessage = ResponseMessages.Success;

        // Act
        Result actual = Result.Success();

        // Asserts
        actual.IsSuccess.Should().BeTrue();
        actual.IsFailed.Should().BeFalse();
        actual.Message.Should().Be(expectedMessage);
        actual.Errors.Should().BeEmpty();
        actual.Status.Should().Be(ResultStatus.Ok);
    }

    [Test]
    public void CreatedResource_WhenResultIsCreatedResourceWithoutMessage_ShouldReturnsResultObject()
    {
        // Arrange
        var expectedMessage = ResponseMessages.CreatedResource;

        // Act
        Result actual = Result.CreatedResource();

        // Asserts
        actual.IsSuccess.Should().BeTrue();
        actual.IsFailed.Should().BeFalse();
        actual.Message.Should().Be(expectedMessage);
        actual.Errors.Should().BeEmpty();
        actual.Status.Should().Be(ResultStatus.Created);
    }

    [Test]
    public void CreatedResource_WhenResultIsCreatedResourceWithMessage_ShouldReturnsResultObject()
    {
        // Arrange
        var expectedMessage = "Created";

        // Act
        Result actual = Result.CreatedResource(expectedMessage);

        // Asserts
        actual.IsSuccess.Should().BeTrue();
        actual.IsFailed.Should().BeFalse();
        actual.Message.Should().Be(expectedMessage);
        actual.Errors.Should().BeEmpty();
        actual.Status.Should().Be(ResultStatus.Created);
    }

    [Test]
    public void CreatedResource_WhenResultIsCreatedResourceWithId_ShouldReturnsResultObject()
    {
        // Arrange
        var expectedData = new CreatedId { Id = 1 };
        var expectedMessage = "Created";

        // Act
        Result<CreatedId> actual = Result.CreatedResource(expectedData.Id, expectedMessage);

        // Asserts
        actual.IsSuccess.Should().BeTrue();
        actual.IsFailed.Should().BeFalse();
        actual.Message.Should().Be(expectedMessage);
        actual.Errors.Should().BeEmpty();
        actual.Data.Should().BeEquivalentTo(expectedData);
        actual.Status.Should().Be(ResultStatus.Created);
    }

    [Test]
    public void CreatedResource_WhenResultIsCreatedResourceWithGuid_ShouldReturnsResultObject()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var expectedData = new CreatedGuid { Id = guid.ToString() };
        var expectedMessage = "Created";

        // Act
        Result<CreatedGuid> actual = Result.CreatedResource(guid, expectedMessage);

        // Asserts
        actual.IsSuccess.Should().BeTrue();
        actual.IsFailed.Should().BeFalse();
        actual.Message.Should().Be(expectedMessage);
        actual.Errors.Should().BeEmpty();
        actual.Data.Should().BeEquivalentTo(expectedData);
        actual.Status.Should().Be(ResultStatus.Created);
    }

    [Test]
    public void UpdatedResource_WhenResultIsUpdatedResourceWithSuccess_ShouldReturnsResultObject()
    {
        // Arrange
        var expectedMessage = ResponseMessages.UpdatedResource;

        // Act
        Result actual = Result.UpdatedResource();

        // Asserts
        actual.IsSuccess.Should().BeTrue();
        actual.IsFailed.Should().BeFalse();
        actual.Message.Should().Be(expectedMessage);
        actual.Errors.Should().BeEmpty();
        actual.Status.Should().Be(ResultStatus.Ok);
    }

    [Test]
    public void DeletedResource_WhenResultIsDeletedResourceWithSuccess_ShouldReturnsResultObject()
    {
        // Arrange
        var expectedMessage = ResponseMessages.DeletedResource;

        // Act
        Result actual = Result.DeletedResource();

        // Asserts
        actual.IsSuccess.Should().BeTrue();
        actual.IsFailed.Should().BeFalse();
        actual.Message.Should().Be(expectedMessage);
        actual.Errors.Should().BeEmpty();
        actual.Status.Should().Be(ResultStatus.Ok);
    }

    [Test]
    public void ObtainedResource_WhenResultIsObtainedResourceWithSuccess_ShouldReturnsResultOfT()
    {
        // Arrange
        var expectedData = new Person { Name = "Test" };
        var expectedMessage = ResponseMessages.ObtainedResource;

        // Act
        Result<Person> actual = Result.ObtainedResource(expectedData);

        // Asserts
        actual.IsSuccess.Should().BeTrue();
        actual.IsFailed.Should().BeFalse();
        actual.Message.Should().Be(expectedMessage);
        actual.Errors.Should().BeEmpty();
        actual.Data.Should().Be(expectedData);
        actual.Status.Should().Be(ResultStatus.Ok);
    }

    [Test]
    public void ObtainedResources_WhenResultIsObtainedResources_ShouldReturnsListedResultOfT()
    {
        // Arrange
        var expectedData = new List<Person>
        {
            new() { Name = "Test" }
        };
        var expectedMessage = ResponseMessages.ObtainedResources;

        // Act
        ListedResult<Person> actual = Result.ObtainedResources(expectedData);

        // Asserts
        actual.IsSuccess.Should().BeTrue();
        actual.IsFailed.Should().BeFalse();
        actual.Message.Should().Be(expectedMessage);
        actual.Errors.Should().BeEmpty();
        actual.Data.Should().BeEquivalentTo(expectedData);
        actual.Status.Should().Be(ResultStatus.Ok);
    }

    [Test]
    public void File_WhenResultIsByteArrayFileContent_ShouldReturnsResultOfByteArrayFileContent()
    {
        // Arrange
        byte[] content = [1, 1, 0, 0];
        var expectedData = new ByteArrayFileContent(content)
        {
            ContentType = "application/pdf",
            FileName    = "Report.pdf"
        };
        var expectedMessage = ResponseMessages.FileContent;

        // Act
        Result<ByteArrayFileContent> actual = Result.File(expectedData);

        // Asserts
        actual.IsSuccess.Should().BeTrue();
        actual.IsFailed.Should().BeFalse();
        actual.Message.Should().Be(expectedMessage);
        actual.Errors.Should().BeEmpty();
        actual.Data.Should().BeEquivalentTo(expectedData);
        actual.Status.Should().Be(ResultStatus.ByteArrayFile);
    }

    [Test]
    public void File_WhenResultIsStreamFileContent_ShouldReturnsResultOfStreamFileContent()
    {
        // Arrange
        byte[] buffer = [1, 0, 1];
        Stream content = new MemoryStream(buffer);
        var expectedData = new StreamFileContent(content)
        {
            ContentType = "application/pdf",
            FileName    = "Report.pdf"
        };
        var expectedMessage = ResponseMessages.FileContent;

        // Act
        Result<StreamFileContent> actual = Result.File(expectedData);

        // Asserts
        actual.IsSuccess.Should().BeTrue();
        actual.IsFailed.Should().BeFalse();
        actual.Message.Should().Be(expectedMessage);
        actual.Errors.Should().BeEmpty();
        actual.Data.Should().BeEquivalentTo(expectedData);
        actual.Status.Should().Be(ResultStatus.StreamFile);
    }
}

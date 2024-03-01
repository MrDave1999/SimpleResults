namespace SimpleResults.Tests.AspNetCore;

public partial class ToActionResultTests
{
    [Test]
    public void ToActionResult_WhenOperationResultIsByteArrayFileContent_ShouldReturnsFileContentResult()
    {
        // Arrange
        byte[] content = [1, 0, 1, 1];
        var expectedData = new ByteArrayFileContent(content)
        {
            ContentType = "application/pdf",
            FileName    = "Report.pdf"
        };
        Result<ByteArrayFileContent> result = Result.File(expectedData);

        // Act
        ActionResult<Result<ByteArrayFileContent>> actionResult = result.ToActionResult();
        var contentResult = actionResult.Result as FileContentResult;

        // Asserts
        contentResult.FileContents.Should().BeEquivalentTo(expectedData.Content);
        contentResult.ContentType.Should().Be(expectedData.ContentType);
        contentResult.FileDownloadName.Should().Be(expectedData.FileName);
    }

    [Test]
    public void ToActionResult_WhenOperationResultIsStreamFileContent_ShouldReturnsFileStreamResult()
    {
        // Arrange
        byte[] expectedBuffer = [1, 1, 0, 1];
        Stream content = new MemoryStream(expectedBuffer);
        var expectedData = new StreamFileContent(content)
        {
            ContentType = "application/pdf",
            FileName    = "Report.pdf"
        };
        Result<StreamFileContent> result = Result.File(expectedData);

        // Act
        ActionResult<Result<StreamFileContent>> actionResult = result.ToActionResult();
        var contentResult = actionResult.Result as FileStreamResult;

        // Asserts
        contentResult
            .FileStream
            .Should()
            .BeOfType<MemoryStream>()
            .Subject
            .ToArray()
            .Should()
            .BeEquivalentTo(expectedBuffer);
        contentResult.ContentType.Should().Be(expectedData.ContentType);
        contentResult.FileDownloadName.Should().Be(expectedData.FileName);
    }

    [Test]
    public void ToActionResult_WhenConversionToFileContentResultFails_ShouldThrowInvalidOperationException()
    {
        // Arrange
        var person = new Person { Name = "Test" };
        var result = new Result<Person>
        {
            Status = ResultStatus.ByteArrayFile
        };
        var typeName = typeof(FileContentResult).FullName;
        var expectedMessage = new FailedConversionError(typeName).Message;

        // Act
        Action act = () => result.ToActionResult();

        // Assert
        act.Should()
           .Throw<InvalidOperationException>()
           .WithMessage(expectedMessage);
    }

    [Test]
    public void ToActionResult_WhenConversionToFileStreamResultFails_ShouldThrowInvalidOperationException()
    {
        // Arrange
        var person = new Person { Name = "Test" };
        var result = new Result<Person>
        {
            Status = ResultStatus.StreamFile
        };
        var typeName = typeof(FileStreamResult).FullName;
        var expectedMessage = new FailedConversionError(typeName).Message;

        // Act
        Action act = () => result.ToActionResult();

        // Assert
        act.Should()
           .Throw<InvalidOperationException>()
           .WithMessage(expectedMessage);
    }
}

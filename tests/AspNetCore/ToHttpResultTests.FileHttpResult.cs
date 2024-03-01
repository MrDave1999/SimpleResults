namespace SimpleResults.Tests.AspNetCore;

public partial class ToHttpResultTests
{
    [Test]
    public void ToHttpResult_WhenOperationResultIsByteArrayFileContent_ShouldReturnsFileContentHttpResult()
    {
        // Arrange
        byte[] content = [1, 0, 0, 1];
        var expectedData = new ByteArrayFileContent(content)
        {
            ContentType = "application/pdf",
            FileName    = "Report.pdf"
        };
        Result<ByteArrayFileContent> result = Result.File(expectedData);

        // Act
        IResult httpResult = result.ToHttpResult();
        var contentResult = httpResult as FileContentHttpResult;

        // Asserts
        byte[] byteArray = contentResult.FileContents.ToArray();
        byteArray.Should().BeEquivalentTo(expectedData.Content);
        contentResult.ContentType.Should().Be(expectedData.ContentType);
        contentResult.FileDownloadName.Should().Be(expectedData.FileName);
    }

    [Test]
    public void ToHttpResult_WhenOperationResultIsStreamFileContent_ShouldReturnsFileStreamHttpResult()
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
        IResult httpResult = result.ToHttpResult();
        var contentResult = httpResult as FileStreamHttpResult;

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

    [TestCase(ResultStatus.ByteArrayFile)]
    [TestCase(ResultStatus.StreamFile)]
    public void ToHttpResult_WhenConversionToFileHttpResultFails_ShouldThrowInvalidOperationException(ResultStatus status)
    {
        // Arrange
        var person = new Person { Name = "Test" };
        var result = new Result<Person>
        {
            Status = status
        };
        var typeName = typeof(IResult).FullName;
        var expectedMessage = new FailedConversionError(typeName).Message;

        // Act
        Action act = () => result.ToHttpResult();

        // Assert
        act.Should()
           .Throw<InvalidOperationException>()
           .WithMessage(expectedMessage);
    }
}

namespace SimpleResults.Tests.Core;

public class ResourceTests
{
    [Test]
    public void LoadDefaultResourceTest()
    {
        // Arrange
        var expectedMessage = "Test";

        // Act
        var actual = ResponseMessages.Test;

        // Assert
        actual.Should().Be(expectedMessage);
    }

    [Test]
    public void LoadResourceInSpanishTest()
    {
        // Arrange
        Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("es");
        var expectedMessage = "Prueba";

        // Act
        var actual = ResponseMessages.Test;

        // Assert
        actual.Should().Be(expectedMessage);
    }
}

namespace SimpleResults.Tests;

public class SystemTextJsonResult
{
    [Test]
    public void Result_ShouldSerializeResultWithoutData()
    {
        // Arrange
        var options = new JsonSerializerOptions { WriteIndented = true };
        Result result = Result.Success();
        var expectedJson =
            $$"""
            {
              "success": true,
              "message": "{{ResponseMessages.Success}}",
              "errors": []
            }
            """;

        // Act
        var actual = JsonSerializer.Serialize(result, options);

        // Assert
        actual.Should().BeEquivalentTo(expectedJson);
    }

    [Test]
    public void Result_ShouldDeserializeResultWithoutData()
    {
        // Arrange
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        Result expectedResult = Result.Success();
        var json =
            $$"""
            {
              "success": true,
              "message": "{{ResponseMessages.Success}}",
              "errors": []
            }
            """;

        // Act
        var actual = JsonSerializer.Deserialize<Result>(json, options);

        // Assert
        actual
            .Should()
            .BeEquivalentTo(expectedResult, o => o.Excluding(r => r.Status));
    }
}

namespace SimpleResults.Tests.Core.Json;

public class SystemTextJsonResultOfT
{
    [Test]
    public void ResultOfT_ShouldSerializeResultOfValueType()
    {
        // Arrange
        var options = new JsonSerializerOptions { WriteIndented = true };
        Result<int> result = Result.Success(1);
        var expectedJson =
            $$"""
            {
              "data": 1,
              "status": "Ok",
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
    public void ResultOfT_ShouldSerializeResultOfReferenceType()
    {
        // Arrange
        var options = new JsonSerializerOptions { WriteIndented = true };
        Result<Person> result = Result.Success(new Person { Name = "Test" });
        var expectedJson =
            $$"""
            {
              "data": {
                "name": "Test"
              },
              "status": "Ok",
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
    public void ResultOfT_ShouldDeserializeResultOfValueType()
    {
        // Arrange
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        Result<int> expectedResult = Result.Success(1);
        var json =
            $$"""
            {
              "data": 1,
              "status": "Ok",
              "success": true,
              "message": "{{ResponseMessages.Success}}",
              "errors": []
            }
            """;

        // Act
        var actual = JsonSerializer.Deserialize<Result<int>>(json, options);

        // Assert
        actual.Should().BeEquivalentTo(expectedResult);
    }

    [Test]
    public void ResultOfT_ShouldDeserializeResultOfCreatedIdType()
    {
        // Arrange
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        Result<CreatedId> expectedResult = Result.Success(new CreatedId { Id = 1 } );
        var json =
            $$"""
            {
              "data": {
                "Id": 1
              },
              "status": "Ok",
              "success": true,
              "message": "{{ResponseMessages.Success}}",
              "errors": []
            }
            """;

        // Act
        var actual = JsonSerializer.Deserialize<Result<CreatedId>>(json, options);

        // Assert
        actual.Should().BeEquivalentTo(expectedResult);
    }

    [Test]
    public void ResultOfT_ShouldDeserializeResultOfCreatedGuidType()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        Result<CreatedGuid> expectedResult = Result.Success(new CreatedGuid { Id = guid.ToString() });
        var json =
            $$"""
            {
              "data": {
                "Id": "{{guid}}"
              },
              "status": "Ok",
              "success": true,
              "message": "{{ResponseMessages.Success}}",
              "errors": []
            }
            """;

        // Act
        var actual = JsonSerializer.Deserialize<Result<CreatedGuid>>(json, options);

        // Assert
        actual.Should().BeEquivalentTo(expectedResult);
    }
}

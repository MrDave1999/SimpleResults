namespace SimpleResults.Tests.Core.Json;

public class SystemTextJsonListedResultOfT
{
    [Test]
    public void ListedResultOfT_ShouldSerializeResultOfValueType()
    {
        // Arrange
        var options = new JsonSerializerOptions { WriteIndented = true };
        var data = new int[] { 1, 2 };
        ListedResult<int> result = Result.Success(data);
        var expectedJson =
            $$"""
            {
              "data": [
                1,
                2
              ],
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
    public void ListedResultOfT_ShouldSerializeResultOfReferenceType()
    {
        // Arrange
        var options = new JsonSerializerOptions { WriteIndented = true };
        var data = new Person[]
        { 
            new() { Name = "Bob" },
            new() { Name = "Alice" }
        };
        ListedResult<Person> result = Result.Success(data);
        var expectedJson =
            $$"""
            {
              "data": [
                {
                  "name": "Bob"
                },
                {
                  "name": "Alice"
                }
              ],
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
    public void ListedResultOfT_ShouldDeserializeResultOfValueType()
    {
        // Arrange
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var data = new int[] { 1, 2 };
        ListedResult<int> expectedResult = Result.Success(data);
        var json =
            $$"""
            {
              "data": [
                1,
                2
              ],
              "status": "Ok",
              "success": true,
              "message": "{{ResponseMessages.Success}}",
              "errors": []
            }
            """;

        // Act
        var actual = JsonSerializer.Deserialize<ListedResult<int>>(json, options);

        // Assert
        actual.Should().BeEquivalentTo(expectedResult);
    }

    [Test]
    public void ListedResultOfT_ShouldDeserializeResultOfReferenceType()
    {
        // Arrange
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var data = new Person[]
        {
            new() { Name = "Bob" },
            new() { Name = "Alice" }
        };
        ListedResult<Person> expectedResult = Result.Success(data);
        var json =
            $$"""
            {
              "data": [
                {
                  "name": "Bob"
                },
                {
                  "name": "Alice"
                }
              ],
              "status": "Ok",
              "success": true,
              "message": "{{ResponseMessages.Success}}",
              "errors": []
            }
            """;

        // Act
        var actual = JsonSerializer.Deserialize<ListedResult<Person>>(json, options);

        // Assert
        actual.Should().BeEquivalentTo(expectedResult);
    }
}

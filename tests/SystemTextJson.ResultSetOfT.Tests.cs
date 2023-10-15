namespace SimpleResults.Tests;

public class SystemTextJsonResultSetOfT
{
    [Test]
    public void ResultSetOfT_ShouldSerializeResultOfValueType()
    {
        // Arrange
        var options = new JsonSerializerOptions { WriteIndented = true };
        var data = new int[] { 1, 2 };
        ResultSet<int> resultSet = Result.Success(data);
        var expectedJson =
            $$"""
            {
              "data": [
                1,
                2
              ],
              "success": true,
              "message": "{{ResponseMessages.Success}}",
              "errors": []
            }
            """;

        // Act
        var actual = JsonSerializer.Serialize(resultSet, options);

        // Assert
        actual.Should().BeEquivalentTo(expectedJson);
    }

    [Test]
    public void ResultSetOfT_ShouldSerializeResultOfReferenceType()
    {
        // Arrange
        var options = new JsonSerializerOptions { WriteIndented = true };
        var data = new Person[]
        { 
            new() { Name = "Bob" },
            new() { Name = "Alice" }
        };
        ResultSet<Person> resultSet = Result.Success(data);
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
              "success": true,
              "message": "{{ResponseMessages.Success}}",
              "errors": []
            }
            """;

        // Act
        var actual = JsonSerializer.Serialize(resultSet, options);

        // Assert
        actual.Should().BeEquivalentTo(expectedJson);
    }

    [Test]
    public void ResultSetOfT_ShouldDeserializeResultOfValueType()
    {
        // Arrange
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var data = new int[] { 1, 2 };
        ResultSet<int> expectedResultSet = Result.Success(data);
        var json =
            $$"""
            {
              "data": [
                1,
                2
              ],
              "success": true,
              "message": "{{ResponseMessages.Success}}",
              "errors": []
            }
            """;

        // Act
        var actual = JsonSerializer.Deserialize<ResultSet<int>>(json, options);

        // Assert
        actual.Should().BeEquivalentTo(expectedResultSet);
    }

    [Test]
    public void ResultSetOfT_ShouldDeserializeResultOfReferenceType()
    {
        // Arrange
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var data = new Person[]
        {
            new() { Name = "Bob" },
            new() { Name = "Alice" }
        };
        ResultSet<Person> expectedResultSet = Result.Success(data);
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
              "success": true,
              "message": "{{ResponseMessages.Success}}",
              "errors": []
            }
            """;

        // Act
        var actual = JsonSerializer.Deserialize<ResultSet<Person>>(json, options);

        // Assert
        actual.Should().BeEquivalentTo(expectedResultSet);
    }
}

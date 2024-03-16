namespace SimpleResults.Tests.Core.Json;

public class SystemTextJsonPagedResultOfT
{
    [Test]
    public void PagedResultOfT_ShouldSerializeResultOfValueType()
    {
        // Arrange
        var options = new JsonSerializerOptions { WriteIndented = true };
        var data = new List<int> { 1, 2, 3 };
        var pagedInfo = new PagedInfo(pageNumber: 1, pageSize: 5, totalRecords: 10);
        PagedResult<int> result = Result.Success(data, pagedInfo);
        var expectedJson =
            $$"""
            {
              "data": [
                1,
                2,
                3
              ],
              "pagedInfo": {
                "pageNumber": 1,
                "pageSize": 5,
                "totalRecords": 10,
                "totalPages": 2,
                "hasPrevious": false,
                "hasNext": true
              },
              "status": "Ok",
              "success": true,
              "message": "{{ResponseMessages.ObtainedResources}}",
              "errors": []
            }
            """;

        // Act
        var actual = JsonSerializer.Serialize(result, options);

        // Assert
        actual.Should().BeEquivalentTo(expectedJson);
    }

    [Test]
    public void PagedResultOfT_ShouldSerializeResultOfReferenceType()
    {
        // Arrange
        var options = new JsonSerializerOptions { WriteIndented = true };
        var data = new List<Person>
        {
            new() { Name = "Bob" },
            new() { Name = "Alice" }
        };
        var pagedInfo = new PagedInfo(pageNumber: 1, pageSize: 5, totalRecords: 10);
        PagedResult<Person> result = Result.Success(data, pagedInfo);
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
              "pagedInfo": {
                "pageNumber": 1,
                "pageSize": 5,
                "totalRecords": 10,
                "totalPages": 2,
                "hasPrevious": false,
                "hasNext": true
              },
              "status": "Ok",
              "success": true,
              "message": "{{ResponseMessages.ObtainedResources}}",
              "errors": []
            }
            """;

        // Act
        var actual = JsonSerializer.Serialize(result, options);

        // Assert
        actual.Should().BeEquivalentTo(expectedJson);
    }

    [Test]
    public void PagedResultOfT_ShouldDeserializeResultOfValueType()
    {
        // Arrange
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var data = new List<int> { 1, 2, 3 };
        var pagedInfo = new PagedInfo(pageNumber: 1, pageSize: 5, totalRecords: 10);
        PagedResult<int> expectedResult = Result.Success(data, pagedInfo);
        var json =
            $$"""
            {
              "data": [
                1,
                2,
                3
              ],
              "pagedInfo": {
                "pageNumber": 1,
                "pageSize": 5,
                "totalRecords": 10,
                "totalPages": 2,
                "hasPrevious": false,
                "hasNext": true
              },
              "status": "Ok",
              "success": true,
              "message": "{{ResponseMessages.ObtainedResources}}",
              "errors": []
            }
            """;

        // Act
        var actual = JsonSerializer.Deserialize<PagedResult<int>>(json, options);

        // Assert
        actual.Should().BeEquivalentTo(expectedResult);
    }

    [Test]
    public void PagedResultOfT_ShouldDeserializeResultOfReferenceType()
    {
        // Arrange
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var data = new List<Person>
        {
            new() { Name = "Bob" },
            new() { Name = "Alice" }
        };
        var pagedInfo = new PagedInfo(pageNumber: 1, pageSize: 5, totalRecords: 10);
        PagedResult<Person> expectedResult = Result.Success(data, pagedInfo);
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
              "pagedInfo": {
                "pageNumber": 1,
                "pageSize": 5,
                "totalRecords": 10,
                "totalPages": 2,
                "hasPrevious": false,
                "hasNext": true
              },
              "status": "Ok",
              "success": true,
              "message": "{{ResponseMessages.ObtainedResources}}",
              "errors": []
            }
            """;

        // Act
        var actual = JsonSerializer.Deserialize<PagedResult<Person>>(json, options);

        // Assert
        actual.Should().BeEquivalentTo(expectedResult);
    }
}

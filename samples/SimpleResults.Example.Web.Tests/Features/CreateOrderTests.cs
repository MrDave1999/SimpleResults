namespace SimpleResults.Example.Web.Tests.Features;

public class CreateOrderTests
{
    [TestCase(Routes.Order.ManualValidation)]
    [TestCase(Routes.Order.AutomaticValidation)]
    public async Task Post_WhenOrderIsCreated_ShouldReturnsHttpStatusCodeCreated(string requestUri)
    {
        // Arrange
        using var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();
        var orders = factory.Services.GetService<List<Order>>();
        var order = new Order
        {
            Description = "Description",
            DeliveryAddress = "DeliveryAddress",
            Details = new List<OrderDetail>
            {
                new()
                {
                    Product = "Product.",
                    Amount = 2,
                    Price = 5600
                }
            }
        };

        // Act
        var httpResponse = await client.PostAsJsonAsync(requestUri, order);
        var result = await httpResponse
            .Content
            .ReadFromJsonAsync<Result<CreatedGuid>>();
        var expectedId = orders[0].Id;

        // Asserts
        httpResponse.StatusCode.Should().Be(HttpStatusCode.Created);
        result.Data.Id.Should().Be(expectedId);
        result.IsSuccess.Should().BeTrue();
        result.Message.Should().NotBeNullOrEmpty();
        result.Errors.Should().BeEmpty();
        result.Status.Should().Be(ResultStatus.Created);
    }

    [TestCase(Routes.Order.ManualValidation)]
    [TestCase(Routes.Order.AutomaticValidation)]
    public async Task Post_WhenModelIsInvalid_ShouldReturnsHttpStatusCodeBadRequest(string requestUri)
    {
        // Arrange
        using var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();
        var order = new Order
        {
            Description = string.Empty,
            DeliveryAddress = string.Empty,
            Details = new List<OrderDetail>
            {
                new()
                {
                    Product = string.Empty,
                    Amount = default,
                    Price = default
                },
                new()
                {
                    Product = string.Empty,
                    Amount = default,
                    Price = default
                }
            }
        };
        var expectedErrors = new[]
        {
            "'Description' property failed validation. Error was: The Description field is required.",
            "'Description' property failed validation. Error was: The field Description must be a string or array type with a minimum length of '10'.",
            "'DeliveryAddress' property failed validation. Error was: The DeliveryAddress field is required.",
            "'DeliveryAddress' property failed validation. Error was: The field DeliveryAddress must be a string or array type with a minimum length of '10'.",
            "'Details[0].Price' property failed validation. Error was: The Price field is required.",
            "'Details[0].Amount' property failed validation. Error was: The Amount field is required.",
            "'Details[0].Product' property failed validation. Error was: The Product field is required.",
            "'Details[0].Product' property failed validation. Error was: The field Product must be a string or array type with a minimum length of '8'.",
            "'Details[1].Price' property failed validation. Error was: The Price field is required.",
            "'Details[1].Amount' property failed validation. Error was: The Amount field is required.",
            "'Details[1].Product' property failed validation. Error was: The Product field is required.",
            "'Details[1].Product' property failed validation. Error was: The field Product must be a string or array type with a minimum length of '8'."
        };

        // Act
        var httpResponse = await client.PostAsJsonAsync(requestUri, order);
        var result = await httpResponse
            .Content
            .ReadFromJsonAsync<Result<CreatedGuid>>();

        // Asserts
        httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        result.Data.Should().BeNull();
        result.IsSuccess.Should().BeFalse();
        result.Message.Should().NotBeNullOrEmpty();
        result.Errors.Should().BeEquivalentTo(expectedErrors);
        result.Status.Should().Be(ResultStatus.Invalid);
    }
}

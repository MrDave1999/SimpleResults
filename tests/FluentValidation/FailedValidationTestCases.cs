namespace SimpleResults.Tests.FluentValidation;

public class FailedValidationTestCases : IEnumerable
{
    public IEnumerator GetEnumerator()
    {
        yield return new object[]
        {
            new Order
            {
                Customer = string.Empty,
                Description = string.Empty,
                DeliveryAddress = default,
                Details = default
            },
            new[]
            {
                "'Customer' property failed validation. Error was: 'Customer' must not be empty.",
                "'Description' property failed validation. Error was: 'Description' must not be empty.",
                "'DeliveryAddress' property failed validation. Error was: 'Delivery Address' must not be empty.",
                "'Details' property failed validation. Error was: 'Details' must not be empty."
            }
        };

        yield return new object[]
        {
            new Order
            {
                Customer = string.Empty,
                Description = string.Empty,
                DeliveryAddress = new Address
                {
                    Description = string.Empty,
                    PostCode = string.Empty,
                    Country = string.Empty
                },
                Details = new List<OrderDetail>
                {
                    new()
                    {
                        Product = string.Empty,
                        Price = 0,
                        Amount = 0
                    },
                    new()
                    {
                        Product = string.Empty,
                        Price = -1,
                        Amount = -1
                    }
                }
            },
            new[]
            {
                "'Customer' property failed validation. Error was: 'Customer' must not be empty.",
                "'Description' property failed validation. Error was: 'Description' must not be empty.",
                "'DeliveryAddress.Description' property failed validation. Error was: 'Description' must not be empty.",
                "'DeliveryAddress.PostCode' property failed validation. Error was: 'Post Code' must not be empty.",
                "'DeliveryAddress.Country' property failed validation. Error was: 'Country' must not be empty.",
                "'Details[0].Product' property failed validation. Error was: 'Product' must not be empty.",
                "'Details[0].Price' property failed validation. Error was: 'Price' must be greater than '0'.",
                "'Details[0].Amount' property failed validation. Error was: 'Amount' must be greater than '0'.",
                "'Details[1].Product' property failed validation. Error was: 'Product' must not be empty.",
                "'Details[1].Price' property failed validation. Error was: 'Price' must be greater than '0'.",
                "'Details[1].Amount' property failed validation. Error was: 'Amount' must be greater than '0'."
            }
        };
    }
}

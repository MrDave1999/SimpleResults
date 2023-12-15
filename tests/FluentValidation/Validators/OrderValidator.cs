namespace SimpleResults.Tests.FluentValidation.Validators;

public class Order
{
    public string Customer { get; init; }
    public string Description { get; init; }
    public Address DeliveryAddress { get; init; }
    public IEnumerable<OrderDetail> Details { get; init; }
}

public class OrderValidator : AbstractValidator<Order>
{
    public OrderValidator()
    {
        RuleFor(o => o.Customer).NotEmpty();
        RuleFor(o => o.Description).NotEmpty();
        RuleFor(o => o.DeliveryAddress)
            .NotEmpty()
            .SetValidator(new DeliveryAddressValidator());
        RuleFor(o => o.Details).NotEmpty();
        RuleForEach(o => o.Details).SetValidator(new OrderDetailValidator());
    }
}

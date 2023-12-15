namespace SimpleResults.Tests.FluentValidation.Validators;

public class OrderDetail
{
    public string Product { get; init; }
    public int Amount { get; init; }
    public double Price { get; init; }
}

public class OrderDetailValidator : AbstractValidator<OrderDetail>
{
    public OrderDetailValidator()
    {
        RuleFor(o => o.Product).NotEmpty();
        RuleFor(o => o.Amount).GreaterThan(0);
        RuleFor(o => o.Price).GreaterThan(0);
    }
}

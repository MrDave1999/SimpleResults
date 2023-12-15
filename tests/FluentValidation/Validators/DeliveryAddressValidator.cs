namespace SimpleResults.Tests.FluentValidation.Validators;

public class Address
{
    public string Description { get; init; }
    public string Country { get; init; }
    public string Postcode { get; init; }
}

public class DeliveryAddressValidator : AbstractValidator<Address>
{
    public DeliveryAddressValidator()
    {
        RuleFor(d => d.Description).NotEmpty();
        RuleFor(d => d.Country).NotEmpty();
        RuleFor(d => d.Postcode).NotEmpty();
    }
}

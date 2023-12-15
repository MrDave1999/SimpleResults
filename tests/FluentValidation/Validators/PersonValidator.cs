namespace SimpleResults.Tests.FluentValidation.Validators;

public class PersonValidator : AbstractValidator<Person>
{
    public PersonValidator()
    {
        RuleFor(person => person.Name)
            .NotEmpty()
            .WithMessage("'Name' must not be empty.");
    }
}

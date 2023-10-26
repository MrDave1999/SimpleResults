namespace SimpleResults.Example.FluentValidation;

public class PersonService
{
    private readonly List<Person> _persons;

    public PersonService(List<Person> persons)
    {
        ArgumentNullException.ThrowIfNull(nameof(persons));
        _persons = persons;
    }

    public Result Create(Person person)
    {
        ValidationResult result = new PersonValidator().Validate(person);
        if(result.IsFailed())
        {
            return Result.Invalid(result.AsErrors());
        }

        _persons.Add(person);
        return Result.CreatedResource();
    }

    public ListedResult<Person> GetAll() 
    {
        if (_persons.Count == 0)
        {
            return Result.Failure("No person found");
        }

        return Result.Success(_persons);
    }
}

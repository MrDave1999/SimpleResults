namespace SimpleResults.Example.AspNetCore.Controllers;

[Tags("Person WebApi")]
[Route("Person-WebApi")]
public class PersonController
{
    private readonly PersonService _personService;

    public PersonController(PersonService personService)
    {
        _personService = personService;
    }

    [HttpPost]
    public async Task<Result> Create([FromBody]Person person)
    {
        await Task.Delay(100);
        return _personService.Create(person);
    }

    [HttpGet]
    public async Task<ListedResult<Person>> Get() 
    {
        await Task.Delay(100);
        return _personService.GetAll();
    }
}

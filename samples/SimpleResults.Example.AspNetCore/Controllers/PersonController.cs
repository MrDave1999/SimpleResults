namespace SimpleResults.Example.AspNetCore.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonController : ControllerBase
{
    private readonly PersonService _personService;

    public PersonController(PersonService personService)
    {
        _personService = personService;
    }

    [HttpPost]
    public Result Create([FromBody]Person person)
    {
        return _personService.Create(person);
    }

    [HttpGet]
    public ListedResult<Person> Get() 
    {
        return _personService.GetAll();
    }
}

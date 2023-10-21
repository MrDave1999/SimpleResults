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
    public ActionResult<Result> Create([FromBody]Person person)
    {
        return _personService
            .Create(person)
            .ToActionResult();
    }

    [HttpGet]
    public ActionResult<ListedResult<Person>> Get() 
    {
        return _personService
            .GetAll()
            .ToActionResult();
    }
}

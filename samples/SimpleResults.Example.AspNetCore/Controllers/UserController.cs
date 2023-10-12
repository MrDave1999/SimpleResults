namespace SimpleResults.Example.AspNetCore.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public ActionResult<Result<List<User>>> Get()
    {
        return _userService
            .GetAll()
            .ToActionResult();
    }

    [HttpGet("{id}")]
    public ActionResult<Result<User>> Get(string id)
    {
        return _userService
            .GetById(id)
            .ToActionResult();
    }

    [HttpPost]
    public ActionResult<Result<CreatedGuid>> Create([FromBody]UserRequest request)
    {
        return _userService
            .Create(request.Name)
            .ToActionResult();
    }

    [HttpPut("{id}")]
    public ActionResult<Result> Update(string id, [FromBody]UserRequest request)
    {
        return _userService
            .Update(id, request.Name)
            .ToActionResult();
    }

    [HttpDelete("{id}")]
    public ActionResult<Result> Delete(string id)
    {
        return _userService
            .Delete(id)
            .ToActionResult();
    }
}

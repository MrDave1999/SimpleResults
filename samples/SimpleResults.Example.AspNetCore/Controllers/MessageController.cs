namespace SimpleResults.Example.AspNetCore.Controllers;

[Tags("Message WebApi")]
[Route("Message-WebApi")]
public class MessageController
{
    [HttpGet("ban")]
    [Produces<Result>]
    public IResult Ban() => Result.Forbidden().ToHttpResult();

    [HttpGet("error")]
    [Produces<Result>]
    public IResult Error() => Result.CriticalError().ToHttpResult();

    [HttpGet("false-identity")]
    [Produces<Result>]
    public IResult FalseIdentity() => Result.Unauthorized().ToHttpResult();
}

# SimpleResults

[![SimpleResults](https://img.shields.io/nuget/vpre/SimpleResults?label=SimpleResults%20-%20nuget)](https://www.nuget.org/packages/SimpleResults)
[![SimpleResults-AspNetCore](https://img.shields.io/nuget/vpre/SimpleResults.AspNetCore?label=SimpleResults.AspNetCore%20-%20nuget)](https://www.nuget.org/packages/SimpleResults.AspNetCore)

[![SimpleResults-logo](https://raw.githubusercontent.com/MrDave1999/SimpleResults/master/SimpleResults-logo.png)](https://github.com/MrDave1999/SimpleResults)

A simple library to implement the Result pattern for returning from services.

> This library was inspired by [Arcadis.Result](https://github.com/ardalis/Result).

## Operation Result Pattern

The purpose of the Result design pattern is to give an operation (a method) the possibility to return a complex result (an object), allowing the consumer to:
- Access the result of an operation; in case there is one.
- Access the success indicator of an operation.
- Access the failure indicator of an operation.
- Access the value (data) of the result if it exists.
- Access the cause of the failure in case the operation was not successful.
- Access an error or success message.
- Access to a collection of error messages.

## Why did I make this library?

- I designed this library for use in the [DentallApp](https://github.com/DentallApp/back-end) project because no library like [Arcadis.Result](https://github.com/ardalis/Result) followed this response format:
```json
{
    "success": true,
    "data": { "id": 1 },
    "message": "..",
    "errors": ["..", ".."]
}
```
I couldn't change this format because the front-end used it, so I didn't want to make a breaking change.

- I do not want to throw [exceptions](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/exceptions/using-exceptions) for all cases.

## Why don't I use exceptions?

I usually throw exceptions when developing open source libraries to alert the developer immediately that an error has occurred and must be corrected. In this case, it makes sense to me to throw an exception because the developer can know exactly where the error originated (by looking at the stack trace).

However, when I develop applications I very rarely find a case for using exceptions.

For example, I could throw an exception when a normal user enters empty fields but this does not make sense to me, because it is an error caused by the end user (he/she manages the system from the user interface). So in this case throwing an exception is useless because:
- Stack trace included in the exception object is of no use to anyone, neither the end user nor the developer. 
  This is not a bug that a developer should be concerned about.

- Nobody cares where the error originated, whether it was in method X or Y, it doesn't matter.

And there are many more examples of errors caused by the end user: the email is duplicated or a password that does not comply with security policies, among others.

I only throw exceptions when the exception object is useful to someone (like a developer), otherwise, I use a result object to handle errors.

This is just my opinion, it is not an **absolute truth** either.

### Anecdote
> At work I had to implement a module to generate a report that performs a monthly comparison of income and expenses for a company, so it was necessary to create a function that is responsible for calculating the percentage of a balance per month:
```cs
Percentage.Calculate(double amount, double total);
```
> The `total` parameter if it is zero, will cause a division by zero (undefined operation), however, this value was not provided by an **end user**, but by the **income and expense reporting module**, but since I did not implement this module correctly, I created a bug, so the algorithm was passing a zero value for a strange reason (I call this a logic error, caused by the developer). 

> Since I didn't throw an exception in the `Percentage.Calculate` function, it took me a couple of minutes to find out where the error originated (I didn't know that the problem was a division by zero).

> If I had thrown an exception, I would have found the error very quickly, just by looking at the stack trace, oh yeah. In this case, it is very useful the exception object, for me and other developers.

## Installation

Run the following command from the terminal:
```
dotnet add package SimpleResults --prerelease
```
Or you can also install the package for [ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-7.0):
```
dotnet add package SimpleResults.AspNetCore --prerelease
```

## Usage

This example is simple and is based on the [EF Core introductory tutorial](https://learn.microsoft.com/en-us/ef/core/get-started/overview/first-app?tabs=netcore-cli).
```cs
using SimpleResults;

public class BlogService
{
    private readonly BloggingContext _db;

    public BlogService(BloggingContext db)
    {
        _db = db;
    }

    public Result<CreatedId> Create(string url)
    {
        if(string.IsNullOrWhiteSpace(url))
        {
            return Result.Invalid();
        }

        var blog = new Blog { Url = "http://blogs.msdn.com/adonet" };
        _db.Add(blog);
        _db.SaveChanges();
        return Result.CreatedResource(blog.BlogId);
    }

    public Result<Blog> Read(int id)
    {
        if(id < 0)
        {
            return Result.Invalid("ID must not be negative");
        }

        var blog = _db.Blogs
            .Where(b => b.BlogId == id)
            .FirstOrDefault();

        if(blog is null)
        {
            return Result.NotFound();
        }

        return Result.Success(blog);
    }
}
```
This approach provides a new way to handle error without the need to use exceptions.

### Integration with ASP.NET Core

You can convert the `Result` object to an [ActionResult](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.actionresult-1?view=aspnetcore-7.0), such as:
```cs
using SimpleResults;

public class BlogRequest 
{ 
    public string Url { get; init; }
}

[ApiController]
[Route("[controller]")]
public class BlogController : ControllerBase
{
    private readonly BlogService _blogService;

    public BlogController(BlogService blogService)
    {
        _blogService = blogService;
    }

    [HttpGet("{id}")]
    public ActionResult<Result<Blog>> Get(int id)
    {
        return _blogService
            .Read(id)
            .ToActionResult();
    }

    [HttpPost]
    public ActionResult<Result<CreatedId>> Create([FromBody]BlogRequest request)
    {
        return _blogService
            .Create(request.Url)
            .ToActionResult();
    }
}
```

You can find a complete and functional example in these projects:
- [SimpleResults.Example](https://github.com/MrDave1999/SimpleResults/tree/master/samples/SimpleResults.Example)
- [SimpleResults.Example.AspNetCore](https://github.com/MrDave1999/SimpleResults/tree/master/samples/SimpleResults.Example.AspNetCore)

## Contribution

Any contribution is welcome! Remember that you can contribute not only in the code, but also in the documentation or even improve the tests.

Follow the steps below:

- Fork it
- Create your feature branch (git checkout -b my-new-feature)
- Commit your changes (git commit -am 'Added some feature')
- Push to the branch (git push origin my-new-feature)
- Create new Pull Request
# SimpleResults

[![SimpleResults](https://img.shields.io/nuget/vpre/SimpleResults?label=SimpleResults%20-%20nuget)](https://www.nuget.org/packages/SimpleResults)
[![SimpleResults-AspNetCore](https://img.shields.io/nuget/vpre/SimpleResults.AspNetCore?label=SimpleResults.AspNetCore%20-%20nuget)](https://www.nuget.org/packages/SimpleResults.AspNetCore)

[![SimpleResults-logo](https://raw.githubusercontent.com/MrDave1999/SimpleResults/master/SimpleResults-logo.png)](https://github.com/MrDave1999/SimpleResults)

A simple library to implement the Result pattern for returning from services.

> This library was inspired by [Arcadis.Result](https://github.com/ardalis/Result).

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

You can convert the `Result` object to an [ActionResult<T>](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.actionresult-1?view=aspnetcore-7.0), such as:
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
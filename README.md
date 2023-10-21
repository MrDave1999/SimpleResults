# SimpleResults

[![SimpleResults](https://img.shields.io/nuget/vpre/SimpleResults?label=SimpleResults%20-%20nuget&color=red)](https://www.nuget.org/packages/SimpleResults)
[![downloads](https://img.shields.io/nuget/dt/SimpleResults?color=yellow)](https://www.nuget.org/packages/SimpleResults)

[![SimpleResults-AspNetCore](https://img.shields.io/nuget/vpre/SimpleResults.AspNetCore?label=SimpleResults.AspNetCore%20-%20nuget&color=red)](https://www.nuget.org/packages/SimpleResults.AspNetCore)
[![downloads](https://img.shields.io/nuget/dt/SimpleResults.AspNetCore?color=yellow)](https://www.nuget.org/packages/SimpleResults.AspNetCore)

[![SimpleResults-logo](https://raw.githubusercontent.com/MrDave1999/SimpleResults/master/SimpleResults-logo.png)](https://github.com/MrDave1999/SimpleResults)

A simple library to implement the Result pattern for returning from services.

> This library was inspired by [Arcadis.Result](https://github.com/ardalis/Result).

## Index

- [Operation Result Pattern](#operation-result-pattern)
- [Why did I make this library?](#why-did-i-make-this-library)
- [Why don't I use exceptions?](#why-dont-i-use-exceptions)
  - [Anecdote](#anecdote)
  - [Interesting resource about exceptions](#interesting-resource-about-exceptions)
- [Installation](#installation)
- [Overview](#overview)
  - [Using the Result type](#using-the-result-type)
  - [Using the ListedResult type](#using-the-listedresult-type)
  - [Using the PagedResult type](#using-the-pagedresult-type)
  - [Creating a resource with Result type](#creating-a-resource-with-resultt-type)
  - [Integration with ASP.NET Core](#integration-with-aspnet-core)
  - [Translate Result object to HTTP status code](#translate-result-object-to-http-status-code)
  - [Integration with Fluent Validation](#integration-with-fluent-validation)
- [Samples](#samples)
- [Language settings](#language-settings)
- [Contribution](#contribution)

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

I only throw exceptions when the exception object is useful to someone (like a developer); otherwise, I use a **Result object** to handle errors. I use **return statements** in my methods to create the error.

This is just my opinion, it is not an **absolute truth** either. My point of view is more philosophical, so the purpose of my paragraphs is not to indicate the disadvantages of using exceptions, but to explain why for me it does not make sense in some cases to throw exceptions.

### Anecdote
> At work I had to implement a module to generate a report that performs a monthly comparison of income and expenses for a company, so it was necessary to create a function that is responsible for calculating the percentage of a balance per month:
```cs
Percentage.Calculate(double amount, double total);
```
> The `total` parameter if it is zero, will cause a division by zero (undefined operation), however, this value was not provided by an **end user**, but by the **income and expense reporting module**, but since I did not implement this module correctly, I created a bug, so the algorithm was passing a zero value for a strange reason (I call this a logic error, caused by the developer). 

> Since I didn't throw an exception in the `Percentage.Calculate` function, it took me a couple of minutes to find out where the error originated (I didn't know that the problem was a division by zero).

> Dividing a floating-point value by zero doesn't throw an exception; it result is not a number (NaN). 
This was a surprise to me! I didn't know! I was expecting an exception but it was not the case.

> If I had thrown an exception, I would have found the error very quickly, just by looking at the stack trace, oh yeah. In this case, it is very useful the exception object, for me and other developers.

### Interesting resource about exceptions

- [Exceptions for flow control in C# by Vladimir Khorikov](https://enterprisecraftsmanship.com/posts/exceptions-for-flow-control)

## Installation

Run the following command from the terminal:
```
dotnet add package SimpleResults --prerelease
```
Or you can also install the package for [ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-7.0):
```
dotnet add package SimpleResults.AspNetCore --prerelease
```

## Overview

You must import the namespace types at the beginning of your class file:
```cs
using SimpleResults;
```

This library provides four main types:
- `Result`
- `Result<TValue>`
- `ListedResult<TValue>`
- `PagedResult<TValue>` and `PagedInfo`

With any of these types you can handle errors and at the same time generate errors with the `return` statement.

This approach provides a new way to generate an error using return statements without the need to throw exceptions.

### Using the `Result` type

You can use the `Result` class when you do not want to return any value.

**Example:**
```cs
public class UserService
{
    private readonly List<User> _users;
    public UserService(List<User> users) => _users = users;

    public Result Update(string id, string name)
    {
        if (string.IsNullOrWhiteSpace(id))
            return Result.Invalid("ID is required");

        if (string.IsNullOrWhiteSpace(name))
            return Result.Invalid("Name is required");

        var user = _users.Find(u => u.Id == id);
        if (user is null)
            return Result.NotFound();

        user.Name = name;
        return Result.UpdatedResource();
    }
}
```

You can use the `Result<TValue>` class when you want to return a value (such as a `User` object).

**Example:**
```cs
public class UserService
{
    private readonly List<User> _users;
    public UserService(List<User> users) => _users = users;

    public Result<User> GetById(string id)
    {
        if(string.IsNullOrWhiteSpace(id))
            return Result.Invalid("ID is required");

        var user = _users.Find(u => u.Id == id);
        if(user is null)
            return Result.NotFound();

        return Result.Success(user, "User found");
    }
}
```

### Using the `ListedResult` type

You can use the `ListedResult<TValue>` class when you want to return a set of values (such as a collection of objects of type `User`).

**Example:**
```cs
public class UserService
{
    private readonly List<User> _users;
    public UserService(List<User> users) => _users = users;

    public ListedResult<User> GetAll()
    {
        if(_users.Count == 0)
            return Result.Failure("No user found");

        return Result.ObtainedResources(_users);
    }
}
```

### Using the `PagedResult` type

You can use the `PagedResult<TValue>` class when you want to include paged information and a data collection in the result.

**Example:**
```cs
public class UserService
{
    private readonly List<User> _users;
    public UserService(List<User> users) => _users = users;

    public PagedResult<User> GetPagedList(int pageNumber, int pageSize)
    {
        if(pageNumber <= 0)
            return Result.Invalid("PageNumber must be greater than zero");

        int itemsToSkip = (pageNumber - 1) * pageSize;
        var data = _users
            .Skip(itemsToSkip)
            .Take(pageSize);

        if (data.Any())
        {
            var pagedInfo = new PagedInfo(pageNumber, pageSize, _users.Count);
            return Result.Success(data, pagedInfo);
        }

        return Result.Failure("No results found");
    }
}
```
### Creating a resource with `Result<T>` type

You can tell the method to return a successfully created resource as a result by using the `Result.CreatedResource` method.
In addition, you can use the `CreatedGuid` class to specify the ID assigned to the created resource.

**Example:**
```cs
public class UserService
{
    private readonly List<User> _users;
    public UserService(List<User> users) => _users = users;

    public Result<CreatedGuid> Create(string name)
    {
        if(string.IsNullOrWhiteSpace(name))
            return Result.Invalid("Name is required");

        var guid = Guid.NewGuid();
        _users.Add(new User { Id = guid.ToString(), Name = name });
        return Result.CreatedResource(guid);
    }
}
```
You can also use the `CreatedId` class when using an **integer** as identifier. 

An example using [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/get-started/overview/first-app?tabs=netcore-cli):
```cs
public class UserModel 
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class UserService
{
    private readonly DbContext _db;
    public UserService(DbContext db) => _db = db;

    public Result<CreatedId> Create(string name)
    {
        if(string.IsNullOrWhiteSpace(name))
            return Result.Invalid("Name is required");

        var user = new UserModel { Name = name };
        _db.Add(user);
        _db.SaveChanges();
        return Result.CreatedResource(user.Id);
    }
}
```

### Integration with ASP.NET Core

You can convert the `Result` object to an [ActionResult](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.actionresult-1?view=aspnetcore-7.0) using the `ToActionResult` extension method.

You need to install the [SimpleResults.AspNetCore](https://www.nuget.org/packages/SimpleResults.AspNetCore) package to have access to the extension method.

**Example:**
```cs
public class UserRequest 
{ 
    public string Name { get; init; }
}

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;
    public UserController(UserService userService) => _userService = userService;

    [HttpPost]
    public ActionResult<Result<CreatedGuid>> Create([FromBody]UserRequest request)
        => _userService.Create(request.Name).ToActionResult();

    [HttpPut("{id}")]
    public ActionResult<Result> Update(string id, [FromBody]UserRequest request)
        => _userService.Update(id, request.Name).ToActionResult();

    [HttpGet("{id}")]
    public ActionResult<Result<User>> Get(string id)
        => _userService.GetById(id).ToActionResult();

    [HttpGet("paged")]
    public ActionResult<PagedResult<User>> GetPagedList([FromQuery]PagedRequest request)
        => _userService
        .GetPagedList(request.PageNumber, request.PageSize)
        .ToActionResult();

    [HttpGet]
    public ActionResult<ListedResult<User>> Get()
        => _userService.GetAll().ToActionResult();
}
```
### Translate Result object to HTTP status code

[SimpleResults.AspNetCore](https://www.nuget.org/packages/SimpleResults.AspNetCore) package is responsible for translating the status of a Result object into an HTTP status code.

The following table is used as a reference to know which type of result corresponds to an HTTP status code:

| Result type             | HTTP status code            |
|-------------------------|-----------------------------|
| Result.Success          | 200 - Ok                    |
| Result.CreatedResource  | 201 - Created               |
| Result.UpdatedResource  | 200 - Ok                    |
| Result.DeletedResource  | 200 - Ok                    |
| Result.ObtainedResource | 200 - Ok                    |
| Result.Invalid          | 400 - Bad Request           |
| Result.NotFound         | 404 - Not Found             |
| Result.Unauthorized     | 401 - Unauthorized          |
| Result.Conflict         | 409 - Conflict              |
| Result.Failure          | 422 - Unprocessable Entity  |
| Result.CriticalError    | 500 - Internal Server Error |

### Integration with Fluent Validation

You do not need to install any additional packages, you only need [Fluent Validation](https://www.nuget.org/packages/FluentValidation).

**Example:**
```cs
// Define extension methods for ValidationResult class.
public static class ValidationResultExtensions
{
    public static bool IsFailed(this ValidationResult result) => !result.IsValid;
    public static IEnumerable<string> AsErrors(this ValidationResult result)
        => result.Errors.Select(failure => failure.ErrorMessage);
}

public class UserService
{
    public Result Create(CreateUserRequest request)
    {
        ValidationResult result = new CreateUserValidator().Validate(request);
        if(result.IsFailed())
            return Result.Invalid(result.AsErrors());

        // Some code..
    }
}
```

## Samples

You can find a complete and functional example in these projects:
- [SimpleResults.Example](https://github.com/MrDave1999/SimpleResults/tree/master/samples/SimpleResults.Example)
- [SimpleResults.Example.AspNetCore](https://github.com/MrDave1999/SimpleResults/tree/master/samples/SimpleResults.Example.AspNetCore)
- [SimpleResults.Example.Web.Tests](https://github.com/MrDave1999/SimpleResults/tree/master/samples/SimpleResults.Example.Web.Tests)
- [SimpleResults.Example.FluentValidation](https://github.com/MrDave1999/SimpleResults/tree/master/samples/SimpleResults.Example/FluentValidation)

## Language settings
`SimpleResults` uses default messages in English. You can change the language in this way:
```cs
// Allows to load the resource in Spanish.
Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("es");
```
In ASP.NET Core applications, the [UseRequestLocalization](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.applicationbuilderextensions.userequestlocalization?view=aspnetcore-7.0#microsoft-aspnetcore-builder-applicationbuilderextensions-userequestlocalization(microsoft-aspnetcore-builder-iapplicationbuilder-system-string())) extension method is used:
```cs
app.UseRequestLocalization("es");
```
At the moment, only two languages are available:
- English
- Spanish

Feel free to contribute :D

## Contribution

Any contribution is welcome! Remember that you can contribute not only in the code, but also in the documentation or even improve the tests.

Follow the steps below:

- Fork it
- Create your feature branch (git checkout -b my-new-feature)
- Commit your changes (git commit -am 'Added some feature')
- Push to the branch (git push origin my-new-feature)
- Create new Pull Request
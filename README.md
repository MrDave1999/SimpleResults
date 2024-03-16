# SimpleResults

[![SimpleResults](https://img.shields.io/nuget/vpre/SimpleResults?label=SimpleResults%20-%20nuget&color=red)](https://www.nuget.org/packages/SimpleResults)
[![downloads](https://img.shields.io/nuget/dt/SimpleResults?color=yellow)](https://www.nuget.org/packages/SimpleResults)

[![SimpleResults-AspNetCore](https://img.shields.io/nuget/vpre/SimpleResults.AspNetCore?label=SimpleResults.AspNetCore%20-%20nuget&color=red)](https://www.nuget.org/packages/SimpleResults.AspNetCore)
[![downloads](https://img.shields.io/nuget/dt/SimpleResults.AspNetCore?color=yellow)](https://www.nuget.org/packages/SimpleResults.AspNetCore)

[![SimpleResults-FluentValidation](https://img.shields.io/nuget/vpre/SimpleResults.FluentValidation?label=SimpleResults.FluentValidation%20-%20nuget&color=red)](https://www.nuget.org/packages/SimpleResults.FluentValidation)
[![downloads](https://img.shields.io/nuget/dt/SimpleResults.FluentValidation?color=yellow)](https://www.nuget.org/packages/SimpleResults.FluentValidation)

[![SimpleResults-logo](https://raw.githubusercontent.com/MrDave1999/SimpleResults/master/SimpleResults-logo.png)](https://github.com/MrDave1999/SimpleResults)

A simple library to implement the Result pattern for returning from services. It also provides a mechanism for translating the Result object to an [ActionResult](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.actionresult?view=aspnetcore-7.0) or [IResult](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.iresult?view=aspnetcore-7.0).

> This library was inspired by [Ardalis.Result](https://github.com/ardalis/Result).

See the [API documentation](https://mrdave1999.github.io/SimpleResults/api/SimpleResults.html) for more information on this project.

## Index

- [Operation Result Pattern](#operation-result-pattern)
- [Why did I make this library?](#why-did-i-make-this-library)
- [Why don't I use exceptions?](#why-dont-i-use-exceptions)
  - [Differences between an expected and unexpected error](#differences-between-an-expected-and-unexpected-error)
  - [Anecdote](#anecdote)
  - [What happens if exceptions are used for all situations?](#what-happens-if-exceptions-are-used-for-all-situations)
  - [Interesting resource about exceptions](#interesting-resource-about-exceptions)
- [Installation](#installation)
- [Overview](#overview)
  - [Using the Result type](#using-the-result-type)
  - [Using the ListedResult type](#using-the-listedresult-type)
  - [Using the PagedResult type](#using-the-pagedresult-type)
  - [Creating a resource with Result type](#creating-a-resource-with-resultt-type)
  - [Designing errors and success messages](#designing-errors-and-success-messages)
  - [Integration with ASP.NET Core](#integration-with-aspnet-core)
    - [Using TranslateResultToActionResult as an action filter](#using-translateresulttoactionresult-as-an-action-filter)
    - [Add action filter as global](#add-action-filter-as-global)
    - [Support for Minimal APIs](#support-for-minimal-apis)
    - [Validating with the ModelState property](#validating-with-the-modelstate-property)
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

- I designed this library for use it in the [DentallApp](https://github.com/DentallApp/back-end) project and for other projects according to my needs.

- I wanted to share my knowledge with the community. I love open source.

- I do not want to throw [exceptions](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/exceptions/using-exceptions) for all situations.

## Why don't I use exceptions?

I usually throw exceptions when developing open source libraries to alert the developer immediately that an unexpected error has occurred and must be corrected. In this case, it makes sense to me to throw an exception because the developer can know exactly where the error originated (by looking at the stack trace). However, when I develop applications, I very rarely find a case for using exceptions.

For example, I could throw an exception when a normal user enters empty fields but this does not make sense to me, because it is an error caused by the end user (who manages the system from the user interface). So in this case throwing an exception is useless because:
- Stack trace included in the exception object is of no use to anyone, neither the end user nor the developer. 
  This is not a bug that a developer should be concerned about.

- Nobody cares where the error originated, whether it was in method X or Y, it doesn't matter.

- It is not an unexpected error. An exception is thrown to indicate an unexpected error. Unexpected errors are those that are not expected to occur, and they are not recoverable.
  - For example, if the database server is not online, it will produce an unexpected error in the application, so there is no way for the application to recover.

And there are many more examples of errors caused by the end user: the email is duplicated or a password that does not comply with security policies, among others.

I only throw exceptions for unexpected errors; otherwise I create **result objects** and use return statements in my methods to terminate execution immediately when an expected error occurs.

### Differences between an expected and unexpected error

It is necessary to understand the differences between an expected and unexpected error in order to know when to throw exceptions. In fact, in practice, third-party dependencies are responsible for reporting unexpected errors, so the developer only has to worry about identifying the expected errors of his business application.

- **Expected errors** are those that are expected to occur, and we tend to recover them. They are also known as recoverable errors.
  - For example, empty fields or a duplicate email. These are errors that are expected to occur and are normal for them to happen.
  - To handle these errors, it is useful to use the Result pattern.

- **Unexpected errors** are those that are not expected to occur, and they are not recoverable. They are also known as non-recoverable errors.
  - For example, a database that does not exist or an incorrectly typed connection string. These are errors that are not expected to occur and it is not normal for them to happen. They should never happen and should be corrected immediately. It is fatal.
  - Exceptions were designed to represent unexpected errors.

### Anecdote
> At work I had to implement a module to generate a report that performs a monthly comparison of income and expenses for a company, so it was necessary to create a function that is responsible for calculating the percentage of a balance per month:
```cs
Percentage.Calculate(double amount, double total);
```
> The `total` parameter if it is zero, will cause a division by zero (undefined operation), however, this value was not provided by an **end user**, but by the **income and expense reporting module**, but since I did not implement this module correctly, I created a bug, so the algorithm was passing a zero value for a strange reason (I call this a logic error, caused by the developer). 

> Since I didn't throw an exception in the `Percentage.Calculate` function, it took me a couple of minutes to find out where the error originated (I didn't know that the problem was a division by zero).

> Dividing a floating-point value by zero doesn't throw an exception; it result is not a number (NaN). 
This was a surprise to me! I didn't know! I was expecting an exception but it was not the case.

> If I had thrown an exception, I would have found the error very quickly, just by looking at the stack trace. In this case, it is very useful the exception object, for me and other developers and yes, divide by zero is an **unexpected error**, an exception should be thrown.

### What happens if exceptions are used for all situations?

**There are some details to consider:**

- New maintainers of your application will learn that it is okay to throw exceptions in all situations. This is bad for their learning, as they don't really understand what [exceptions](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/exceptions) were designed for in C#.

- You make your code confusing, since you don't follow the official definition of what an [exception](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/exceptions) is in C#.
  - Yes, exceptions represent unexpected errors. This is the definition, changing it, only causes confusion.

- You need to create custom classes that inherit from the Exception type, otherwise you end up using the Exception type in many places. This type does not express any information to the consumer (who calls the public API).

- You need to document those methods that throw exceptions, otherwise the consumer will not know which exceptions to handle, and will end up reviewing the source code of the method (this is not good).

- Performance. Yes, throwing exceptions is very expensive. Although in many applications there may not be any impact, it is not a justification for wasting resources unnecessarily. For more information, see these links: 
  - [Exceptions and Exception Handling](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/exceptions)
  - [Thread contention while throwing and catching exceptions](https://github.com/dotnet/runtime/issues/97181)
  - [Exceptions are extremely expensive](https://github.com/dotnet/aspnetcore/issues/46280?fbclid=IwAR25e2CFNrI0VhS_H8V4WzgmR_JkXrXTqVn0vpNUFnyCMa9LC9GtnfzMvRU#issuecomment-1527898867)

- If your project is a web application, you will have to find a mechanism to translate the exception object to HTTP status code, so you will have to create base classes like InvalidDataException to catch it from a global exception handler. 
  - For example: `WrongEmailException` inherits from `InvalidDataException` and in turn, it inherits from `Exception`. 
    It is necessary to think of a hierarchy of types that use inheritance (this adds another complexity).

### Interesting resource about exceptions

- [Exceptions for flow control in C# by Vladimir Khorikov](https://enterprisecraftsmanship.com/posts/exceptions-for-flow-control)
- [Exceptions and Result pattern by Ben Witt](https://medium.com/@wgyxxbf/result-pattern-a01729f42f8c)

## Installation

You can run any of these commands from the terminal:
```sh
dotnet add package SimpleResults
dotnet add package SimpleResults.AspNetCore
dotnet add package SimpleResults.FluentValidation
```
[SimpleResults](https://www.nuget.org/packages/SimpleResults) package is the main library (the core). The other two packages complement the main library (they are like add-ons).

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

See the [API documentation](https://mrdave1999.github.io/SimpleResults/api/SimpleResults.html) for more information on these types.

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

### Designing errors and success messages

You can create an object that represents an error or success message. The advantage is all the relevant information of an error or success is encapsulated within one object.

**Example:**
```cs
public readonly ref struct StartDateIsAfterEndDateError
{
    public string Message { get; }
    public StartDateIsAfterEndDateError(DateTime startDate, DateTime endDate)
    { 
        Message = string.Format(
            "The start date {0} is after the end date {1}", 
            startDate.ToString("yyyy-MM-dd"), 
            endDate.ToString("yyyy-MM-dd"));
    }
}
```
This approach allows you to change the format of the message without having to make changes elsewhere.

And then you can use it in your service:
```cs
public class UserService
{
    public Result<List<User>> GetUsersByDateRange(DateTime startDate, DateTime endDate)
    {
        if(startDate > endDate)
            return Result.Invalid(new StartDateIsAfterEndDateError(startDate, endDate).Message);

        // Do something..
    }
}
```

### Integration with ASP.NET Core

You can convert the Result object to a [Microsoft.AspNetCore.Mvc.ActionResult](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.actionresult-1?view=aspnetcore-7.0) using the [ToActionResult](https://mrdave1999.github.io/SimpleResults/api/SimpleResults.ResultExtensions.ToActionResult.html) extension method.

You need to install the [SimpleResults.AspNetCore](https://www.nuget.org/packages/SimpleResults.AspNetCore) package to have access to the extension method. See the [ResultExtensions](https://mrdave1999.github.io/SimpleResults/api/SimpleResults.ResultExtensions.html#methods) class to find all extension methods.

**Example:**
```cs
public class UserRequest 
{ 
    public string Name { get; init; }
}

[ApiController]
[Route("[controller]")]
public class UserController
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
See the [API documentation](https://mrdave1999.github.io/SimpleResults/api/SimpleResults.ResultExtensions.html#methods) for a list of available extension methods.

#### Using TranslateResultToActionResult as an action filter

You can also use the [TranslateResultToActionResult](https://mrdave1999.github.io/SimpleResults/api/SimpleResults.TranslateResultToActionResultAttribute.html) filter to translate the Result object to [ActionResult](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.actionresult?view=aspnetcore-7.0). 

`TranslateResultToActionResultAttribute` class will internally call the `ToActionResult` method and perform the translation.

**Example:**
```cs
[TranslateResultToActionResult]
[ApiController]
[Route("[controller]")]
public class UserController
{
    private readonly UserService _userService;
    public UserController(UserService userService) => _userService = userService;

    [HttpGet("{id}")]
    public Result<User> Get(string id) => _userService.GetById(id);
}
```
The return value of `Get` action is a `Result<User>`. **After the action is executed**, the filter (i.e. `TranslateResultToActionResult`) will run and translate the `Result<User>` to [ActionResult](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.actionresult?view=aspnetcore-7.0).

[See the source code](https://github.com/MrDave1999/SimpleResults/blob/e0b4bf02c77c02862f0a95ded268ee05cbc6d033/src/AspNetCore/TranslateResultToActionResultAttribute.cs#L12), it is very simple.

#### Add action filter as global

If you do not want to use the filter on each controller, you can add it globally for all controllers (see [sample](https://github.com/MrDave1999/SimpleResults/blob/e0b4bf02c77c02862f0a95ded268ee05cbc6d033/samples/SimpleResults.Example.AspNetCore/Program.cs#L10C1-L14C4)).
```cs
builder.Services.AddControllers(options =>
{
    // Add filter for all controllers.
    options.Filters.Add<TranslateResultToActionResultAttribute>();
});
```
This way you no longer need to add the `TranslateResultToActionResult` attribute on each controller or individual action.

#### Support for Minimal APIs

As of version 2.3.0, a feature has been added to convert the Result object to an implementation of [Microsoft.AspNetCore.Http.IResult](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.iresult?view=aspnetcore-7.0).

You only need to use the extension method called [ToHttpResult](https://mrdave1999.github.io/SimpleResults/api/SimpleResults.ResultExtensions.ToHttpResult.html). See the [ResultExtensions](https://mrdave1999.github.io/SimpleResults/api/SimpleResults.ResultExtensions.html#methods) class to find all extension methods.

**Example:**
```cs
public static class UserEndpoint
{
    public static void AddRoutes(this WebApplication app)
    {
        var userGroup = app
            .MapGroup("/User")
            .WithTags("User");

        userGroup
            .MapGet("/", (UserService service) => service.GetAll().ToHttpResult())
            .Produces<ListedResult<User>>();

        userGroup
            .MapGet("/{id}", (string id, UserService service) => service.GetById(id).ToHttpResult())
            .Produces<Result<User>>();

        userGroup.MapPost("/", ([FromBody]UserRequest request, UserService service) =>
        {
            return service.Create(request.Name).ToHttpResult();
        })
        .Produces<Result<CreatedGuid>>();
    }
}
```
You can also use the [TranslateResultToHttpResult](https://mrdave1999.github.io/SimpleResults/api/SimpleResults.TranslateResultToHttpResultFilter.html) filter to translate the Result object to an implementation of [IResult](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.iresult?view=aspnetcore-7.0). 

`TranslateResultToHttpResultFilter` class will internally call the `ToHttpResult` method and perform the translation.

**Example:**
```cs
public static class UserEndpoint
{
    public static void AddRoutes(this WebApplication app)
    {
        var userGroup = app
            .MapGroup("/User")
            .WithTags("User")
            .AddEndpointFilter<TranslateResultToHttpResultFilter>();

        userGroup
            .MapGet("/{id}", (string id, UserService service) => service.GetById(id))
            .Produces<Result<User>>();
    }
}
```
The endpoint handler returns a `Result<User>`. After the handler is executed, the filter (i.e. `TranslateResultToHttpResult`) will run and translate the `Result<User>` to an implementation of [IResult](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.iresult?view=aspnetcore-7.0).

[See the source code](https://github.com/MrDave1999/SimpleResults/blob/25387945f57241dadad3baf52886ab59949c98fa/src/AspNetCore/TranslateResultToHttpResultFilter.cs#L26), it is very simple.

#### Validating with the ModelState property

[SimpleResults.AspNetCore](https://www.nuget.org/packages/SimpleResults.AspNetCore) package also adds extension methods for the [ModelStateDictionary](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.modelbinding.modelstatedictionary?view=aspnetcore-7.0) type.

See the [ModelStateDictionaryExtensions](https://mrdave1999.github.io/SimpleResults/api/SimpleResults.SRModelStateDictionaryExtensions.html) class to find all extension methods.

The `ModelStateDictionary` type contains the validation errors that are displayed to the client. Somehow these errors must be included in an instance of type [Result](https://mrdave1999.github.io/SimpleResults/api/SimpleResults.Result.html).

##### Manual validation

Manual validation is performed directly in the controller action.

**Example:**
```cs
[TranslateResultToActionResult]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly OrderService _orderService;
    public OrderController(OrderService orderService) => _orderService = orderService;

    [HttpPost]
    public Result<CreatedGuid> Create([FromBody]CreateOrderRequest request)
    {
        if (ModelState.IsFailed())
            return ModelState.Invalid();

        return _orderService.Create(request);
    }
}
```
In this example a manual validation is performed with `ModelState.IsFailed()` (an extension method), so if the model state is failed, an invalid result type is returned. What `ModelState.Invalid()` does is to convert the instance of `ModelStateDictionary` to an instance of type [Result](https://mrdave1999.github.io/SimpleResults/api/SimpleResults.Result.html), so in the result object the validation errors will be added.

After the controller action is executed, the `TranslateResultToActionResult` filter will translate the Result object to an instance of type `ActionResult`.

You can also return the ActionResult directly in the controller action instead of using the action filter.

**Example:**
```cs
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly OrderService _orderService;
    public OrderController(OrderService orderService) => _orderService = orderService;

    [HttpPost]
    public ActionResult<Result<CreatedGuid>> Create([FromBody]CreateOrderRequest request)
    {
        if (ModelState.IsFailed())
            return ModelState.BadRequest();

        return _orderService
            .Create(request)
            .ToActionResult();
    }
}
```
`ModelState.BadRequest()` has a behavior similar to `ModelState.Invalid()`, the difference is that the first one returns an instance of type [BadRequestObjectResult](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.badrequestobjectresult?view=aspnetcore-7.0) in which contains the instance of type [Result](https://mrdave1999.github.io/SimpleResults/api/SimpleResults.Result.html).

##### Automatic validation

You need to make a setting in the `Program.cs` to convert the instance of type `ModelStateDictionary` to an instance of type [Result](https://mrdave1999.github.io/SimpleResults/api/SimpleResults.Result.html) when the model validation fails.

**Example:**
```cs
builder.Services.AddControllers()
.ConfigureApiBehaviorOptions(options =>
{
    options.InvalidModelStateResponseFactory = (ActionContext context) => context.ModelState.BadRequest();
});
```
This delegate is only invoked on actions annotated with `ApiControllerAttribute` and will execute the `context.ModelState.BadRequest()` call when a model validation failure occurs. If a validation failure occurs in the model, the controller action will never be executed.

Your controller no longer needs to perform manual validation, for example:
```cs
[ApiController]
[TranslateResultToActionResult]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly OrderService _orderService;
    public OrderController(OrderService orderService) => _orderService = orderService;

    [HttpPost]
    public Result<CreatedGuid> Create([FromBody]CreateOrderRequest request)
        => _orderService.Create(request);
}
```
The [ApiController](https://learn.microsoft.com/en-us/aspnet/core/web-api/?view=aspnetcore-7.0#automatic-http-400-responses) is necessary because it allows to activate the [ModelStateInvalid](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.infrastructure.modelstateinvalidfilter?view=aspnetcore-7.0) filter to perform the model validation before executing the controller action.

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
| Result.ObtainedResources| 200 - Ok                    |
| Result.File             | 200 - Ok                    |
| Result.Invalid          | 400 - Bad Request           |
| Result.NotFound         | 404 - Not Found             |
| Result.Unauthorized     | 401 - Unauthorized          |
| Result.Conflict         | 409 - Conflict              |
| Result.Failure          | 422 - Unprocessable Entity  |
| Result.CriticalError    | 500 - Internal Server Error |
| Result.Forbidden        | 403 - Forbidden             |

### Integration with Fluent Validation

You need to install the [SimpleResults.FluentValidation](https://www.nuget.org/packages/SimpleResults.FluentValidation) package to have access to the extension methods.

**Example:**
```cs
public class UserService
{
    public Result Create(CreateUserRequest request)
    {
        ValidationResult result = new CreateUserValidator().Validate(request);
        if(result.IsFailed())
            return result.Invalid();

        // Some code..
    }
}
```
See the [API documentation](https://mrdave1999.github.io/SimpleResults/api/SimpleResults.SRFluentValidationResultExtensions.html#methods) for a list of available extension methods.

## Samples

You can find a complete and functional example in these projects:
- [SimpleResults.Example](https://github.com/MrDave1999/SimpleResults/tree/master/samples/SimpleResults.Example)
- [SimpleResults.Example.NativeAOT](https://github.com/MrDave1999/SimpleResults/tree/master/samples/SimpleResults.Example.NativeAOT)
- [SimpleResults.Example.AspNetCore](https://github.com/MrDave1999/SimpleResults/tree/master/samples/SimpleResults.Example.AspNetCore)
- [SimpleResults.Example.Web.Tests](https://github.com/MrDave1999/SimpleResults/tree/master/samples/SimpleResults.Example.Web.Tests)
- [SimpleResults.Example.FluentValidation](https://github.com/MrDave1999/SimpleResults/tree/master/samples/SimpleResults.Example/FluentValidation)
- [Double-V-Partners](https://github.com/MrDave1999/double-v-partners/tree/master/backend/src/Features/Users)
- [Orders.Sample](https://github.com/MrDave1999/orders-sample/tree/master/src/Application/Features/Orders)

## Language settings

`SimpleResults` has resources that contain response messages. [See the source code](https://github.com/MrDave1999/SimpleResults/tree/23f5fdb4af10195b182ace9ff78fb4fbf4fa9768/src/Core/Resources).

At the moment there are only two resources:
- `ResponseMessages.resx`. It contains messages in English.
- `ResponseMessages.es.resx`. It contains messages in Spanish.

The loading of these resources depends on your locale settings. 
For example, if your computer has the language as Spanish, the resource that will be loaded will be `ResponseMessages.es.resx`.
Likewise, if it is set to English, the default resource will be loaded: `ResponseMessages.resx`.

And if the configuration is set to French, the resource that will be loaded will be the default one (i.e. `ResponseMessages.resx`), since there is no resource called `ResponseMessages.fr.resx`.

You can explicitly specify the culture to ensure that a resource is loaded regardless of your computer's language settings:
```cs
Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("es");
```
In ASP.NET Core applications, the [UseRequestLocalization](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.applicationbuilderextensions.userequestlocalization?view=aspnetcore-7.0#microsoft-aspnetcore-builder-applicationbuilderextensions-userequestlocalization(microsoft-aspnetcore-builder-iapplicationbuilder-system-string())) extension method is used:
```cs
app.UseRequestLocalization("es");
```

## Contribution

Any contribution is welcome! Remember that you can contribute not only in the code, but also in the documentation or even improve the tests.

Follow the steps below:

- Fork it
- Create your feature branch (git checkout -b my-new-feature)
- Commit your changes (git commit -am 'Added some feature')
- Push to the branch (git push origin my-new-feature)
- Create new Pull Request

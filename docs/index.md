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

## Operation Result Pattern

The purpose of the Result design pattern is to give an operation (a method) the possibility to return a complex result (an object), allowing the consumer to:
- Access the result of an operation; in case there is one.
- Access the success indicator of an operation.
- Access the failure indicator of an operation.
- Access the value (data) of the result if it exists.
- Access the cause of the failure in case the operation was not successful.
- Access an error or success message.
- Access to a collection of error messages.

## Contribution

Any contribution is welcome! Remember that you can contribute not only in the code, but also in the documentation or even improve the tests.

Follow the steps below:

- Fork it
- Create your feature branch (git checkout -b my-new-feature)
- Commit your changes (git commit -am 'Added some feature')
- Push to the branch (git push origin my-new-feature)
- Create new Pull Request

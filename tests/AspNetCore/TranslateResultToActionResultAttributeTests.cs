namespace SimpleResults.Tests.AspNetCore;

public class TranslateResultToActionResultAttributeTests
{
    private static ActionExecutedContext CreateContext()
    {
        var httpContext = new DefaultHttpContext();
        var actionContext = new ActionContext(httpContext, new(), new(), new());
        var filters = new List<IFilterMetadata>();
        return new ActionExecutedContext(actionContext, filters, controller: null);
    }

    [Test]
    public void OnActionExecuted_WhenValueIsNotSubtypeOfResultBase_ShouldNotRunAnyLogic()
    {
        // Arrange
        var actionExecutedContext = CreateContext();
        var actionFilter = new TranslateResultToActionResultAttribute();
        var value = new Person { Name = "Test" };
        actionExecutedContext.Result = new ObjectResult(value);

        // Act
        actionFilter.OnActionExecuted(actionExecutedContext);

        // Assert
        actionExecutedContext.Result.Should().As<ObjectResult>();
    }

    [Test]
    public void OnActionExecuted_WhenValueIsSubtypeOfResultBase_ShouldTranslateToActionResult()
    {
        // Arrange
        var actionExecutedContext = CreateContext();
        var actionFilter = new TranslateResultToActionResultAttribute();
        int expectedStatusCode = 200;
        var person = new Person { Name = "Test" };
        Result<Person> value = Result.Success(person);
        actionExecutedContext.Result = new ObjectResult(value);

        // Act
        actionFilter.OnActionExecuted(actionExecutedContext);

        // Asserts
        var objectResult = actionExecutedContext.Result as OkObjectResult;
        objectResult.StatusCode.Should().Be(expectedStatusCode);
        objectResult.Value.Should().As<Result<Person>>();
    }

    [Test]
    public void OnActionExecuted_WhenResultIsNotSubtypeOfObjectResult_ShouldNotRunAnyLogic()
    {
        // Arrange
        var actionExecutedContext = CreateContext();
        var actionFilter = new TranslateResultToActionResultAttribute();
        var value = new Person { Name = "Test" };
        actionExecutedContext.Result = new HttpActionResult(value);

        // Act
        actionFilter.OnActionExecuted(actionExecutedContext);

        // Assert
        actionExecutedContext.Result.Should().As<HttpActionResult>();
    }

    private class HttpActionResult : IActionResult
    {
        public object Value { get; }
        public int StatusCode => StatusCodes.Status200OK;
        public HttpActionResult(object value) => Value = value;

        public Task ExecuteResultAsync(ActionContext context)
        {
            context.HttpContext.Response.StatusCode = StatusCode;
            return context.HttpContext.Response.WriteAsJsonAsync(Value);
        }
    }
}

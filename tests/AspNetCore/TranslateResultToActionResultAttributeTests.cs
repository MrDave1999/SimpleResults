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
    public void OnActionExecuted_WhenObjectResultValueIsNotResultBase_ShouldNotRunAnyLogic()
    {
        // Arrange
        var actionExecutedContext = CreateContext();
        var actionFilter = new TranslateResultToActionResultAttribute();
        var person = new Person { Name = "Test" };
        actionExecutedContext.Result = new ObjectResult(person);

        // Act
        actionFilter.OnActionExecuted(actionExecutedContext);

        // Asserts
        var objectResult = actionExecutedContext.Result as ObjectResult;
        objectResult.StatusCode.Should().BeNull();
        objectResult.Value.Should().As<Person>();
    }

    [Test]
    public void OnActionExecuted_WhenObjectResultValueIsResultBase_ShouldTranslateToActionResult()
    {
        // Arrange
        var actionExecutedContext = CreateContext();
        var actionFilter = new TranslateResultToActionResultAttribute();
        var person = new Person { Name = "Test" };
        Result<Person> result = Result.Success(person);
        actionExecutedContext.Result = new ObjectResult(result);

        // Act
        actionFilter.OnActionExecuted(actionExecutedContext);

        // Asserts
        var objectResult = actionExecutedContext.Result as OkObjectResult;
        objectResult.StatusCode.Should().Be(200);
        objectResult.Value.Should().As<Result<Person>>();
    }

    [Test]
    public void OnActionExecuted_WhenContextResultIsNotObjectResult_ShouldNotRunAnyLogic()
    {
        // Arrange
        var actionExecutedContext = CreateContext();
        var actionFilter = new TranslateResultToActionResultAttribute();
        actionExecutedContext.Result = new TestResult();

        // Act
        actionFilter.OnActionExecuted(actionExecutedContext);

        // Assert
        actionExecutedContext.Result.Should().As<TestResult>();
    }

    private class TestResult : IActionResult
    {
        public Task ExecuteResultAsync(ActionContext context) => throw new NotImplementedException();
    }
}

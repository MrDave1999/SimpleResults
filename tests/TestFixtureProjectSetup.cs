namespace SimpleResults.Tests;

[SetUpFixture]
public class TestFixtureProjectSetup
{
    [OneTimeSetUp]
    public void RunBeforeAllTestFixtures()
    {
        Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");
    }
}

namespace SimpleResults.Tests;

[SetUpFixture]
public class TestFixtureProjectSetup
{
    [OneTimeSetUp]
    public void RunBeforeAllTestFixtures()
    {
        // Allows to load the default resource in English.
        Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en");
    }
}

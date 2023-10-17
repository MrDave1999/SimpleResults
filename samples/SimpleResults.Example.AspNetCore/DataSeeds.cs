namespace SimpleResults.Example.AspNetCore;

public class DataSeeds
{
    public static List<User> CreateUsers()
    {
        var testUsers = new Faker<User>()
            .RuleFor(u => u.Id, Guid.NewGuid().ToString())
            .RuleFor(u => u.Name, f => f.Name.FirstName());

        return testUsers.Generate(10);
    }
}

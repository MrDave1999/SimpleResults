Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en");
var users = new List<User>
{
    new() { Id = "1", Name = "Bob" },
    new() { Id = "2", Name = "Alice" }
};
var userService = new UserService(users);
ListedResult<User> result = userService.GetAll();

var json = JsonSerializer.Serialize(
    result, 
    CustomContext.Default.ListedResultUser);

Console.WriteLine(json);

var deserializedResult = JsonSerializer.Deserialize(
    json,
    CustomContext.Default.ListedResultUser);

Console.WriteLine();
Console.WriteLine("Status: " + deserializedResult.Status);
foreach(var user in deserializedResult.Data)
{
    Console.WriteLine($"Id: {user.Id}, Name: {user.Name}");
}

Console.ReadLine();

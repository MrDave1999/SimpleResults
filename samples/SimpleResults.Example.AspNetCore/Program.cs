var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddSingleton(DataSeeds.CreateUsers())
    .AddSingleton<List<Person>>()
    .AddSingleton<UserService>()
    .AddSingleton<PersonService>();

builder.Services.AddControllers(options =>
{
    // Add filter for all controllers.
    options.Filters.Add<TranslateResultToActionResultAttribute>();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Allows to load the default resource in English.
app.UseRequestLocalization("en-US");

app.UseAuthorization();

app.MapControllers();

app.AddUserRoutes();

app.AddPersonRoutes();

app.Run();

// This class used in the integration test project.
public partial class Program { }
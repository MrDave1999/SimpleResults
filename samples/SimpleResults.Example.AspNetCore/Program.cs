var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton(DataSeeds.CreateUsers());
builder.Services.AddSingleton<UserService>();

builder.Services.AddControllers();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

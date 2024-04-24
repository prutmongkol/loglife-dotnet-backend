using ActivitiesApi.Models;
using ActivitiesApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = Environment.GetEnvironmentVariable("MONGODB_URI");
if (connectionString == null)
{
    Console.WriteLine("You must set your 'MONGODB_URI' environment variable.");
    Environment.Exit(0);
}
var databaseName = Environment.GetEnvironmentVariable("MONGODB_DB");
if (databaseName == null)
{
    Console.WriteLine("You must set your 'MONGODB_DB' environment variable.");
    Environment.Exit(0);
}
var activitiesCollectionName = Environment.GetEnvironmentVariable("MONGODB_ACTIVITIES_COLLECTION");
if (activitiesCollectionName == null)
{
    Console.WriteLine("You must set your 'MONGODB_ACTIVITIES_COLLECTION' environment variable.");
    Environment.Exit(0);
}

builder.Services.Configure<ActivitiesDatabaseSettings>(i =>
{
    i.ConnectionString = connectionString;
    i.DatabaseName = databaseName;
    i.ActivitiesCollectionName = activitiesCollectionName;
});

builder.Services.AddSingleton<ActivitiesService>();

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

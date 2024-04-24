using ActivitiesApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = Environment.GetEnvironmentVariable("MONGODB_URI");
if (connectionString == null)
{
    Console.WriteLine("You must set your 'MONGODB_URI' environment variable. To learn how to set it, see https://www.mongodb.com/docs/drivers/csharp/current/quick-start/#set-your-connection-string");
    Environment.Exit(0);
}
var config = builder.Configuration.GetSection("ActivityDatabaseSettings").Get<ActivityDatabaseSettings>();
config!.ConnectionString = connectionString;
builder.Services.Configure<ActivityDatabaseSettings>(i =>
{
    i.ConnectionString = config.ConnectionString;
    i.DatabaseName = config.DatabaseName;
    i.ActivitiesCollectionName = config.ActivitiesCollectionName;
});

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

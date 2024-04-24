using ActivitiesApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ActivitiesApi.Services;

public class ActivitiesService
{
    private readonly IMongoCollection<Activity> _activitiesCollection;

    public ActivitiesService(
        IOptions<ActivityDatabaseSettings> activityDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            activityDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            activityDatabaseSettings.Value.DatabaseName);

        _activitiesCollection = mongoDatabase.GetCollection<Activity>(
            activityDatabaseSettings.Value.ActivitiesCollectionName);
    }

    public async Task<List<Activity>> GetActivitiesAsync()
    {
        // filter mock userId
        var filter = Builders<Activity>.Filter
            .Eq(a => a.UserId, "660e6b21af25ac37c491da23");

        return await _activitiesCollection.Find(filter).ToListAsync();
    }

    public async Task<Activity?> GetActivityAsync(string id) =>
        await _activitiesCollection.Find(a => a.Id == id).FirstOrDefaultAsync();

    public async Task CreateActivityAsync(Activity newActivity) =>
        await _activitiesCollection.InsertOneAsync(newActivity);
    
    public async Task UpdateActivityAsync(string id, Activity updatedActivity) =>
        await _activitiesCollection.ReplaceOneAsync(a => a.Id == id, updatedActivity);

    public async Task DeleteActivityAsync(string id) =>
        await _activitiesCollection.DeleteOneAsync(a => a.Id == id);
}
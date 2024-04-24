namespace ActivitiesApi.Models;

public class ActivityDatabaseSettings
{
    public string ConnectionString {get; set;} = null!;
    public string DatabaseName {get; set;} = null!;
    public string ActivitiesCollectionName {get; set;} = null!;
}
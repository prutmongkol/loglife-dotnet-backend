using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ActivityApi.Models;

public class Activity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("userId")]
    public string UserId { get; set; } = null!;

    [BsonElement("title")]
    public string Title { get; set; } = null!;

    [BsonElement("description")]
    public string Description { get; set; } = null!;

    [BsonElement("type")]
    public string Type { get; set; } = null!;

    [BsonElement("startTime")]
    public string StartTime { get; set; } = null!;

    [BsonElement("endTime")]
    public string EndTime { get; set; } = null!;

    [BsonElement("date")]
    public string Date { get; set; } = null!;

    [BsonElement("duration")]
    public DurationClass Duration { get; set; } = null!;

    public class DurationClass
    {
        [BsonElement("hour")]
        public int Hour { get; set; }

        [BsonElement("minute")]
        public int Minute { get; set; }
    }

    [BsonElement("barometer")]
    public string Barometer { get; set; } = null!;

    [BsonElement("image")]
    public ImageClass? Image { get; set; }

    public class ImageClass
    {
        [BsonElement("url")]
        public string Url { get; set; } = null!;

        [BsonElement("publicId")]
        public string PublicId { get; set; } = null!;
    }
}
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PWPA.CallLogging.BackEnd.ApplicationCore.Models;

public class Call
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonRequired]
    public string? CallerName { get; set; }

    [BsonRequired]
    public string? Address { get; set; }

    [BsonRequired]
    public string? Description { get; set; }
}
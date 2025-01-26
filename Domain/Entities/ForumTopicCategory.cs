using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    public class ForumTopicCategory : BaseEntity
    {
        [BsonElement] public required string CategoryName { get; set; }
    }
}

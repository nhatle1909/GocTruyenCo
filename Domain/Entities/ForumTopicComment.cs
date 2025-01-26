using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    public class ForumTopicComment : BaseEntity
    {
        [BsonElement] public required Guid ForumTopicId { get; set; }
        [BsonElement] public required Guid AccountId { get; set; }
        [BsonElement] public required string Comment { get; set; }
    }
}

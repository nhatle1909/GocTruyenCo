using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    public class ForumTopic : BaseEntity
    {
        [BsonElement] public required Guid AccountId { get; set; }
        [BsonElement] public required Guid ForumCategoryId { get; set; }
        [BsonElement] public required string Title { get; set; }
    }
}

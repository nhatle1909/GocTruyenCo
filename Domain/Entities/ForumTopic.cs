using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    public class ForumTopic : BaseEntity
    {
        [BsonRepresentation(BsonType.String)] public required Guid CreatorId { get; set; }
        [BsonRepresentation(BsonType.String)] public required Guid ForumCategoryId { get; set; }
         public required bool isLock { get; set; }
         public required string Title { get; set; }
    }
}

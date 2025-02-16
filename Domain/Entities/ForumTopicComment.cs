using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    public class ForumTopicComment : BaseEntity
    {
        [BsonRepresentation(BsonType.String)] public required Guid ForumTopicId { get; set; }
        [BsonRepresentation(BsonType.String)] public required Guid AccountId { get; set; }
         public required string Comment { get; set; }

        [BsonIgnoreIfNull] public string AccountName { get; set; }
    }
}

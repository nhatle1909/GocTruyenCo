using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    public class Bookmark : BaseEntity
    {
        [BsonElement][BsonRepresentation(BsonType.String)] public required Guid AccountId { get; set; }
        [BsonElement] public required Guid ComicId { get; set; }

        [BsonIgnoreIfNull] public string ComicName { get; set; }
    }
}

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    public class ComicChapter : BaseEntity
    {
        [BsonElement][BsonRepresentation(BsonType.String)] public required Guid ComicId { get; set; }
        [BsonElement] public required string Name { get; set; }

    }
}

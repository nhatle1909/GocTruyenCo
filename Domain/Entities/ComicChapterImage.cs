using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    public class ComicChapterImage : BaseEntity
    {
        [BsonElement][BsonRepresentation(BsonType.String)] public required Guid ComicChapterId { get; set; }
        [BsonElement] public required string ImageURL { get; set; }
    }
}

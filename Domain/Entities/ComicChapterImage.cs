using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    public class ComicChapterImage : BaseEntity
    {
        [BsonElement] public required Guid ComicChapterId { get; set; }
        [BsonElement] public required string ImageURL { get; set; }
    }
}

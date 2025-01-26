using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    public class ComicChapterComment : BaseEntity
    {
        [BsonElement] public required Guid AccountId { get; set; }
        [BsonElement] public required Guid ComicChapterId { get; set; }
        [BsonElement] public required string Comment { get; set; }
    }
}

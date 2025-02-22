using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    public class ComicChapterImage : BaseEntity
    {
        [BsonRepresentation(BsonType.String)] public required Guid ComicChapterId { get; set; }
        public required string ImageURL { get; set; }
    }
}

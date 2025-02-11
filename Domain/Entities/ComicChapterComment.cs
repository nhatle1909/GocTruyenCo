using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    public class ComicChapterComment : BaseEntity
    {
        [BsonElement][BsonRepresentation(BsonType.String)] public required Guid AccountId { get; set; }
        [BsonElement][BsonRepresentation(BsonType.String)] public required Guid ComicChapterId { get; set; }
        [BsonElement] public required string Comment { get; set; }

        // This is a reference to the Account collection
        [BsonIgnoreIfNull] public string AccountName { get; set; }
        [BsonIgnoreIfNull] public string ChapterName { get; set; }
    }
}

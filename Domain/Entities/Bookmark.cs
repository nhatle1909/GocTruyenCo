using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using static Domain.Enums.BookmarkEnum;
namespace Domain.Entities
{
    public class Bookmark : BaseEntity
    {
        [BsonRepresentation(BsonType.String)] public required Guid AccountId { get; set; }
        [BsonRepresentation(BsonType.String)] public required Guid ComicId { get; set; }
        [BsonRepresentation(BsonType.String)] public required BookmarkType BookmarkType { get; set; }

        [BsonIgnoreIfNull] public string Name { get; set; }
        [BsonIgnoreIfNull] public int Chapters { get; set; }
        [BsonIgnoreIfNull] public string Description { get; set; }
        [BsonIgnoreIfNull] public string ThemeURL { get; set; }
        [BsonIgnoreIfNull] public string Status { get; set; }
    }
}

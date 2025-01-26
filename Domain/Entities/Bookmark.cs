using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    public class Bookmark : BaseEntity
    {
        [BsonElement] public required Guid AccountId { get; set; }
        [BsonElement] public required Guid ComicId { get; set; }
    }
}

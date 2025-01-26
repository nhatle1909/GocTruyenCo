using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    internal class ComicChapter : BaseEntity
    {
        [BsonElement] public required Guid ComicId { get; set; }
        [BsonElement] public required string Name { get; set; }

    }
}

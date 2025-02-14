using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    public class ComicChapter : BaseEntity
    {
        [BsonRepresentation(BsonType.String)] public required Guid ComicId { get; set; }
         public required string Name { get; set; }

    }
}

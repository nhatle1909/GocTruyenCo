using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    public class ComicCategory : BaseEntity
    {
        [BsonElement] public required string Name { get; set; }
    }
}

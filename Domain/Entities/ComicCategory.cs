using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    public class ComicCategory : BaseEntity
    {
         public required string Name { get; set; }
         public required string Description { get; set; }
    }
}

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    public class ForumTopicCategory : BaseEntity
    {
         public required string CategoryName { get; set; }
    }
}

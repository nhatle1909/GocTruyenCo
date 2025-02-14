using Domain.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    public class Ticket : BaseEntity
    {
         public required string Title { get; set; }
         public required string Description { get; set; }
        [BsonRepresentation(BsonType.String)] public TicketStatus Status { get; set; }
        [BsonRepresentation(BsonType.String)] public TicketType Type { get; set; }
    }
}

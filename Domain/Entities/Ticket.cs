using Domain.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Ticket : BaseEntity
    {
        [BsonElement] public required string Title { get; set; }
        [BsonElement] public required string Description { get; set; }
        [BsonElement][BsonRepresentation(BsonType.String)] public TicketStatus Status { get; set; }
        [BsonElement][BsonRepresentation(BsonType.String)] public TicketType Type { get; set; }
    }
}

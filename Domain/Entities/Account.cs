using Domain.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Account : BaseEntity
    {

        [BsonElement][EmailAddress] public required string Email { get; set; }
        [BsonElement] public required string Password { get; set; }
        [BsonElement] public required string Username { get; set; }
        [BsonElement] public required bool isRestricted { get; set; }
        [BsonElement][BsonRepresentation(BsonType.String)] public required Role Role { get; set; }
    }
}

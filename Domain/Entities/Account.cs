using Domain.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Account : BaseEntity
    {

        [EmailAddress] public required string Email { get; set; }
         public required string Password { get; set; }
         public required string Username { get; set; }
         public required bool isRestricted { get; set; }
        [BsonRepresentation(BsonType.String)] public required Role Role { get; set; }
    }
}

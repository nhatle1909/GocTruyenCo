using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Globalization;

namespace Domain.Entities
{
    public class BaseEntity
    {
        [BsonId][BsonRepresentation(BsonType.String)] public required Guid Id { get; set; } = Guid.NewGuid();
        public required bool isDeleted { get; set; }

        public required string CreatedDate { get; set; } = DateTime.Now.ToString("d", new CultureInfo("vi-VN"));
    }
}

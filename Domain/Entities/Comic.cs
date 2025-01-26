using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using static Domain.Enums.ComicStatusEnum;

namespace Domain.Entities
{
    public class Comic : BaseEntity
    {
        [BsonElement] public required Guid UploaderId { get; set; }
        [BsonElement] public required List<Guid> CategoryId { get; set; }
        [BsonElement] public required string Name { get; set; }
        [BsonElement] public required string ThemeURL { get; set; }
        [BsonElement] public required int Chapters { get; set; }
        [BsonElement] public required ComicStatus Status { get; set; }
    }
}

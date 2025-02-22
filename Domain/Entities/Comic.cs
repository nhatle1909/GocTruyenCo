using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using static Domain.Enums.ComicStatusEnum;

namespace Domain.Entities
{

    public class Comic : BaseEntity
    {
        [BsonRepresentation(BsonType.String)] public required Guid UploaderId { get; set; }
        [BsonRepresentation(BsonType.String)] public required List<Guid> CategoryId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string ThemeURL { get; set; }
        public required int Chapters { get; set; }
        [BsonRepresentation(BsonType.String)] public required ComicStatus Status { get; set; }


        [BsonIgnoreIfNull] public List<string> CategoryName { get; set; }
        [BsonIgnoreIfNull] public string UploaderName { get; set; }

    }
}

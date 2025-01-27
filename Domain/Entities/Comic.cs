﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using static Domain.Enums.ComicStatusEnum;

namespace Domain.Entities
{
    public class Comic : BaseEntity
    {
        [BsonElement][BsonRepresentation(BsonType.String)] public required Guid UploaderId { get; set; }
        [BsonElement][BsonRepresentation(BsonType.String)] public required List<Guid> CategoryId { get; set; }
        [BsonElement] public required string Name { get; set; }
        [BsonElement] public required string Description { get; set; }
        [BsonElement] public required string ThemeURL { get; set; }
        [BsonElement] public required int Chapters { get; set; }
        [BsonElement] public required ComicStatus Status { get; set; }
        
        //Only Deserializing field
        [BsonElement][BsonIgnore] public required List<string> Category { get; set; }
    }
}

﻿namespace Application.DTO
{
    public class QueryComicDTO
    {
        public required Guid Id { get; set; }
        public required string UploaderName { get; set; }
        public required List<string> CategoryName { get; set; }
        public required string CreatedDate { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string ThemeURL { get; set; }
        public required int Chapters { get; set; }
        public required string Status { get; set; }
    }
    public class CommandComicDTO
    {
        public required Guid UploaderId { get; set; }
        public required List<Guid> CategoryId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string ThemeURL { get; set; }
        public required int Chapters { get; set; }
    }
}

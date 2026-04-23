using MongoDB.Bson;

namespace Application.Aggregation
{
    public class ComicAggregation
    {
        public static BsonDocument[] ComicBsonAggregation
        {
            get
            {
                BsonDocument lookupPipeline1 = new BsonDocument("$lookup",
                   new BsonDocument
                       {
                            { "from", "Account" },
                            { "localField", "UploaderId" },
                            { "foreignField", "_id" },
                            { "as", "UploaderName" }
                       });
                BsonDocument projectPipeline = new BsonDocument("$project",
                    new BsonDocument
                        {
                            { "_id", "$_id" },
                            { "isDeleted", "$isDeleted" },
                            { "CreatedDate", "$CreatedDate" },
                            { "UploaderName", "$UploaderName.Username" },
                            { "CategoryName", "$CategoryName" },
                            { "Name", "$Name" },
                            { "Description", "$Description" },
                            { "ThemeURL", "$ThemeURL" },
                            { "Chapters", "$Chapters" },
                            {  "Status","$Status" }
                        });
                BsonDocument unwindPipeline = new BsonDocument("$unwind",
                new BsonDocument("path", "$UploaderName"));
                BsonDocument[] aggregateStages = new BsonDocument[] {  lookupPipeline1, projectPipeline, unwindPipeline };
                return aggregateStages;
            }
        }
    }
}

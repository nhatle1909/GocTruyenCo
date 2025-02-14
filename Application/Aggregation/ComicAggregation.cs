using MongoDB.Bson;

namespace Application.Aggregation
{
    public class ComicAggregation
    {
        public static BsonDocument[] ComicBsonAggregation
        {
            get
            {
                BsonDocument lookupPipeline = new BsonDocument("$lookup",
                    new BsonDocument
                        {
                            { "from", "ComicCategory" },
                            { "localField", "CategoryId" },
                            { "foreignField", "_id" },
                            { "as", "CategoryId" }
                        });
                BsonDocument lookupPipeline2 = new BsonDocument("$lookup",
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
                            { "CategoryName", "$CategoryId.Name" },
                            { "Name", "$Name" },
                            { "Description", "$Description" },
                            { "ThemeURL", "$ThemeURL" },
                            { "Chapters", "$Chapters" },
                            {  "Status","$Status" }
                        });
                BsonDocument unwindPipeline = new BsonDocument("$unwind",
                new BsonDocument("path", "$UploaderName"));
                BsonDocument[] aggregateStages = new BsonDocument[] { lookupPipeline, lookupPipeline2, projectPipeline, unwindPipeline };
                return aggregateStages;
            }
        }
    }
}

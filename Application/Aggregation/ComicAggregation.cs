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
                BsonDocument projectPipeline = new BsonDocument("$project",
                    new BsonDocument
                        {
                            { "_id", "$_id" },
                            { "isDeleted", "$isDeleted" },
                            { "CreatedDate", "$CreatedDate" },
                            { "UploaderId", "$UploaderId" },
                            { "Category", "$CategoryId.Name" },
                            { "Name", "$Name" },
                            { "Description", "$Description" },
                            { "ThemeURL", "$ThemeURL" },
                            { "Chapters", "$Chapters" },
                            {  "Status","$Status" }
                        });
                BsonDocument[] aggregateStages = new BsonDocument[] { lookupPipeline, projectPipeline };
                return aggregateStages;
            }
        }
    }
}

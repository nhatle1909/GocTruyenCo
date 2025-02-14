using MongoDB.Bson;

namespace Application.Aggregation
{
    public class BookmarkAggregation
    {
        public static BsonDocument[] BookmarkBsonAggregation
        {
            get
            {
                BsonDocument lookupPipeline = new BsonDocument("$lookup",
                                                new BsonDocument
                                                    {
                                                        { "from", "Comic" },
                                                        { "localField", "ComicId" },
                                                        { "foreignField", "_id" },
                                                        { "as", "Comic" }
                                                    });
                BsonDocument unwindpipeline = new BsonDocument("$unwind",
                                                new BsonDocument("path", "$Comic"));
                BsonDocument projectPipeline = new BsonDocument("$project",
                                                new BsonDocument
                                                    {
                                                        { "_id", "$_id" },
                                                        { "CreatedDate", "$CreatedDate" },
                                                        { "BookmarkType", "$BookmarkType" },
                                                        { "Name", "$Comic.Name" },
                                                        { "Chapters", "$Comic.Chapters" },
                                                        { "Status", "$Comic.Status" },
                                                        { "Description", "$Comic.Description" },
                                                        { "ThemeURL", "$Comic.ThemeURL" }
                                                    });
                return new BsonDocument[] { lookupPipeline, unwindpipeline, projectPipeline };
            }
        }
    }
}

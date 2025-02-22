using MongoDB.Bson;

namespace Application.Aggregation
{
    public class ForumTopicAggregation
    {
        public static BsonDocument[] ForumTopicBsonAggregation
        {
            get
            {
                BsonDocument lookupPipeline1 = new BsonDocument("$lookup",
                                                    new BsonDocument
                                                        {
                                                            { "from", "ForumTopicCategory" },
                                                            { "localField", "ForumCategoryId" },
                                                            { "foreignField", "_id" },
                                                            { "as", "ForumCategory" }
                                                        });
                BsonDocument lookupPipeline2 = new BsonDocument("$lookup",
                                                    new BsonDocument
                                                        {
                                                            { "from", "Account" },
                                                            { "localField", "CreatorId" },
                                                            { "foreignField", "_id" },
                                                            { "as", "result" }
                                                        });
                BsonDocument projectPipeline = new BsonDocument("$project",
                                                    new BsonDocument
                                                        {
                                                            { "_id", "$_id" },
                                                            { "CreatorName", "$result.Username" },
                                                            { "CreatedDate", "$CreatedDate" },
                                                            { "Title", "$Title" },
                                                            { "TopicCategory", "$ForumCategory.CategoryName" },
                                                            { "isLock", "$isLock" },
                                                            { "isDeleted", "$isDeleted" }
                                                        });
                BsonDocument unwindPipeline1 = new BsonDocument("$unwind",
                                                    new BsonDocument("path", "$CreatorName"));
                BsonDocument unwindPipeline2 = new BsonDocument("$unwind",
                                                    new BsonDocument("path", "$TopicCategory"));
                return new BsonDocument[] { lookupPipeline1, lookupPipeline2, projectPipeline, unwindPipeline1, unwindPipeline2 };
            }
        }
    }
}

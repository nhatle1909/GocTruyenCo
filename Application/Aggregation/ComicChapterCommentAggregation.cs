using MongoDB.Bson;

namespace Application.Aggregation
{
    public class ComicChapterCommentAggregation
    {
        public static BsonDocument[] ComicChapterCommentBsonAggregation
        {
            get
            {
                BsonDocument lookupPipeline = new BsonDocument("$lookup",
                    new BsonDocument
                        {
                           { "from", "Account" },
                           { "localField", "AccountId" },
                           { "foreignField", "_id" },
                           { "as", "result" }
                        });
                //BsonDocument lookupPipeline2 = new BsonDocument("$lookup",
                //    new BsonDocument
                //        {
                //            { "from", "ComicChapter" },
                //            { "localField", "ComicChapterId" },
                //            { "foreignField", "_id" },
                //            { "as", "ComicChapterName" }
                //        });
                BsonDocument projectPipeline = new BsonDocument("$project",
                    new BsonDocument
                        {
                            { "_id", "$_id" },
                            { "isDeleted", "$isDeleted" },
                            { "CreatedDate", "$CreatedDate" },
                            //{ "ChapterName", "$ComicChapterName.Name" },
                            { "AccountName", "$result.Username" },
                            { "Comment", "$Comment" }
                        });
                BsonDocument unwindPipeline = new BsonDocument("$unwind",
                 new BsonDocument("path", "$AccountName"));
                //BsonDocument unwindPipeline2 = new BsonDocument("$unwind",
                //new BsonDocument("path", "$ChapterName"));
                BsonDocument[] aggregateStages = new BsonDocument[] { lookupPipeline, projectPipeline, unwindPipeline };
                return aggregateStages;
            }
        }
    }
}

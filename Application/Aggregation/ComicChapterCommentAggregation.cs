using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                BsonDocument lookupPipeline2 = new BsonDocument("$lookup",
                    new BsonDocument
                        {
                            { "from", "ComicChapter" },
                            { "localField", "ComicChapterId" },
                            { "foreignField", "_id" },
                            { "as", "ComicChapterName" }
                        });
                BsonDocument projectPipeline = new BsonDocument("$project",
                    new BsonDocument
                        {
                            { "_id", "$_id" },
                            { "isDeleted", "$isDeleted" },
                            { "CreatedDate", "$CreatedDate" },
                            { "ChapterName", "$ComicChapterName.Name" },
                            { "AccountName", "$result.Username" },
                            { "Comment", "$Comment" }
                        });
                BsonDocument[] aggregateStages = new BsonDocument[] { lookupPipeline, lookupPipeline2, projectPipeline };
                return aggregateStages;
            }
        }
    }
}

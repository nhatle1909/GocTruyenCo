using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Aggregation
{
    public class ForumTopicCommentAggregation
    {
        public static BsonDocument[] ForumTopicCommentBsonAggregation
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
                BsonDocument projectPipeline = new BsonDocument("$project",
                    new BsonDocument
                        {
                            { "_id", "$_id" },
                            { "isDeleted", "$isDeleted" },
                            { "CreatedDate", "$CreatedDate" },
                            { "AccountName", "$result.Username" },
                            { "Comment", "$Comment" }
                        });
                BsonDocument unwindPipeline = new BsonDocument("$unwind",
              new BsonDocument("path", "$AccountName"));
                return new BsonDocument[] { lookupPipeline,projectPipeline,unwindPipeline };
            }
        }
    }
}

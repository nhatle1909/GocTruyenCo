using Application.Interface;
using Infrastructure.Configuration;
using Microsoft.Extensions.Caching.Memory;
using MongoDB.Bson;
using MongoDB.Driver;
namespace Infrastructure
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly IMongoCollection<T> _collection;
        private List<string> keyValuePairs = new();
        private readonly IMemoryCache _memoryCache;

        private readonly string cacheKey;

        public GenericRepository(IMongoDatabase database, string collectionName, IMemoryCache memoryCache)
        {
            _collection = database.GetCollection<T>(collectionName);
            _memoryCache = memoryCache;
            cacheKey = $"{collectionName}";
        }

        public async Task<bool> AddOneItemAsync(T item)
        {
            if (item == null) return false;

            try
            {
                await _collection.InsertOneAsync(item);
                RemoveCache(null);
                return true;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        } //Finish

        public async Task<bool> AddManyItemAsync(List<T> items)
        {
            if (items.Count == 0) return false;

            try
            {
                await _collection.InsertManyAsync(items);
                RemoveCache(null);
                return true;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        } //Finish

        public async Task<T> GetByIdAsync(Guid id, BsonDocument[] aggregates = null)
        {

            string idString = id.ToString();
            T result;

            var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5));

            if (_memoryCache.TryGetValue(cacheKey + idString, out result))
            {
                return result;
            }

            var filter = Builders<T>.Filter.Eq("_id", idString);

            var item = _collection.Aggregate()
                .Match(filter);
            if (aggregates != null)
            {
                foreach (var Stage in aggregates)
                {
                    item = item.AppendStage<T>(Stage);
                }

            }
            result = await item.FirstOrDefaultAsync();

            _memoryCache.Set(cacheKey + id, result, cacheEntryOptions);

            return result;


        }   //Finish
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            List<T> itemList;
            var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5));

            if (_memoryCache.TryGetValue(cacheKey, out itemList))
            {
                return itemList;
            }

            itemList = await _collection.Find(_ => true).ToListAsync();

            _memoryCache.Set(cacheKey, itemList, cacheEntryOptions);

            return itemList;
        } //Finish

        public async Task<IEnumerable<T>> PagingAsync(string[] searchFields, string[] searchValue, string sortField, bool isAsc, int pageSize, int skip, BsonDocument[] aggregates = null)
        {

            IAggregateFluent<T> query;

            //Create Filter
            FilterDefinition<T> filterDefinition = FilterDefinitionsBuilders(searchFields, searchValue);

            //Create Sort
            SortDefinition<T> sortDefinition = isAsc ? Builders<T>.Sort.Ascending(sortField) : Builders<T>.Sort.Descending(sortField);

            //Query
            query = _collection.Aggregate()
                               .Match(filterDefinition);
            if (aggregates != null)
            {
                foreach (var item in aggregates)
                {
                    query = query.AppendStage<T>(item);
                }
            }
            //Sort and paging
            query = query.Sort(sortDefinition)
                         .Skip(skip)
                         .Limit(pageSize);

            var result = await query.ToListAsync();

            return result;
        }   //Finish


        public async Task<bool> RemoveItemAsync(Guid id)
        {
            try
            {
                string IdString = id.ToString();
                var filter = Builders<T>.Filter.Eq("_id", IdString);
                await _collection.DeleteOneAsync(filter);
                RemoveCache(IdString);
                return true;
            }
            catch
            {
                return false;
            }
        } //Finish

        public async Task<bool> UpdateItemAsync(Guid id, T replacement)
        {
            try
            {
                string IdString = id.ToString();
                var filter = Builders<T>.Filter.Eq("_id", IdString);
                var updateDefinition = Builders<T>.Update.Set(T => T, replacement);
                await _collection.ReplaceOneAsync(filter, replacement);
                RemoveCache(IdString);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return false;
        } //Finish


        public async Task<IEnumerable<T>> GetAllByFilterAsync(string[] searchFields, string[] searchValue)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
            var postCacheKey = CacheKeyUtils.GenerateCacheKeyMinor(searchFields, searchValue);

            FilterDefinition<T> filterDefinition = Builders<T>.Filter.Empty;

            for (int i = 0; i < searchFields.Length; i++)
            {
                FilterDefinition<T> tempFilter = Builders<T>.Filter.Regex(searchFields[i], searchValue[i]);
                filterDefinition = Builders<T>.Filter.And(filterDefinition, tempFilter);
            }

            var query = await _collection.Find(filterDefinition).ToListAsync();

            _memoryCache.Set(cacheKey + postCacheKey, query);
            keyValuePairs.Add(cacheKey + postCacheKey);
            return query;
        } //Finish
        public async Task<long> CountAsync(string[] searchFields, string[] searchValue, int pageSize)
        {

            FilterDefinition<T> filterDefinition = FilterDefinitionsBuilders(searchFields, searchValue);
            //Query
            var count = await _collection.CountDocumentsAsync(filterDefinition);
            return (long)Math.Ceiling((double)count / pageSize);
        }

        private void RemoveCache(string key)
        {
            if (key != null) _memoryCache.Remove(key); // Remove Id key

            _memoryCache.Remove(cacheKey); // Remove collection key

            foreach (var item in keyValuePairs)
            {
                _memoryCache.Remove(item); // Remove paging key
            }
        }

        private FilterDefinition<T> FilterDefinitionsBuilders(string[] searchFields, string[] searchValue, bool[] isExclude = null)
        {

            //Create Filter
            FilterDefinition<T> filterDefinition = Builders<T>.Filter.Eq("isDeleted", false);

            for (int i = 0; i < searchFields.Length; i++)
            {
                //Default search if search value is null or empty
                FilterDefinition<T> tempFilter = Builders<T>.Filter.Empty;


                if (!string.IsNullOrEmpty(searchValue[i]))
                {

                    tempFilter = Builders<T>.Filter.Regex(searchFields[i], searchValue[i]);

                    bool isIntVar = false;
                    int intVar = 0;
                    bool Excluded = searchValue[i].StartsWith("!");

                    //Convert boolean string to boolean variable
                    if (bool.TryParse(searchValue[i], out bool boolval))
                    {
                        tempFilter = Builders<T>.Filter.Eq(searchFields[i], boolval);
                    }
                    //Convert int string to int variable
                    if (isIntVar = Int32.TryParse(searchValue[i], out intVar)) tempFilter = Builders<T>.Filter.Eq(searchFields[i], intVar);
                    //Exclude search value
                    if (Excluded) tempFilter = Builders<T>.Filter.Ne(searchFields[i], searchValue[i].Substring(1));

                }
                filterDefinition = Builders<T>.Filter.And(filterDefinition, tempFilter);
            }


            return filterDefinition;
        }
    }
}

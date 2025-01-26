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
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> AddManyItemAsync(List<T> items)
        {
            if (items.Count == 0) return false;

            try
            {
                await _collection.InsertManyAsync(items);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<T> GetByIdAsync(Guid id)
        {


            T result;

            var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5));

            if (_memoryCache.TryGetValue(cacheKey + id, out result))
            {
                return result;
            }

            var filter = Builders<T>.Filter.Eq("Id", id);
            var item = await _collection.FindAsync(filter);

            result = await item.FirstOrDefaultAsync();

            _memoryCache.Set(cacheKey + id, result, cacheEntryOptions);

            return result;


        }
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
        }

        public async Task<IEnumerable<T>> PagingAsync(string[] searchFields, string[] searchValue, string sortField, bool isAsc, int pageSize, int skip)
        {
            string postcacheKey = CacheKeyUtils.GenerateCacheKey(searchFields, searchValue, sortField, isAsc, pageSize, skip);
            var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(1));
            IFindFluent<T, T> query;

            if (_memoryCache.TryGetValue(cacheKey + postcacheKey, out query))
            {
                return await query.ToListAsync();
            }

            FilterDefinition<T> filterDefinition = Builders<T>.Filter.Empty;
            for (int i = 0; i < searchFields.Length; i++)
            {
                FilterDefinition<T> tempFilter = Builders<T>.Filter.Regex(searchFields[i], searchValue[i]);
                filterDefinition = Builders<T>.Filter.And(filterDefinition, tempFilter);
            }

            SortDefinition<T> sortDefinition = isAsc ? Builders<T>.Sort.Ascending(sortField) : Builders<T>.Sort.Descending(sortField);

            query = _collection.Find(filterDefinition)
                                  .Sort(sortDefinition)
                                  .Limit(pageSize)
                                  .Skip((skip - 1) * pageSize);

           _memoryCache.Set(cacheKey + postcacheKey, query, cacheEntryOptions);
            return await query.ToListAsync();
        }


        public async Task<bool> RemoveItemAsync(Guid id)
        {
            try
            {
                var filter = Builders<T>.Filter.Eq("Id", id);
                await _collection.DeleteOneAsync(filter);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateItemAsync(Guid id, T replacement)
        {
            try
            {
                var filter = Builders<T>.Filter.Eq("Id", id);
                await _collection.ReplaceOneAsync(filter, replacement);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<long> CountAsync()
        {
            return await _collection.CountDocumentsAsync(new BsonDocument());
        }

        public async Task<IEnumerable<T>> GetAllByFilterAsync(string[] searchFields, string[] searchValue)
        {
            FilterDefinition<T> filterDefinition = Builders<T>.Filter.Empty;
            for (int i = 0; i < searchFields.Length; i++)
            {
                FilterDefinition<T> tempFilter = Builders<T>.Filter.Regex(searchFields[i], searchValue[i]);
                filterDefinition = Builders<T>.Filter.And(filterDefinition, tempFilter);
            }
            var query = await _collection.Find(filterDefinition).ToListAsync();
            return query;
        }

    }
}

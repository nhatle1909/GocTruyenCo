using MongoDB.Bson;

namespace Application.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        Task<bool> AddOneItemAsync(T item);
        Task<bool> AddManyItemAsync(List<T> items);
        Task<bool> UpdateItemAsync(Guid id, T replacement);
        Task<bool> RemoveItemAsync(Guid id);
        Task<T> GetByIdAsync(Guid id, BsonDocument[] aggregates = null);
        Task<IEnumerable<T>> GetAllByFilterAsync(string[] searchFields, string[] searchValue);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> PagingAsync(string[] searchFields, string[] searchValue, string sortField, bool isAsc, int pageSize, int skip, BsonDocument[] aggregates = null);
        Task<long> CountAsync();

    }
}

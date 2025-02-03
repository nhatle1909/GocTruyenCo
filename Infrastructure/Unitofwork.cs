
using Application.Interface;
using CloudinaryDotNet;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Infrastructure
{
    public class Unitofwork : IUnitofwork
    {
        private readonly IMongoDatabase _database;
        private readonly IMemoryCache _memoryCache;
        private readonly Dictionary<Type, object> _repositories;
        private readonly IConfiguration _configuration;
        private readonly Cloudinary _cloudinary;
        private ICloudinaryRepository _cloudinaryRepository;


        public Unitofwork(IMongoClient client, IConfiguration configuration, IMemoryCache memoryCache, Cloudinary cloudinary)
        {
            _configuration = configuration;
            _memoryCache = memoryCache;
            _database = client.GetDatabase(_configuration.GetSection("DatabaseName").Value);
            _repositories = new Dictionary<Type, object>();
            _cloudinary = cloudinary;
        }
        public async Task CommitAsync()
        {
            await Task.CompletedTask;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Dispose managed resources

            }
        }
        public IGenericRepository<T> GetRepository<T>() where T : class
        {

            if (_repositories.ContainsKey(typeof(T)))
            {
                return (IGenericRepository<T>)_repositories[typeof(T)];
            }

            var repository = new GenericRepository<T>(_database, typeof(T).Name, _memoryCache);

            _repositories.Add(typeof(T), repository);

            return repository;
        }

        public ICloudinaryRepository CloudinaryRepository => _cloudinaryRepository ??= new CloudinaryRepository(_cloudinary);
    }
}

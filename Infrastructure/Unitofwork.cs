
using Application.Interface;
using CloudinaryDotNet;
using Infrastructure.Configuration;
using Microsoft.Extensions.Caching.Memory;
using MongoDB.Driver;

namespace Infrastructure
{
    public class Unitofwork : IUnitofwork
    {
        private readonly IMongoDatabase _database;
        private readonly IMemoryCache _memoryCache;
        private readonly Dictionary<Type, object> _repositories;
        //private readonly IConfiguration _configuration;
        private readonly Cloudinary _cloudinary;
        private ICloudinaryRepository _cloudinaryRepository;


        public Unitofwork(MongoDbOptions options, IMemoryCache memoryCache, Cloudinary cloudinary)
        {

            _memoryCache = memoryCache;
            //_database = client.GetDatabase(_configuration.GetSection("DatabaseName").Value);
            _database = new MongoClient(options.ConnectionString).GetDatabase(options.DatabaseName);
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

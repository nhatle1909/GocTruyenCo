using Application.Interface;
using CloudinaryDotNet;
using Infrastructure;
using Infrastructure.Configuration;
using Microsoft.Extensions.Caching.Memory;

namespace GTT.IntegrationTest.Infrastructure
{
    public class UnitOfWorkTest
    {
        private IUnitofwork _unitofwork;
        private IMemoryCache _memoryCache;
        private Cloudinary _cloudinary;
        public UnitOfWorkTest()
        {
            MongoDbOptions options = new MongoDbOptions
            {
                ConnectionString = "mongodb://localhost:27017",
                DatabaseName = "TestDatabase"
            };

            _memoryCache = new MemoryCache(new MemoryCacheOptions());
            _cloudinary = new Cloudinary(new Account("cloudName", "apiKey", "apiSecret"));
            _unitofwork = new Unitofwork(options, _memoryCache, _cloudinary);
        }
        [Fact]
        public void GetRepository_Success()
        {
            //Arrange
            var expected = typeof(GenericRepository<object>);
            //Act
            var result = _unitofwork.GetRepository<object>();
            //Assert
            Assert.True(result.GetType().IsGenericType);
            Assert.NotNull(result);
            Assert.IsType(expected, result);
        }
        [Fact]
        public void CommitAsync_Success()
        {
            //Arrange
            //Act
            var result = _unitofwork.CommitAsync();
            //Assert
            Assert.NotNull(result);
        }
        [Fact]
        public void Dispose_Success()
        {
            //Arrange
            //Act
            _unitofwork.Dispose();
            //Assert
            Assert.True(true);
        }
        [Fact]
        public void CloudinaryRepository_Success()
        {
            //Arrange
            var expected = typeof(CloudinaryRepository);
            //Act
            var result = _unitofwork.CloudinaryRepository;
            //Assert
            Assert.NotNull(result);
            Assert.IsType(expected, result);
        }

    }
}

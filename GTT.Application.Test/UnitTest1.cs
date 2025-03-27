using Application.Interface;
using CloudinaryDotNet;
using Infrastructure;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Moq;

namespace GTT.Application.Test
{
    public class InfrastructureTest
    {
        public class UnitOfWorkTests
        {
            Mock<IMongoClient> mockMongoClient = new Mock<IMongoClient>();
            Mock<IMongoDatabase> mockDatabase = new Mock<IMongoDatabase>();
            Mock<IConfiguration> mockConfiguration = new Mock<IConfiguration>();
            Mock<IMemoryCache> mockMemoryCache = new Mock<IMemoryCache>();
            string cloudName = "";
            string apiKey = "";
            string apiSecret = "";
            public void UnitOfWork_Arrange_Success()
            {
                // Arrange
                mockConfiguration.Setup(config => config.GetSection("Cloudinary:CloudName").Value).Returns("CloudinaryName");
                mockConfiguration.Setup(config => config.GetSection("Cloudinary:ApiKey").Value).Returns("ApiKey");
                mockConfiguration.Setup(config => config.GetSection("Cloudinary:ApiSecret").Value).Returns("ApiSecret");

                 cloudName = mockConfiguration.Object.GetSection("Cloudinary:CloudName").Value;
                 apiKey = mockConfiguration.Object.GetSection("Cloudinary:ApiKey").Value;
                 apiSecret = mockConfiguration.Object.GetSection("Cloudinary:ApiSecret").Value;

               
                mockMongoClient.Setup(client => client.GetDatabase(It.IsAny<string>(), null)).Returns(mockDatabase.Object);
                mockConfiguration.Setup(config => config.GetSection("DatabaseName").Value).Returns("TestDatabase");
            }
            [Fact]
            public void Constructor_InitializesDatabaseAndDependencies()
            {
                UnitOfWork_Arrange_Success();
                var mockCloudinary = new Mock<Cloudinary>(new Account(cloudName, apiKey, apiSecret));
                // Act
                var unitOfWork = new Unitofwork(mockMongoClient.Object, mockConfiguration.Object, mockMemoryCache.Object, mockCloudinary.Object);

                // Assert
                Assert.NotNull(unitOfWork);
            }
            [Fact]
            public void GetRepository_ReturnsRepository()
            {
                UnitOfWork_Arrange_Success();
                var mockCloudinary = new Mock<Cloudinary>(new Account(cloudName, apiKey, apiSecret));
                var unitOfWork = new Unitofwork(mockMongoClient.Object, mockConfiguration.Object, mockMemoryCache.Object, mockCloudinary.Object);

                // Act
                var repository = unitOfWork.GetRepository<GenericRepository<object>>();

                // Assert
                Assert.NotNull(repository);
            }
            [Fact]
            public void CloudinaryRepository_ReturnsRepository()
            {
                UnitOfWork_Arrange_Success();
                var mockCloudinary = new Mock<Cloudinary>(new Account(cloudName, apiKey, apiSecret));
                var unitOfWork = new Unitofwork(mockMongoClient.Object, mockConfiguration.Object, mockMemoryCache.Object, mockCloudinary.Object);

                // Act
                var repository = unitOfWork.CloudinaryRepository;

                // Assert
                Assert.NotNull(repository);
            }
            [Fact]
            // Test CommitAsync method
            public async Task CommitAsync_Success()
            {
                UnitOfWork_Arrange_Success();
                var mockCloudinary = new Mock<Cloudinary>(new Account(cloudName, apiKey, apiSecret));
                var unitOfWork = new Unitofwork(mockMongoClient.Object, mockConfiguration.Object, mockMemoryCache.Object, mockCloudinary.Object);

                // Act
                await unitOfWork.CommitAsync();

                // Assert
                Assert.True(true);
            }
            
            [Fact]
            public void Dispose_Success()
            {
                UnitOfWork_Arrange_Success();
                var mockCloudinary = new Mock<Cloudinary>(new Account(cloudName, apiKey, apiSecret));
                var unitOfWork = new Unitofwork(mockMongoClient.Object, mockConfiguration.Object, mockMemoryCache.Object, mockCloudinary.Object);

                // Act
                unitOfWork.Dispose();

                // Assert
                Assert.True(true);
            }

        }
    }

}
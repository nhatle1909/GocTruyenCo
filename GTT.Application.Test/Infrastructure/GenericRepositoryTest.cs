using Application.Common;
using Application.DTO;
using Application.Interface;
using Application.Interface.Service;

using Domain.Entities;
using Infrastructure;
using Microsoft.Extensions.Caching.Memory;
using MongoDB.Driver;
using Moq;
using System.Globalization;

namespace GTT.IntegrationTest.Infrastructure
{
    public class GenericRepositoryTest
    {
        Account testItem;
        private GenericRepository<Account> _genericRepository;
        private IMongoClient _mongoClient;
        private IMemoryCache _memoryCache;

        public GenericRepositoryTest()
        {
            _mongoClient = new MongoClient("mongodb+srv://cadenzavna:cadenz@cadenz.zmjag.mongodb.net/?retryWrites=true&w=majority&appName=Cadenz");
            var IMongoDatabase = _mongoClient.GetDatabase("TestDatabase");
            _memoryCache = new MemoryCache(new MemoryCacheOptions());
            _genericRepository = new GenericRepository<Account>(
                IMongoDatabase,
                "TestAccountCollection",
                _memoryCache);
            testItem = new Account
            {
                Id = Guid.NewGuid(),
                Username = "admin",
                Email = "admin@gmail.com",
                Password = "admin",
                Role = Domain.Enums.Role.Admin,
                isRestricted = false,
                CreatedDate = DateTime.Now.ToString("d", new CultureInfo("vi-VN")),
                isDeleted = false
            };
        }
        //Test with T = Account


        [Fact]
        public async Task AddOneItemAsync_Success()
        {

            //auto fill all properties of Account

            var result = await _genericRepository.AddOneItemAsync(testItem);
            Assert.True(result);
            Assert.NotNull(result);

            var query = await _genericRepository.GetByIdAsync(testItem.Id);
            Assert.NotNull(query);
            Assert.Equal(testItem.Id, query.Id);

        }
        [Fact]
        public async Task RemoveOneItemAsync_Success()
        {

            var result = await _genericRepository.RemoveItemAsync(testItem.Id);
            Assert.True(result);


            var query = await _genericRepository.GetByIdAsync(testItem.Id);
            Assert.Null(query);

        }
        [Fact]
        public async Task UpdateItemAsync_Success()
        {
            var result = await _genericRepository.AddOneItemAsync(testItem);
            Assert.True(result);
            var query = await _genericRepository.GetByIdAsync(testItem.Id);
            Assert.NotNull(query);
            Assert.Equal(testItem.Id, query.Id);
            query.Username = "newAdmin";
            await _genericRepository.UpdateItemAsync(testItem.Id, query);
            var query2 = await _genericRepository.GetByIdAsync(testItem.Id);
            Assert.NotNull(query2);
            Assert.Equal("newAdmin", query2.Username);
        }
        [Fact]
        public async Task GetByIdAsync_Success()
        {
            var result = await _genericRepository.AddOneItemAsync(testItem);
            Assert.True(result);
            var query = await _genericRepository.GetByIdAsync(testItem.Id);
            Assert.NotNull(query);
            Assert.Equal(testItem.Id, query.Id);
        }
        [Fact]
        public async Task GetAllItemAsync_Success()
        {
            var result = await _genericRepository.AddOneItemAsync(testItem);
            Assert.True(result);
            var query = await _genericRepository.GetAllAsync();
            Assert.NotNull(query);
            Assert.NotEmpty(query);
        }
        [Fact]
        public async Task GetPagingAsync_Success()
        {
            SearchDTO searchDTO = new SearchDTO
            {
                searchFields = new string[] { "Username" },
                searchValues = new string[] { "admin" },
                sortField = "Username",
                sortAscending = true,
                pageSize = 5,
                skip = 1
            };
            var query = await _genericRepository.PagingAsync(searchDTO.searchFields, searchDTO.searchValues, searchDTO.sortField, searchDTO.sortAscending, searchDTO.pageSize, searchDTO.skip);
            Assert.NotNull(query);
            Assert.NotEmpty(query);
            Assert.True(query.Count() <= searchDTO.pageSize);
            Assert.True(query.ToList()[0].Username.Contains(searchDTO.searchValues[0]));

        }
    }
}
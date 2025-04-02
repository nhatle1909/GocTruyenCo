using Application.Configuration;
using Application.DTO;
using Application.Interface;
using Application.Interface.Service;
using Application.Service;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Infrastructure;
using Infrastructure.Configuration;
using Microsoft.Extensions.Caching.Memory;
using MongoDB.Driver;
using System.Globalization;

namespace GTT.IntegrationTest.Application
{
    public class Account_accountServiceTest
    {
        public IUnitofwork _unitofwork;
        private IMemoryCache _memoryCache;
        private CloudinaryDotNet.Cloudinary _cloudinary;
        private IMapper _mapper;
        private Account account;
        private readonly AccountService _accountService;
        public Account_accountServiceTest()
        {
            
            MongoDbOptions options = new MongoDbOptions
            {
                ConnectionString = "mongodb://localhost:27017",
                DatabaseName = "TestDatabase"
            };
      

            _memoryCache = new MemoryCache(new MemoryCacheOptions());
            _cloudinary = new CloudinaryDotNet.Cloudinary(new CloudinaryDotNet.Account("cloudName", "apiKey", "apiSecret"));
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<MapperProfile>()).CreateMapper();

            _unitofwork = new Unitofwork(options, _memoryCache, _cloudinary);
            _accountService = new AccountService(_unitofwork, _mapper);
            account = new Account
            {
                Id = Guid.NewGuid(),
                Username = "admin",
                Email = "admin@gmail.com",
                Password = "admin",
                Role = Role.Reader,
                isRestricted = false,
                CreatedDate = DateTime.Now.ToString("d", new CultureInfo("vi-VN")),
                isDeleted = false
            };
        }
        [Fact]
        public async Task UpdateRoleAsync()
        {
            // Arrange

            await _unitofwork.GetRepository<Account>().AddOneItemAsync(account);

            // Act
            var result = await _accountService.UpdateRoleAsync(account.Id, "Uploader");
            var query = await _unitofwork.GetRepository<Account>().GetByIdAsync(account.Id);
            // Assert
            Assert.True(result.Success);
            Assert.Equal("Role updated successful", result.Message);
            Assert.Equal(Role.Uploader, query.Role);
        }
        [Fact]
        public async Task GetAllAccountAsync()
        {
            // Arrange
            await _unitofwork.GetRepository<Account>().AddOneItemAsync(account);


            // Act
            var result = await _accountService.GetAllAccountAsync();
            // Assert
            Assert.True(result.Success);
            Assert.Equal("Retrieve data successful", result.Message);
            Assert.NotNull(result.Result);
        }
        [Fact]
        public async Task UpdateRoleAsync_NotFound()
        {
            // Arrange

            // Act
            var result = await _accountService.UpdateRoleAsync(Guid.NewGuid(), "Uploader");
            // Assert
            Assert.False(result.Success);
            Assert.Equal("Account not found", result.Message);
        }
        [Fact]
        public async Task UpdateRoleAsync_Exception()
        {
            // Arrange

            // Act
            var result = await _accountService.UpdateRoleAsync(Guid.NewGuid(), "Uploader");
            // Assert
            Assert.False(result.Success);
            Assert.Equal("Account not found", result.Message);
        }
        [Fact]
        public async Task GetAllAccountAsync_Exception()
        {
            // Arrange
            var _accountService = new AccountService(null, _mapper);
            // Act
            var result = await _accountService.GetAllAccountAsync();
            // Assert
            Assert.False(result.Success);
        }

        [Fact]
        public async Task UpdateRoleAsync_EmptyRole()
        {
            // Arrange

            // Act
            var result = await _accountService.UpdateRoleAsync(Guid.NewGuid(), "");
            // Assert
            Assert.False(result.Success);
        }
        [Fact]
        public async Task GetByIdAsync_Success()
        {
            // Arrange
            await _unitofwork.GetRepository<Account>().AddOneItemAsync(account);
            // Act
            var result = await _accountService.GetByIdAsync(account.Id);
            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Result);
            Assert.Equal("Retrieve data successful", result.Message);
            Assert.Equal(result.Result.Id, account.Id);
        }
        [Fact]
        public async Task GetByIdAsync_NotFound()
        {
            // Arrange
            // Act
            var result = await _accountService.GetByIdAsync(Guid.NewGuid());
            // Assert
            Assert.False(result.Success);
            Assert.Null(result.Result);
            Assert.Equal("Account not found", result.Message);
        }
        [Fact]
        public async Task RestrictAccount_Success()
        {
            // Arrange
            await _unitofwork.GetRepository<Account>().AddOneItemAsync(account);
            // Act
            var result = await _accountService.RestrictAccountAsync(account.Id);
            // Assert
            Assert.True(result.Success);


        }
        [Fact]
        public async Task RestrictAccount_NotFound()
        {
            // Arrange
            // Act
            var result = await _accountService.RestrictAccountAsync(Guid.NewGuid());
            // Assert
            Assert.False(result.Success);
            Assert.Equal("Account not found", result.Message);
        }
        [Fact]
        public async Task GetPagingAsync_SearchByEmail_Success()
        {
            //Arrange
            SearchDTO searchDTO = new SearchDTO
            {
                searchFields = ["Email"],
                searchValues = [""],
                sortField = "Username",
                sortAscending = true,
                pageSize = 10,
                skip = 1
            };
            //Act
            var result = await _accountService.GetPagingAsync(searchDTO);
            //Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Result);
            Assert.True(result.Result.Count() > 0 && result.Result.Count() <= searchDTO.pageSize);

        }
        [Fact]
        public async Task GetPagingAsync_SearchByUsernameAndEmail_Success()
        {
            //Arrange
            SearchDTO searchDTO = new SearchDTO
            {
                searchFields = ["Username","Email"],
                searchValues = ["","adm"],
                sortField = "Username",
                sortAscending = true,
                pageSize = 10,
                skip = 1
            };
            //Act
            var result = await _accountService.GetPagingAsync(searchDTO);
            //Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Result);
            Assert.True(result.Result.Count() > 0 && result.Result.Count() <= searchDTO.pageSize);

        }
        [Fact]
        public async Task CountAsync_SearchByEmail_Success()
        {
            //Arrange
            CountDTO searchDTO = new CountDTO
            {
                searchFields = ["Email"],
                searchValues = [""],
                pageSize = 10,
              
            };
            //Act
            var result = await _accountService.CountAsync(searchDTO);
            //Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Result);
            Assert.True(result.Result >= 0);
        }
        [Fact]
        public async Task UpdateAccount()
        {
            // Arrange

            await _unitofwork.GetRepository<Account>().AddOneItemAsync(account);

            // Act
            var result = await _accountService.UpdateAccountAsync(account.Id, new CommandAccountDTO {Password = account.Password, Username = "newUsername" });
            //Assert
            Assert.True(result.Success);
            Assert.Equal("Data updated successful", result.Message);
        }
        [Fact]
        public async Task UpdateAccount_NotFound()
        {
            // Arrange

            // Act
            var result = await _accountService.UpdateAccountAsync(Guid.NewGuid(), new CommandAccountDTO { Password = account.Password, Username = "newUsername" });
            //Assert
            Assert.False(result.Success);
            Assert.Equal("Account not found", result.Message);
        }
    }
}

using Application.Configuration;
using Application.DTO;
using Application.Interface;
using Application.Service;
using AutoMapper;
using Domain.Entities;
using Infrastructure;
using Infrastructure.Configuration;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTT.IntegrationTest.Application
{
  
    public class AuthenticateServiceTest
    {
        public IUnitofwork _unitofwork;
        private IMemoryCache _memoryCache;
        private CloudinaryDotNet.Cloudinary _cloudinary;
        private IMapper _mapper;
        private AuthenticateService _authenticateService;
        private ISendMailOTPRepository _sendMailOTPRepository;
        private IConfiguration _configuration;
        private Account account;
        public AuthenticateServiceTest()
        {
            MongoDbOptions options = new MongoDbOptions
            {
                ConnectionString = "mongodb://localhost:27017",
                DatabaseName = "TestDatabase"
            };

            _memoryCache = new MemoryCache(new MemoryCacheOptions());
            _cloudinary = new CloudinaryDotNet.Cloudinary(new CloudinaryDotNet.Account("cloudName", "apiKey", "apiSecret"));
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<MapperProfile>()).CreateMapper();
            //Add in memory config for me to use in the test
            _configuration = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string>
            {
                {"JWT:expire", "720"},
                {"JWT:secretkey", "c1005f2acc31a25a1d9244f3bb941e55387c7daac55fc191860a2349625888f29092459252d294377f2e7f1fd63de1fd80c54ae7fe2d7d856c4d2976a094b514f45a58b8247ea21f0afdcbed83bfd5d07222a1b564a8c0f25382c643139f30171b7c9a85bbbeabfb46ad3ee563365eeaff308f7e954b048d783012a830cc67782ce4b369378aba92641990aa977942fbc5ff313cdb43afedfd122b5151293079430eff0f4b8bf517b89c388834209bf0eb016eaee50c6112538ba4d5cd6e4f665c97f8316fa89abca80493f94fd2ac603a044f114bb2fb4d726e0707"},
                {"JWT:audience", "GocTruyenTranh"},
                {"JWT:issuer","GocTruyenTranh" }
                // Add more configuration key-value pairs as needed
            }).Build();

            _sendMailOTPRepository = new SendMailOTPRepository(_memoryCache,_configuration);
            _unitofwork = new Unitofwork(options, _memoryCache, _cloudinary);
            _authenticateService = new AuthenticateService(_unitofwork, _mapper,_configuration,_sendMailOTPRepository);

        }
        [Fact]
        public async Task Register_Success()
        {
            Random random = new Random();
            //Arrange
            SignUpDTO signUpDTO = new SignUpDTO { Email = random.Next(1,100).ToString(), Password = "readerPassword", Username = "readerUsername" };
            //Act
            var result = await _authenticateService.SignUpAsync(signUpDTO);
            //Assert
            var account = await _unitofwork.GetRepository<Account>().GetAllByFilterAsync(["Email", "Password"], [signUpDTO.Email,signUpDTO.Password]);
            Assert.NotNull(account);
            Assert.True(result.Success);
        }
        [Fact]
        public async Task Register_Fail_EmailExisted()
        {
            //Arrange
            SignUpDTO signUpDTO = new SignUpDTO { Email = "readerEmail@gmail.com", Password = "readerPassword", Username = "readerUsername" };
            //Act
            await _authenticateService.SignUpAsync(signUpDTO);
            var result = await _authenticateService.SignUpAsync(signUpDTO);

            //Assert
            Assert.False(result.Success);
        }
        [Fact]
        public async Task Login_Success()
        {
            //Arrange
            SignUpDTO signUpDTO = new SignUpDTO { Email = "readerEmail@gmail.com", Password = "readerPassword", Username = "readerUsername" };
           
            await _authenticateService.SignUpAsync(signUpDTO);
            //Act
            var result = await _authenticateService.SignInAsync(new AuthenticateDTO { Email = signUpDTO.Email, Password = signUpDTO.Password });
            //Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Result);
        }
        [Fact]
        public async Task Login_Failed()
        {
            SignUpDTO signUpDTO = new SignUpDTO { Email = "", Password = "readerPassword", Username = "readerUsername" };
            //Act
            var result = await _authenticateService.SignInAsync(new AuthenticateDTO { Email = signUpDTO.Email, Password = signUpDTO.Password });
            //Assert
            Assert.False(result.Success);

        }
        [Fact]
        public async Task ChangePassword_Success()
        {
            //Arrange
            SignUpDTO signUpDTO = new SignUpDTO { Email = "readerEmail@gmail.com", Password = "readerPassword", Username = "readerUsername" };

            await _authenticateService.SignUpAsync(signUpDTO);
            AuthenticateDTO authenticateDTO = new AuthenticateDTO { Email = signUpDTO.Email, Password = "NewPass" };
            //Act
            var result = await _authenticateService.ChangePassword(authenticateDTO);
            //Assert
            Assert.True(result.Success);
        }
        [Fact]
        public async Task ChangePassword_Failed()
        {
            //Arrange
            SignUpDTO signUpDTO = new SignUpDTO { Email = "readerEmail@gmail.com", Password = "readerPassword", Username = "readerUsername" };

            await _authenticateService.SignUpAsync(signUpDTO);
            AuthenticateDTO authenticateDTO = new AuthenticateDTO { Email = "", Password = "NewPass" };
            //Act
            var result = await _authenticateService.ChangePassword(authenticateDTO);
            //Assert
            Assert.False(result.Success);
        }
    }
}

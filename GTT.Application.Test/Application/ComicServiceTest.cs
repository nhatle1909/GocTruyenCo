using Application.Configuration;
using Application.DTO;
using Application.Service;
using AutoMapper;
using Infrastructure;
using Infrastructure.Configuration;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTT.IntegrationTest.Application
{
    public class ComicServiceTest
    {
        private Unitofwork _unitofwork;
        private IMapper _mapper;
        private readonly ComicService _comicService;
        private IMemoryCache _memoryCache;
        private CloudinaryDotNet.Cloudinary _cloudinary;
        public ComicServiceTest() 
        {
            MongoDbOptions options = new MongoDbOptions
            {
                ConnectionString = "mongodb://localhost:27017",
                DatabaseName = "TestDatabase"
            };
            _cloudinary = new CloudinaryDotNet.Cloudinary(new CloudinaryDotNet.Account("cloudName", "apiKey", "apiSecret"));
            _memoryCache = new MemoryCache(new MemoryCacheOptions());
            _unitofwork = new Unitofwork(options, _memoryCache, _cloudinary);
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<MapperProfile>()).CreateMapper();
            _comicService = new ComicService(_unitofwork, _mapper);
        }
        //Create async
        [Fact]
        public async Task CreateComic_Success()
        {
            // Arrange

            //var comic = new CommandComicDTO({
            //    UploaderId = ""
            //});
            // Act
            var result = await _comicService.CreateComicAsync(comic);
            // Assert
            Assert.True(result);
        }
    }
}

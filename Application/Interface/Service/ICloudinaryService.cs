using Application.Common;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson.Serialization.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Service
{
    public interface ICloudinaryService
    {
        Task<ServiceResponse<string>> UploadThemeUrl(string comicName, IFormFile image);
        Task<ServiceResponse<IEnumerable<string>>> UploadChapterImage(string comicName, string chapterNumber, List<IFormFile> imageURIs);
    }
}

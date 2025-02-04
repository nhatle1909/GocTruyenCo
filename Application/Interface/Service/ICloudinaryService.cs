using Application.Common;
using Microsoft.AspNetCore.Http;

namespace Application.Interface.Service
{
    public interface ICloudinaryService
    {
        Task<ServiceResponse<string>> UploadThemeUrl(string comicName, IFormFile image);
        Task<ServiceResponse<IEnumerable<string>>> UploadChapterImage(string comicName, string chapterNumber, List<IFormFile> imageURIs);
    }
}

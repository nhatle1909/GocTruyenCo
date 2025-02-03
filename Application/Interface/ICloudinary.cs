using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface ICloudinaryRepository
    {
        Task<IEnumerable<string>> UploadChapterImageAsync(string comicName, string chapterNumber,List<IFormFile> imageURIs);
        Task<string> UploadComicThemeAsync(string ImageUri,IFormFile image);
    }
}

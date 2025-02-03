using Application.Common;
using Application.Interface;
using Application.Interface.Service;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly IUnitofwork _unitofwork;
        public CloudinaryService(IUnitofwork unitofwork)
        {
            _unitofwork = unitofwork;
        }

        public async Task<ServiceResponse<IEnumerable<string>>> UploadChapterImage(string comicName, string chapterNumber, List<IFormFile> imageURIs)
        {
            ServiceResponse<IEnumerable<string>> response = new();
            try
            {
                var result = await _unitofwork.CloudinaryRepository.UploadChapterImageAsync(comicName, chapterNumber, imageURIs);
                response.CustomResponse(result, true, "Upload Images Successfully");
            }
            catch (Exception ex)
            {
                response.TryCatchResponse(ex);
            }
            return response;
        }

        public async  Task<ServiceResponse<string>> UploadThemeUrl(string comicName, IFormFile image)
        {
            ServiceResponse<string> response = new();
            try
            {
                var result = await _unitofwork.CloudinaryRepository.UploadComicThemeAsync(comicName, image);
                
                response.CustomResponse(result,true,"Upload Theme Successfully");
            }
            catch (Exception ex)
            {
                response.TryCatchResponse(ex);
            }
            return response;
        }
    }
}

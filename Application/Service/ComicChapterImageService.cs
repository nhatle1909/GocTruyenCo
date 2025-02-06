using Application.Common;
using Application.DTO;
using Application.Interface;
using Application.Interface.Service;
using AutoMapper;
using Domain.Entities;
using MongoDB.Bson.Serialization.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service
{
    public class ComicChapterImageService : IComicChapterImageService
    {
        private readonly IUnitofwork _unitofwork;
        private readonly IMapper _mapper;
        public ComicChapterImageService(IUnitofwork unitofwork, IMapper mapper)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<bool>> CreateChapterImagesAsync(List<CommandComicChapterImageDTO> createChapterImageDTO)
        {
            ServiceResponse<bool> result = new(); 
            try
            {
                foreach (var item in createChapterImageDTO)
                {
                    var comicChapterImage = _mapper.Map<ComicChapterImage>(item);
                    bool command = await _unitofwork.GetRepository<ComicChapterImage>().AddOneItemAsync(comicChapterImage);
                }
                await _unitofwork.CommitAsync();
                result.CustomResponse(false, false, "Create chapter image successfully");
            }
            catch (Exception ex)
            {
                result.TryCatchResponse(ex);
            }
            return result;
        }

        public async Task<ServiceResponse<bool>> DeleteChapterImageAsync(Guid id)
        {
            ServiceResponse<bool> result = new();
            try
            {
                var query = await _unitofwork.GetRepository<ComicChapterImage>().GetByIdAsync(id);
                if (query == null)
                {
                    result.CustomResponse(false, false, "Chapter image not found");
                }
                
                query.isDeleted = true;
                await _unitofwork.GetRepository<ComicChapterImage>().UpdateItemAsync(id, query);
                await _unitofwork.CommitAsync();

                result.CustomResponse(true, true, "Delete chapter image successfully");
            }
            catch (Exception ex)
            {
                result.TryCatchResponse(ex);
            }
            return result;
        }

        public async Task<ServiceResponse<IEnumerable<QueryAccountDTO>>> GetAllChapterImage(Guid comicChapterId)
        {
           ServiceResponse<IEnumerable<QueryAccountDTO>> result = new();
            try
            {
                string[] searchField = ["ComicChapterId"];
                string[] searchValue = [comicChapterId.ToString()];
                var query = await _unitofwork.GetRepository<ComicChapterImage>().GetAllByFilterAsync(searchField, searchValue);
              
                var data = _mapper.Map<IEnumerable<QueryAccountDTO>>(query);
                result.CustomResponse(data, true, "Get all chapter image successfully");
                
            }
            catch (Exception ex)
            {
                result.TryCatchResponse(ex);
            }
            return result;
        }

        public async Task<ServiceResponse<bool>> UpdateChapterImagesAsync(List<CommandComicChapterImageDTO> updateChapterImageDTO)
        {
            ServiceResponse<bool> result = new();
            try
            {
                foreach (var item in updateChapterImageDTO)
                {
                    var query = await _unitofwork.GetRepository<ComicChapterImage>().GetByIdAsync(item.Id);
                    if (query == null)
                    {
                        result.CustomResponse(false, false, "Chapter image with id " + item + " not found");
                        return result;
                      
                    }
                    var comicChapterImage = _mapper.Map<ComicChapterImage>(updateChapterImageDTO);
                    bool command = await _unitofwork.GetRepository<ComicChapterImage>().UpdateItemAsync(item.Id, comicChapterImage);
                }
                await _unitofwork.CommitAsync();
                result.CustomResponse(false, false, "Update chapter image success");
            }
            catch (Exception ex)
            {
                result.TryCatchResponse(ex);
            }
            return result;
        }
    }
}

using Application.Common;
using Application.DTO;
using Application.Interface;
using Application.Interface.Service;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service
{
    public class ComicChapterService : IComicChapterService
    {
        private readonly IUnitofwork _unitofwork;
        private readonly IMapper _mapper;
        public ComicChapterService(IUnitofwork unitofwork,IMapper mapper) 
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<bool>> CreateChapter(CommandComicChapterDTO commandComicChapterDTO)
        {
            ServiceResponse<bool> result = new();
            try
            {
                var comicChapter = _mapper.Map<ComicChapter>(commandComicChapterDTO);
                bool command = await _unitofwork.GetRepository<ComicChapter>().AddOneItemAsync(comicChapter);
                if (command)
                {
                    await _unitofwork.CommitAsync();
                    result.CustomResponse(true, true, "Create chapter successful");
                }
                else
                {
                    result.CustomResponse(false, false, "Create chapter failed");
                }
            }
            catch (Exception ex)
            {
                result.TryCatchResponse(ex);
            }
            return result;
        }

        public async Task<ServiceResponse<QueryComicChapterDTO>> GetAllChapter(Guid comicId)
        {
            ServiceResponse<QueryComicChapterDTO> result = new();
            try
            {
                string[] searchField = ["ComicId"];
                string[] searchValue = [comicId.ToString()];
                var query = await _unitofwork.GetRepository<ComicChapter>().GetAllByFilterAsync(searchField, searchValue);
                
                var data = _mapper.Map<QueryComicChapterDTO>(query);
                result.CustomResponse(data, true, "Get all chapter successful");
                
            }
            catch (Exception ex)
            {
                result.TryCatchResponse(ex);
            }
            return result;
        }

        public async Task<ServiceResponse<QueryComicChapterDTO>> GetChaptersPaging(SearchDTO searchDTO)
        {
            ServiceResponse<QueryComicChapterDTO> result = new();
            try
            {
               
                var query = await _unitofwork.GetRepository<ComicChapter>().PagingAsync(searchDTO.searchFields,searchDTO.searchValues,searchDTO.sortField,
                                                                                        searchDTO.sortAscending,searchDTO.pageSize,searchDTO.skip);
             
                var data = _mapper.Map<QueryComicChapterDTO>(query);
                result.CustomResponse(data, true, "Get all chapter successful");
                
            }
            catch (Exception ex)
            {
                result.TryCatchResponse(ex);
            }
            return result;
        }

        public async Task<ServiceResponse<bool>> UpdateChapter(Guid chapterId, CommandComicChapterDTO commandComicChapterDTO)
        {
            ServiceResponse<bool> result = new();
            try
            {
                var query = await _unitofwork.GetRepository<ComicChapter>().GetByIdAsync(chapterId);
                if (query != null)
                {
                    var comicChapter = _mapper.Map<ComicChapter>(commandComicChapterDTO);
 
                    bool command = await _unitofwork.GetRepository<ComicChapter>().UpdateItemAsync(chapterId, comicChapter);
                    if (command)
                    {
                        await _unitofwork.CommitAsync();
                        result.CustomResponse(true, true, "Update chapter successful");
                    }
                    else
                    {
                        result.CustomResponse(false, false, "Update chapter failed");
                    }
                }
                else
                {
                    result.CustomResponse(false, false, "Chapter not found");
                }
            }
            catch (Exception ex)
            {
                result.TryCatchResponse(ex);
            }
            return result;
        }
    }
}

using Application.DTO;
using AutoMapper;
using Domain.Entities;

namespace Application.Configuration
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            //Authenticate
            CreateMap<Account, AuthenticateDTO>().ReverseMap();
            CreateMap<Account, SignUpDTO>().ReverseMap();
            //Account
            CreateMap<Account, QueryAccountDTO>().ReverseMap();
            CreateMap<Account, CommandAccountDTO>().ReverseMap();
            //ComicCategory
            CreateMap<ComicCategory, QueryComicCategoryDTO>().ReverseMap();
            CreateMap<ComicCategory, CommandComicCategoryDTO>().ReverseMap();
            //Comic
            CreateMap<Comic,QueryComicDTO>().ReverseMap();
            CreateMap<Comic, CommandComicDTO>().ReverseMap();
        }
    }
}

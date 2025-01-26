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
            //Account
            CreateMap<Account, AccountDTO>().ReverseMap();
            CreateMap<Account, CreateAccountDTO>().ReverseMap();
            CreateMap<Account, UpdateAccountDTO>().ReverseMap();

        }
    }
}

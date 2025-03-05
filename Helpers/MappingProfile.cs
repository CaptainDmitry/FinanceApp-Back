using AutoMapper;
using TestApi.DTOs;
using TestApi.Models;

namespace TestApi.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<Account, AccountDto>();
        }

    }
}

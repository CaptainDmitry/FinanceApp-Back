using AutoMapper;
using TestApi.DTOs;
using TestApi.Models;

namespace TestApi.Helpers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<Account, AccountDto>();
            CreateMap<Transaction, TransactionDto>();
        }

    }
}

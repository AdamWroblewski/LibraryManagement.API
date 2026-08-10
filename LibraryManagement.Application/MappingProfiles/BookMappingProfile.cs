using AutoMapper;
using LibraryManagement.Application.DTOs;
using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Application.MappingProfiles
{
    public class BookMappingProfile : Profile
    {
        public BookMappingProfile()
        {
            CreateMap<Book, BookListDto>();
            CreateMap<Book, BookDetailsDto>().ReverseMap();
        }
    }
}

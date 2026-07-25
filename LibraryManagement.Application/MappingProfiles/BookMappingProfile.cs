using AutoMapper;
using LibraryManagement.Application.Commands.Books;
using LibraryManagement.Application.DTOs;
using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Application.MappingProfiles
{
    public class BookMappingProfile : Profile
    {
        public BookMappingProfile()
        {
            CreateMap<CreateBookCommand, Book>().ReverseMap();
            CreateMap<Book, BookDto>().ReverseMap();
        }
    }
}

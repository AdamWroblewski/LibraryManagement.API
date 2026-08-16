using AutoMapper;
using LibraryManagement.Application.DTOs;
using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Application.MappingProfiles
{
    public class BookMappingProfile : Profile
    {
        public BookMappingProfile()
        {
            int userId = 0;

            CreateMap<Book, BookListDto>();

            CreateMap<Book, BookDetailsDto>()
            .ForMember(dest => dest.IsAvailable,
                opt => opt.MapFrom(src => !src.Loans.Any(l => l.ReturnedAt == null)))
            .ForMember(dest => dest.CurrentUserLoans,
                    opt => opt.MapFrom(src => src.Loans.Where(l => l.UserId == userId)));
        }
    }
}

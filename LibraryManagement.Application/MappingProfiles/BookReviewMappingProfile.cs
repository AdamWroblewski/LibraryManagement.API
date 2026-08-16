using AutoMapper;
using LibraryManagement.Application.DTOs;
using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Application.MappingProfiles
{
    public class BookReviewMappingProfile : Profile
    {
        public BookReviewMappingProfile()
        {
            CreateMap<BookReview, BookReviewDto>();
        }
    }
}

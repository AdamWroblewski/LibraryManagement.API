using AutoMapper;
using LibraryManagement.Application.Commands.BookReservations;
using LibraryManagement.Application.Commands.Books;
using LibraryManagement.Application.DTOs;
using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Application.MappingProfiles
{
    public class BookLoanMappingProfile : Profile
    {
        public BookLoanMappingProfile()
        {
            CreateMap<CreateBookReservationCommand, BookLoan>();
            CreateMap<BookLoan, ActiveBookLoanDto>();
        }
    }
}

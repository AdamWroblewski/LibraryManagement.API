using AutoMapper;
using LibraryManagement.Application.Commands.BookLoans;
using LibraryManagement.Application.DTOs;
using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Application.MappingProfiles
{
    public class BookLoanMappingProfile : Profile
    {
        public BookLoanMappingProfile()
        {
            DateTime utcNow = DateTime.MinValue;

            CreateMap<CreateBookLoanCommand, BookLoan>();

            CreateMap<BookLoan, BookLoanDto>()
                .ForMember(dest => dest.ReservationExpiresAt, opt => opt.MapFrom(src =>
                    src.ReservedAt.AddHours(BookLoan.HoldPolicyHours)))
                .ForMember(dest => dest.IsReservationExpired, opt => opt.MapFrom(src =>
                    src.Status == LoanStatus.Expired ||
                    src.Status == LoanStatus.Cancelled ||
                    (src.Status == LoanStatus.Reserved && src.ReservedAt.AddHours(BookLoan.HoldPolicyHours) <= utcNow)));
        }
    }
}

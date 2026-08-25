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
            DateTime utcNow = DateTime.MinValue;

            CreateMap<Book, BookDetailsDto>()
                .ForMember(dest => dest.IsAvailable, opt => opt.MapFrom(src =>
                    !src.Loans.Any(l =>
                        l.Status == LoanStatus.Active ||
                        l.Status == LoanStatus.Overdue ||
                        (l.Status == LoanStatus.Reserved && l.ReservedAt.AddHours(BookLoan.HoldPolicyHours) > utcNow))))
                .ForMember(dest => dest.CurrentUserLoans, opt => opt.MapFrom(src =>
                    src.Loans.Where(l => l.UserId == userId)))
                .ForMember(dest => dest.Reviews, opt => opt.Ignore());
        }
    }
}

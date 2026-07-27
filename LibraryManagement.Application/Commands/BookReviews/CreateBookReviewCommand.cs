using LibraryManagement.Application.DTOs;
using MediatR;

namespace LibraryManagement.Application.Commands.BookReviews
{
    public class CreateBookReviewCommand : IRequest<int>
    {
        public string Comment { get; set; }
        public int BookId { get; set; }
        public int ApplicationUserId { get; set; }
        public int Rate { get; set; }
    }
}

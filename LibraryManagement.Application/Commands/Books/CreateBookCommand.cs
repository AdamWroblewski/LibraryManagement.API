using LibraryManagement.Application.DTOs;
using MediatR;

namespace LibraryManagement.Application.Commands.Books
{
    public class CreateBookCommand : IRequest<BookDto>
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public int PublicationYear { get; set; }
        public string Publisher { get; set; }
        public int AvailableCopies { get; set; }
    }
}
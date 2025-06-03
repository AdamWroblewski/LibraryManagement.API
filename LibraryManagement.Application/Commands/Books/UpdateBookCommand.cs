using MediatR;

namespace LibraryManagement.Application.Commands.Books
{
    public class UpdateBookCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public int PublicationYear { get; set; }
        public string Publisher { get; set; }
    }
}

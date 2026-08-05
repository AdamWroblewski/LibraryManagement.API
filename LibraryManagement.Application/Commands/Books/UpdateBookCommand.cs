using MediatR;
using System.Text.Json.Serialization;

namespace LibraryManagement.Application.Commands.Books
{
    public record UpdateBookCommand(
        [property: JsonIgnore] int Id, 
        string Title, 
        string Author, 
        string ISBN, 
        int PublicationYear, 
        string Publisher) : IRequest<Unit>;
}

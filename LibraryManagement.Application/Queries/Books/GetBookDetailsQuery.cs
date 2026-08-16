using LibraryManagement.Application.DTOs;
using MediatR;
using System.Text.Json.Serialization;

namespace LibraryManagement.Application.Queries.Books
{
    public record GetBookDetailsQuery(int Id, [property: JsonIgnore] int UserId) : IRequest<BookDetailsDto>;
}

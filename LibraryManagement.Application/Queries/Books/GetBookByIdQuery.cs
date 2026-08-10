using LibraryManagement.Application.DTOs;
using MediatR;
using System.Text.Json.Serialization;

namespace LibraryManagement.Application.Queries.Books
{
    public record GetBookByIdQuery([property: JsonIgnore] int UserId, int Id) : IRequest<BookDetailsDto>;
}

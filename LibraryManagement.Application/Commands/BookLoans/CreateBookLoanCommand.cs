using MediatR;
using System.Text.Json.Serialization;

namespace LibraryManagement.Application.Commands.BookLoans
{
    public record CreateBookLoanCommand([property: JsonIgnore] int UserId, int BookId) : IRequest<int>;
}

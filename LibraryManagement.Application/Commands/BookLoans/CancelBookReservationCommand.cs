using MediatR;
using System.Text.Json.Serialization;

namespace LibraryManagement.Application.Commands.BookLoans
{
    public record CancelBookReservationCommand(int Id, [property: JsonIgnore] int UserId) : IRequest<Unit>;
}

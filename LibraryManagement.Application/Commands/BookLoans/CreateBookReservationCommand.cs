using MediatR;

namespace LibraryManagement.Application.Commands.BookReservations
{
    public record CreateBookReservationCommand(int BookId, int ApplicationUserId) : IRequest<int>;
}

using FluentResults;
using MediatR;

namespace LibraryManagement.Application.Commands.BookReservations
{
    public record CreateBookReservationCommand(int bookId, int applicationUserId) : IRequest<Result>;
}

using MediatR;
using System.Text.Json.Serialization;

namespace LibraryManagement.Application.Commands.BookReviews
{
    public record DeleteBookReviewCommand(
        [property: JsonIgnore] int UserId,
        [property: JsonIgnore] int BookId
        ) : IRequest<Unit>;
}

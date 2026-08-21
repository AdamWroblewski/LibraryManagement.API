using MediatR;
using System.Text.Json.Serialization;

namespace LibraryManagement.Application.Commands.BookReviews
{
    public record UpdateBookReviewCommand(
        [property: JsonIgnore] int UserId,
        [property: JsonIgnore] int BookId,
        string Comment,
        int Rate
        ) : IRequest<Unit>;
}

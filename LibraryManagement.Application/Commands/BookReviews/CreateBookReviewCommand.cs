using MediatR;
using System.Text.Json.Serialization;

namespace LibraryManagement.Application.Commands.BookReviews
{
    public record CreateBookReviewCommand(
        [property: JsonIgnore] int UserId,
        [property: JsonIgnore] int BookId,
        string Comment,
        int Rate 
        ) : IRequest<int>;
}

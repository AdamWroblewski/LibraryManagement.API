using LibraryManagement.Application.Commands.BookReviews;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.API.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BookReviewsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookReviewsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost("{id}/reviews")]
        public async Task<IActionResult> CreateReview(int id, [FromBody] CreateBookReviewCommand command)
        {
            command.BookId = id;
            var reviewId = await _mediator.Send(command);
            return CreatedAtAction(
                actionName: "GetBookById",
                controllerName: "Books",
                routeValues: new { id = command.BookId },
                value: new { id = reviewId });
        }
    }
}
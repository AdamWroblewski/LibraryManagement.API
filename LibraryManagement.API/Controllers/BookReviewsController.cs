using LibraryManagement.Application.Commands.BookReviews;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.API.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BookReviewsController : BaseApiController
    {
        private readonly IMediator _mediator;

        public BookReviewsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet("{bookId:int}/reviews/{reviewId:int}")]
        public async Task<IActionResult> GetById()
        {
            return null;
        }

        [Authorize]
        [HttpPost("{bookId:int}/reviews")]
        public async Task<IActionResult> Create(int bookId, [FromBody] CreateBookReviewCommand command, CancellationToken cancellationToken)
        {
            var secureCommand = command with
            {
                BookId = bookId,
                UserId = UserId
            };

            var reviewId = await _mediator.Send(secureCommand, cancellationToken);

            return CreatedAtAction(
                actionName: nameof(GetById),
                routeValues: new { id = secureCommand.BookId, reviewId = reviewId },
                value: new { id = reviewId });
        }

        [Authorize]
        [HttpPut("{bookId:int}/reviews")]
        public async Task<IActionResult> Update(int bookId, [FromBody] UpdateBookReviewCommand command, CancellationToken cancellationToken)
        {
            var secureCommand = command with
            {
                BookId = bookId,
                UserId = UserId
            };

            await _mediator.Send(secureCommand, cancellationToken);

            return NoContent();
        }

        [Authorize]
        [HttpDelete("{bookId:int}/reviews")]
        public async Task<IActionResult> Delete(int bookId, DeleteBookReviewCommand command, CancellationToken cancellationToken)
        {
            var secureCommand = command with
            {
                BookId = bookId,
                UserId = UserId
            };

            await _mediator.Send(secureCommand, cancellationToken);

            return NoContent();
        }
    }
}
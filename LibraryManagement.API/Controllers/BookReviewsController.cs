using LibraryManagement.Application.Commands.BookReviews;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        [HttpGet("{bookId:int}/reviews/{reviewId:int}")]
        public async Task<IActionResult> GetReviewById()
        {
            return null;
        }

        [Authorize]
        [HttpPost("{bookId:int}/reviews")]
        public async Task<IActionResult> CreateReview(int bookId, [FromBody] CreateBookReviewCommand command)
        {
            var secureCommand = command with
            {
                BookId = bookId,
                ApplicationUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)
            };

            var reviewId = await _mediator.Send(secureCommand);

            return CreatedAtAction(
                actionName: nameof(GetReviewById),
                routeValues: new { id = command.BookId, reviewId = reviewId },
                value: new { id = reviewId });
        }
    }
}
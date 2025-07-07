using System.Security.Claims;
using LibraryManagement.Application.Commands.BookReservations;
using LibraryManagement.Application.Queries.BookLoans;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace LibraryManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookLoansController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookLoansController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet("activeLoans/{bookId}")]
        public async Task<ActionResult> GetActiveLoans(int bookId)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var query = new GetActiveLoansQuery(bookId, userId);

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> CreateBookReservation([FromBody] CreateBookReservationCommand command)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var commandWithUserId = command with { applicationUserId = userId };

            var result = await _mediator.Send(commandWithUserId);
            if (!result.IsSuccess)
                return BadRequest(result.Errors);

            return Ok(new { SuccessMessage = $"The book has been reserved correctly, the reservation is valid until { DateTime.UtcNow.AddDays(-4).Date.ToShortDateString() }" });
        }
    }
}
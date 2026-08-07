using LibraryManagement.Application.Commands.BookReservations;
using LibraryManagement.Application.Queries.BookLoans;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        [HttpGet("book/{bookId:int}")]
        public async Task<ActionResult> GetActiveLoansByBookId(int bookId, CancellationToken cancellationToken)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var query = new GetActiveLoansQuery(bookId, userId);

            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result);
        }

        [Authorize]
        [HttpGet("activeLoans/{id:int}")]
        public async Task<ActionResult> GetLoanById(int id)
        {
            return Ok();
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> CreateBookReservation([FromBody] CreateBookReservationCommand command)
        {
            var loanId = await _mediator.Send(command);
            return CreatedAtAction(
                nameof(GetLoanById),
                new { id = loanId },
                new { id = loanId });
        }
    }
}
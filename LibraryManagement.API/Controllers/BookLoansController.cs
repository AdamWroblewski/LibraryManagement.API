using LibraryManagement.Application.Commands.BookLoans;
using LibraryManagement.Application.DTOs;
using LibraryManagement.Application.Models;
using LibraryManagement.Application.Queries.BookLoans;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.API.Controllers
{
    [Route("api/book-loans")]
    [ApiController]
    public class BookLoansController : BaseApiController
    {
        private readonly IMediator _mediator;

        public BookLoansController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<PagedList<BookLoanDto>>> Get(CancellationToken cancellationToken, 
            int pageNumber = 1,
            int pageSize = 25)
        {
            var secureQuery = new GetBookLoansQuery(UserId, pageNumber, pageSize);

            var result = await _mediator.Send(secureQuery, cancellationToken);

            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> CreateBookLoan([FromBody] CreateBookLoanCommand command, CancellationToken cancellationToken)
        {
            var secureCommand = command with
            {
                UserId = UserId
            };

            var loanId = await _mediator.Send(secureCommand, cancellationToken);

            return StatusCode(StatusCodes.Status201Created, new { id = loanId });
        }

        [Authorize(Roles = "Employee")]
        [HttpPost("{id:int}/checkout")]
        public async Task<IActionResult> Checkout(int id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new CheckoutBookLoanCommand(id), cancellationToken);
            return NoContent();
        }

        [Authorize(Roles = "Employee")]
        [HttpPost("direct")]
        public async Task<IActionResult> CreateDirectLoan([FromBody] CreateDirectBookLoanCommand command, CancellationToken cancellationToken)
        {
            var loanId = await _mediator.Send(command);

            return StatusCode(StatusCodes.Status201Created, new { id = loanId });
        }

        [Authorize(Roles = "Employee")]
        [HttpPost("{id:int}/return")]
        public async Task<IActionResult> Return(int id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new ReturnBookLoanCommand(id));

            return NoContent();
        }
    }
}
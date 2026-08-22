using LibraryManagement.Application.Commands.BookLoans;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookLoansController : BaseApiController
    {
        private readonly IMediator _mediator;

        public BookLoansController(IMediator mediator)
        {
            _mediator = mediator;
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

            // TODO: change to CreatedAtAction()
            return StatusCode(StatusCodes.Status201Created, new { id = loanId });
        }
    }
}
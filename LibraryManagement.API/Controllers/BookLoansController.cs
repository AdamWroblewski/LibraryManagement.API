using LibraryManagement.Application.Commands.BookReservations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        [HttpPost]
        public async Task<ActionResult> CreateBookLoan([FromBody] CreateBookLoanCommand command)
        {
            if (command.Status != LoanStatus.Active && command.Status != LoanStatus.Reserved)
                throw new InvalidOperationException("Invalid status for new reservation.");

            var loanId = await _mediator.Send(command); 
            
            // TODO: change to CreatedAtAction()
            return StatusCode(StatusCodes.Status201Created, new { id = loanId });
        }
    }
}
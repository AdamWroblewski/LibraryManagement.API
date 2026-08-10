using LibraryManagement.Application.Commands.Books;
using LibraryManagement.Application.DTOs;
using LibraryManagement.Application.Queries.Books;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LibraryManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BooksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<BookListDto>>> GetAll(CancellationToken cancellationToken)
        {
            var query = new GetAllBooksQuery();
            var books = await _mediator.Send(query, cancellationToken);
            return Ok(books);
        }

        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<BookDetailsDto>> GetById(int id, CancellationToken cancellationToken)
        {

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
             
            var query = new GetBookByIdQuery(id, userId);
            var book = await _mediator.Send(query, cancellationToken);
            return Ok(book);
        }

        [Authorize(Roles = "Employee")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBookCommand command, CancellationToken cancellationToken)
        {
            var bookId = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = bookId }, new { id = bookId });
        }

        [Authorize(Roles = "Employee")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateBookCommand command, CancellationToken cancellationToken)
        {
            var secureCommand = command with
            {
                Id = id
            };

            await _mediator.Send(secureCommand, cancellationToken);
            return NoContent();
        }

        [Authorize(Roles = "Employee")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteBookByIdCommand(id), cancellationToken);

            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
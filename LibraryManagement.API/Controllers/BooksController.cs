using LibraryManagement.Application.Commands.Books;
using LibraryManagement.Application.DTOs;
using LibraryManagement.Application.Models;
using LibraryManagement.Application.Queries.Books;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : BaseApiController
    {
        private readonly IMediator _mediator;

        public BooksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<PagedList<BookListDto>>> GetPagedBooks(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var query = new GetPagedBooksQuery(pageNumber, pageSize);
            
            var books = await _mediator.Send(query, cancellationToken);
            
            return Ok(books);
        }

        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<BookDetailsDto>> GetBookDetails(int id, CancellationToken cancellationToken)
        {
            var query = new GetBookDetailsQuery(id, UserId);

            var book = await _mediator.Send(query, cancellationToken);

            return Ok(book);
        }

        [Authorize(Roles = "Employee")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBookCommand command, CancellationToken cancellationToken)
        {
            var bookId = await _mediator.Send(command, cancellationToken);
            
            return CreatedAtAction(nameof(GetBookDetails), new { id = bookId }, new { id = bookId });
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
            await _mediator.Send(new DeleteBookCommand(id), cancellationToken);

            return NoContent();
        }
    }
}
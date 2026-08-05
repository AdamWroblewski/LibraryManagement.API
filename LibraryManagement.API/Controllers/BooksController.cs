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

        [HttpGet]
        public async Task<ActionResult<List<BookDto>>> GetAll()
        {
            var query = new GetAllBooksQuery();
            var books = await _mediator.Send(query);
            return Ok(books);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<List<BookDto>>> GetById(int id)
        {
            var query = new GetBookByIdQuery(id);
            var book = await _mediator.Send(query);
            return Ok(book);
        }

        [Authorize(Roles = "Employee")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBookCommand command)
        {
            var bookId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = bookId }, bookId);
        }

        [Authorize(Roles = "Employee")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateBookCommand command)
        {
            var secureCommand = command with
            {
                Id = id
            };

            await _mediator.Send(secureCommand);
            return NoContent();
        }

        [Authorize(Roles = "Employee")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteBookByIdCommand(id));

            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
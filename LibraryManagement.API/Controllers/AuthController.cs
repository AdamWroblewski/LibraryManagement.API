using LibraryManagement.Application.Commands.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }

            //TODO: change to CreatedAtAction
            return Created($"/api/users/{result.Value}", new { id = result.Value });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.IsSuccess)
                return Ok(new { token = result.Value });

            return BadRequest(new { errors = result.Errors });
        }
    }
}
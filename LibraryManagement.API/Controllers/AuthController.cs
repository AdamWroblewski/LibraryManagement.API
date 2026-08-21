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
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand command, CancellationToken cancellationToken)
        {
            var newUserId = await _mediator.Send(command, cancellationToken);

            //TODO: change to CreatedAtAction
            return Created($"/api/users/{newUserId}", new { id = newUserId });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserCommand command, CancellationToken cancellationToken)
        {
            string token = await _mediator.Send(command, cancellationToken);

            return Ok(new { token });
        }
    }
}
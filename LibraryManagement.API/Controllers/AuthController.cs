using LibraryManagement.Application.Commands.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : BaseApiController
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

            return Created($"/api/users/{newUserId}", new { id = newUserId });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("create-employee")]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeCommand command, CancellationToken cancellationToken)
        {
            var newEmployeeId = await _mediator.Send(command, cancellationToken);

            return Created($"/api/users/{newEmployeeId}", new { id = newEmployeeId });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserCommand command, CancellationToken cancellationToken)
        {
            string token = await _mediator.Send(command, cancellationToken);

            return Ok(new { token });
        }

        [Authorize]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command, CancellationToken cancellationToken)
        {
            var secureCommand = command with { UserId = UserId };

            await _mediator.Send(secureCommand, cancellationToken);

            return NoContent();
        }
    }
}
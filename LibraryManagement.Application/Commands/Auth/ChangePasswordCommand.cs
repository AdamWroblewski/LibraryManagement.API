using MediatR;
using System.Text.Json.Serialization;

namespace LibraryManagement.Application.Commands.Auth
{
    public record ChangePasswordCommand(
        [property: JsonIgnore] int UserId,
        string CurrentPassword,
        string NewPassword) : IRequest<Unit>;
}

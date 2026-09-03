using LibraryManagement.Application.Interfaces;
using MediatR;

namespace LibraryManagement.Application.Commands.Auth
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, int>
    {
        private readonly IIdentityService _identityService;

        public CreateEmployeeCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<int> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employeeId = await _identityService.CreateEmployeeAsync(request.Email, 
                request.Password, 
                request.FirstName, 
                request.LastName,
                cancellationToken);
            
            return employeeId;
        }
    }
}

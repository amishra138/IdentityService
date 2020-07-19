using IdentityService.Domain.Contracts;
using IdentityService.Domain.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityService.Application.Commands
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, UserResponse>
    {
        private readonly IUserService _userService;
        public RegisterUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<UserResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var response = _userService.Register(request.User);

            return response;
        }
    }
}

using IdentityService.Domain.Models;
using MediatR;

namespace IdentityService.Application.Commands
{
    public class AuthenticateUserCommand : IRequest<AuthenticateResponse>
    {
        public AuthenticateRequest AuthenticateRequest { get; private set; }
        public AuthenticateUserCommand(AuthenticateRequest request)
        {
            AuthenticateRequest = request;
        }
    }
}

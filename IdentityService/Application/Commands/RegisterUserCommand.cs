using IdentityService.Domain.Models;
using MediatR;

namespace IdentityService.Application.Commands
{
    /// <summary>
    /// Command to register user
    /// </summary>
    public class RegisterUserCommand : IRequest<UserResponse>
    {
        public UserRequest User { get; private set; }

        public RegisterUserCommand(UserRequest user)
        {
            User = user;
        }
    }
}

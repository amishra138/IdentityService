using IdentityService.Domain.Entities;
using IdentityService.Domain.Models;
using System.Collections.Generic;

namespace IdentityService.Domain.Contracts
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);

        UserResponse Register(UserRequest model);
        IEnumerable<User> GetAll();
    }
}

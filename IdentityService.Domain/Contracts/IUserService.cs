using IdentityService.Domain.Entities;
using IdentityService.Domain.Models;
using System.Collections.Generic;

namespace IdentityService.Domain.Contracts
{
    public interface IUserService
    {
        UserResponse Register(UserRequest model);
        IEnumerable<User> GetAll();
    }
}

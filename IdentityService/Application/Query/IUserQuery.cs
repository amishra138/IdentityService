using IdentityService.Domain.Entities;
using System.Collections.Generic;

namespace IdentityService.Application.Query
{
    public interface IUserQuery
    {
        IEnumerable<User> Get(string username);
    }
}

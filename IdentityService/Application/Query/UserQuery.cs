using IdentityService.Domain.Contracts;
using IdentityService.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace IdentityService.Application.Query
{
    public class UserQuery : IUserQuery
    {
        private readonly IUserService _userService;

        public UserQuery(IUserService userService)
        {
            _userService = userService;
        }

        public IEnumerable<User> Get(string username)
        {
            var result = _userService.GetAll().ToList();

            if (!string.IsNullOrEmpty(username))
            {
                return result.Where(x => x.Username == username);
            }
            else
                return result;
        }
    }
}

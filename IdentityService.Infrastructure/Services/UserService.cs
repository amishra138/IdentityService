using IdentityService.Domain.Contracts;
using IdentityService.Domain.Entities;
using IdentityService.Domain.Models;
using System.Collections.Generic;

namespace IdentityService.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private IdentityContext _context;

        public UserService(IdentityContext context)
        {
            _context = context;
        }

        public UserResponse Register(UserRequest model)
        {
            string _id = System.Guid.NewGuid().ToString();
            _context.Users.Add(new User() { Id = _id, Email = model.Email, FirstName = model.FirstName, LastName = model.LastName, Username = model.Username, Password = model.Password, UserType = model.UserType });
            _context.SaveChanges();
            return new UserResponse() { Id = _id, Name = model.FirstName + model.LastName };
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }
    }
}

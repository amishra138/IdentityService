using IdentityService.Domain.Contracts;
using IdentityService.Domain.Entities;
using IdentityService.Domain.Helpers;
using IdentityService.Domain.Models;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace IdentityService.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private IdentityContext _context;
        private readonly AppSettings _appSettings;


        public UserService(IOptions<AppSettings> appSettings, IdentityContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }

        public UserResponse Register(UserRequest model)
        {
            string _id = System.Guid.NewGuid().ToString();
            _context.Users.Add(new User() { Id = _id, Email = model.Email, FirstName = model.FirstName, LastName = model.LastName, Username = model.Username, Password = model.Password, UserType = model.UserType });
            _context.SaveChanges();
            return new UserResponse() { Id = _id, Name = model.FirstName + model.LastName };
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _context.Users.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = GenerateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }


        // helper methods
        private string GenerateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var key = System.Text.Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, $"{user.Username}"),
                    new Claim(ClaimTypes.GivenName, $"{user.FirstName} {user.LastName}"),
                    new Claim(ClaimTypes.Role,user.UserType.ToString()),
                    new Claim(ClaimTypes.Email,user.Email),
                }),
                Expires = System.DateTime.UtcNow.AddDays(7),
                SigningCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(key), Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}

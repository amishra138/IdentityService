using IdentityService.Application.Query;
using IdentityService.Domain.Entities;
using IdentityService.Domain.Helpers;
using IdentityService.Domain.Models;
using MediatR;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityService.Application.Commands
{
    public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, AuthenticateResponse>
    {
        private readonly AppSettings _appSettings;
        private readonly IUserQuery _userQuery;

        public AuthenticateUserCommandHandler(IOptions<AppSettings> appSettings, IUserQuery userQuery)
        {
            _appSettings = appSettings.Value;
            _userQuery = userQuery;

        }
        public async Task<AuthenticateResponse> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
        {
            var user = _userQuery.Get(request.AuthenticateRequest.Username);

            if (user == null || user.Count() == 0)
                return null;

            return new AuthenticateResponse(user.First(), GenerateJwtToken(user.First()));
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

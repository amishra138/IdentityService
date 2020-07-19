using IdentityService.Domain.Entities;

namespace IdentityService.Domain.Models
{
    public class AuthenticateResponse
    {
        public string Id { get; set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Username { get; private set; }
        public string Token { get; private set; }


        public AuthenticateResponse(User user, string token)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Username = user.Username;
            Token = token;
        }
    }
}

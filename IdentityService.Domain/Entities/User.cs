using IdentityService.Domain.Enums;
using System.Text.Json.Serialization;

namespace IdentityService.Domain.Entities
{
    public class User
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }

        public string Email { get; set; }

        public UserType UserType { get; set; }

        [JsonIgnore]
        public string Password { get; set; }
    }
}

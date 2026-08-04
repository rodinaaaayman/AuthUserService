using AuthUserServiceDomain.Enums;

namespace AuthUserServiceDomain.Models
{
    public class Users
    {

        public int Id { get; set; }
        public string? Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; } = string.Empty;
        public string? Address { get; set; } = string.Empty;
        public string? City { get; set; }
        public string NationalId { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public Roles Role { get; set; }
        //public string? RefreshToken { get; set; }
        //public DateTime RefreshTokenExpiryTime { get; set; }
    }
}

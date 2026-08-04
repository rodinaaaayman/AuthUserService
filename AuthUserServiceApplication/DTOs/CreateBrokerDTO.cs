using System.ComponentModel.DataAnnotations;
namespace AuthUserServiceApplication.DTOs
{
    public class CreateBrokerDTO
    {
        public string? Username { get; set; }
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; }

    }
}

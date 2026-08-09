using System.ComponentModel.DataAnnotations;
namespace AuthUserServiceApplication.DTOs
{
    public class CreateClientsDTO
    {
        [Required]
        [StringLength(50)]
        public string? Username { get; set; }
        [Required]
        [StringLength(200)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        [StringLength(50)]
        public string? FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string? LastName { get; set; }
        [StringLength(15)]
        public string PhoneNumber { get; set; } = string.Empty;
        public string? Address { get; set; } = string.Empty;
        public string? City { get; set; }
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        [StringLength(14, MinimumLength = 14)]
        public string NationalId { get; set; } = string.Empty;
        public decimal Deposit { get; set; }
    }
}

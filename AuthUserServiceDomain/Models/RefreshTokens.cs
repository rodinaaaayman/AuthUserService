using System.ComponentModel.DataAnnotations.Schema;

namespace AuthUserServiceDomain.Models
{
    public class RefreshTokens
    {
        [ForeignKey("Users")]
        public int Id { get; set; }
        public string? RefreshToken { get; set; }

        public DateTime RefreshTokenExpiryTime { get; set; }
        //public Users? User { get; set; }
    }
}

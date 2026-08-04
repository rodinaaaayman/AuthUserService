namespace AuthUserServiceApplication.DTOs
{
    public class ClientsDTO
    {
        public string? Username { get; set; }
        public int Id { get; set; }

        public string? Name { get; set; }

        public string Email { get; set; } = string.Empty;

        public string NationalID { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public decimal AccountBalance { get; set; }
    }
}

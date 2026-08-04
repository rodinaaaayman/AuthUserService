namespace AuthUserServiceDomain.Models
{
    public class Brokers : Users
    {
        public ICollection<Clients> Clients { get; set; } = new List<Clients>();

    }
}

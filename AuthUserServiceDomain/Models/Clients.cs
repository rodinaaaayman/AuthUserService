namespace AuthUserServiceDomain.Models
{
    public class Clients : Users
    {
        public Decimal AccountBalance { get; set; }
        public void Deposit(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Deposit must be positive.");

            AccountBalance += amount;
        }
        public void Withdraw(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Withdrawal must be positive.");

            if (AccountBalance < amount)
                throw new InvalidOperationException("Insufficient balance.");

            AccountBalance -= amount;
        }
        public bool IsActive { get; set; } = true;
        public ICollection<Brokers> Brokers { get; set; }
        = new List<Brokers>();
    }
}

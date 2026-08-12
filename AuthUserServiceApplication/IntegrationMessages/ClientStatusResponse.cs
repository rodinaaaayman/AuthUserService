namespace AuthUserServiceApplication.IntegrationMessages;
public class ClientStatusResponse
{
    public int Id { get; set; }
    public bool Exists { get; set; }
    public bool IsActive { get; set; }
    public decimal AccountBalance { get; set; }
    public bool FundsReserved { get; set; }   // true if balance was sufficient AND deducted
    public string? FailureReason { get; set; }
}
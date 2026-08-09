namespace AuthUserServiceApplication.IntegrationMessages;
public class ClientStatusResponse
{
    public int Id { get; set; }
    public bool Exists { get; set; }
    public bool IsActive { get; set; }
}
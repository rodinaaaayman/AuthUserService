public class PasswordHasher
{
    private const int WorkFactor = 12;

    public string Hash(string plainPassword)
    {
        return BCrypt.Net.BCrypt.HashPassword(plainPassword, workFactor: WorkFactor);
    }

    public bool Verify(string plainPassword, string HashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(plainPassword, HashedPassword);
    }
}
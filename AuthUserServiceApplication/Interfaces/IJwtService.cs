using AuthUserServiceDomain.Models;

namespace AuthUserServiceApplication.Interfaces
{
    public interface IJwtService
    {
        string GenerateAccessToken(Users User);

        string GenerateRefreshToken();
    }
}

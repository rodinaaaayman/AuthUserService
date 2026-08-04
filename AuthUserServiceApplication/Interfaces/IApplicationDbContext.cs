using AuthUserServiceDomain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;


namespace AuthUserServiceApplication.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Users> Users { get; }
        DbSet<Clients> Clients { get; }
        DatabaseFacade Database { get; }
        DbSet<RefreshTokens> RefreshTokens { get; }

        Task<int> SaveChangesAsync(
            CancellationToken cancellationToken);
    }
}

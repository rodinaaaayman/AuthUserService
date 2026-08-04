using Microsoft.EntityFrameworkCore;
using AuthUserServiceApplication.Interfaces;
using AuthUserServiceApplication.DTOs;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using AuthUserServiceApplication.Exceptions;

namespace AuthUserServiceApplication.Services.clients.Queries.GetClients
{

    public class GetClientsQueryHandler
        : IRequestHandler<GetClientsQuery, IEnumerable<ClientsDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMemoryCache _cache;
        private readonly ICurrentUserService _currentUser;

        public GetClientsQueryHandler(IApplicationDbContext context, IMemoryCache cache, ICurrentUserService currentUser)
        {
            _context = context;
            _cache = cache;
            _currentUser = currentUser;
        }

        public async Task<IEnumerable<ClientsDTO>> Handle(
            GetClientsQuery request,
            CancellationToken cancellationToken)
        {
            if (!_currentUser.IsAdmin )
            {
                throw new ForbiddenException();
            }

            var clients = await _context.Clients
                .Where(c => c.IsActive)
                .Select(c => new ClientsDTO
                {
                    Username = c.Username,
                    Id = c.Id,
                    Name = c.FirstName,
                    Email = c.Email,
                    NationalID = c.NationalId,
                    PhoneNumber = c.PhoneNumber,
                    AccountBalance = c.AccountBalance
                })
                .ToListAsync(cancellationToken);
            
            return clients;
        }
    }
}
using AuthUserServiceApplication.Interfaces;
using AuthUserServiceApplication.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace AuthUserServiceApplication.Services.clients.Queries.GetClients
{

    public class GetClientsQueryHandler
        : IRequestHandler<GetClientsQuery, IEnumerable<ClientsDTO>>
    {
        private readonly IApplicationDbContext _context;

        public GetClientsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<ClientsDTO>> Handle(
            GetClientsQuery request,
            CancellationToken cancellationToken)
        {
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

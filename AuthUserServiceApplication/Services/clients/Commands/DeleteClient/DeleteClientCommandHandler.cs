using AuthUserServiceApplication.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AuthUserServiceApplication.Services.clients.Commands.DeleteClient
{
    

    public class DeleteClientCommandHandler
        : IRequestHandler<DeleteClientCommand, bool>
    {
        private readonly IApplicationDbContext _context;

        public DeleteClientCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<bool> Handle(
            DeleteClientCommand request,
            CancellationToken cancellationToken)
        {
            var client = await _context.Clients
                .FirstOrDefaultAsync(
                    c => c.Id == request.Id,
                    cancellationToken);


            if (client == null)
            {
                return false;
            }


            // Soft delete
            client.IsActive = false;


            await _context.SaveChangesAsync(cancellationToken);


            return true;
        }
    }
}

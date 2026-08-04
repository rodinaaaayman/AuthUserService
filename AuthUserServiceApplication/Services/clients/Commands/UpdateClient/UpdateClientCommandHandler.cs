using AuthUserServiceApplication.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AuthUserServiceApplication.Services.clients.Commands.UpdateClient;



public class UpdateClientCommandHandler
    : IRequestHandler<UpdateClientCommand, bool>
{

    private readonly IApplicationDbContext _context;

    public UpdateClientCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<bool> Handle(
        UpdateClientCommand request,
        CancellationToken cancellationToken)
    {

        var client = await _context.Clients
            .FirstOrDefaultAsync(
                c => c.Id == request.Id && c.IsActive,
                cancellationToken);


        if (client == null)
        {
            return false;
        }


        client.FirstName = request.Client.FirstName;
        client.LastName = request.Client.LastName;
        client.PhoneNumber = request.Client.PhoneNumber;


        await _context.SaveChangesAsync(cancellationToken);


        return true;
    }
}

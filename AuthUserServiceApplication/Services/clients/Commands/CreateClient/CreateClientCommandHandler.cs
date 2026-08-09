using AuthUserService.Application.IntegrationEvents;
using AuthUserServiceApplication.Interfaces;
using AuthUserServiceDomain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AuthUserServiceApplication.Services.clients.Commands.CreateClient
{
    public class CreateClientCommandHandler
        : IRequestHandler<CreateClientCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IEventPublisher _eventPublisher;

        public CreateClientCommandHandler(IApplicationDbContext context, IEventPublisher eventPublisher)
        {
            _context = context;
            _eventPublisher = eventPublisher;
        }

        public async Task<int> Handle(
            CreateClientCommand request,
            CancellationToken cancellationToken)
        {
            if (await _context.Clients.AnyAsync(
                c => c.Email == request.Email,
                cancellationToken))
            {
                throw new InvalidOperationException("Email already exists.");
            }

            if (await _context.Clients.AnyAsync(
                c => c.NationalId == request.NationalId,
                cancellationToken))
            {
                throw new InvalidOperationException("National ID already exists.");
            }

            var client = new Clients
            {
                Username = request.Username,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                Address = request.Address,
                City = request.City,
                NationalId = request.NationalId,
                Password = request.Password,
                Role = AuthUserServiceDomain.Enums.Roles.Client
            };

            client.Deposit(request.Deposit);

            _context.Clients.Add(client);

            await _context.SaveChangesAsync(cancellationToken);

            // Publish only after SaveChangesAsync succeeds — client.Id is
            // populated by EF Core at this point (auto-increment), not before.
            await _eventPublisher.PublishAsync(new UserCreatedEvent
            {
                Id = client.Id,
                Email = client.Email,
                Role = client.Role.ToString(),
                CreatedAtUtc = DateTime.UtcNow
            }, routingKey: "user.created");

            return client.Id;
        }
    }
}
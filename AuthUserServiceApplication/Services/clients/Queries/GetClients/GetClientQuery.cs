using AuthUserServiceApplication.DTOs;
using MediatR;

namespace AuthUserServiceApplication.Services.clients.Queries.GetClients;

public record GetClientsQuery : IRequest<IEnumerable<ClientsDTO>>;



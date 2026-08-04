using AuthUserServiceApplication.DTOs;
using MediatR;
namespace AuthUserServiceApplication.Services.clients.Queries.GetClientById
{
    public record GetClientByIdQuery(int Id) : IRequest<ClientsDTO?>;
}

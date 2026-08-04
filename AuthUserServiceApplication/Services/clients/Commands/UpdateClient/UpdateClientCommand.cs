using AuthUserServiceApplication.DTOs;
using MediatR;

namespace AuthUserServiceApplication.Services.clients.Commands.UpdateClient;

public record UpdateClientCommand(
    int Id,
    UpdateClientDTO Client
) : IRequest<bool>;

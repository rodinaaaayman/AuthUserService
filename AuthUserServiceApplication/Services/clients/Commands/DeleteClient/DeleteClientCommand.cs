using MediatR;
namespace AuthUserServiceApplication.Services.clients.Commands.DeleteClient;


public record DeleteClientCommand(int Id) : IRequest<bool>;

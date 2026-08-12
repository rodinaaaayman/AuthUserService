using AuthUserServiceApplication.Services.clients.Commands.CreateClient;
using MediatR;

public record CreateClientCommand(
    string Username,
    string Email,
    string FirstName,
    string LastName,
    string PhoneNumber,
    string Address,
    string City,
    string NationalId,
    string Password,
    decimal Deposit) : IRequest<ClientResponse>;
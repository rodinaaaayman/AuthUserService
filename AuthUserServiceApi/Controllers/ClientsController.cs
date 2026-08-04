using AuthUserServiceApplication.DTOs;
using AuthUserServiceApplication.Services.clients.Commands.DeleteClient;
using AuthUserServiceApplication.Services.clients.Commands.UpdateClient;
using AuthUserServiceApplication.Services.clients.Queries.GetClientById;
using AuthUserServiceApplication.Services.clients.Queries.GetClients;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace AuthUserServiceApi.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClientsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Clients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientsDTO>>> GetClients()
        {
            var clients = await _mediator.Send(new GetClientsQuery());

            return Ok(clients);
        }

        // GET: api/Clients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClientsDTO>> GetClient(int id)
        {
            var client = await _mediator.Send(new GetClientByIdQuery(id));


            if (client == null)
            {
                return NotFound();
            }


            return Ok(client);
        }

        // PUT: api/Clients/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClient(
     int id,
     UpdateClientDTO dto)
        {

            var result = await _mediator.Send(
                new UpdateClientCommand(id, dto));


            if (!result)
            {
                return NotFound();
            }


            return NoContent();
        }

        // POST: api/Clients
        [HttpPost]
        public async Task<IActionResult> PostClients(CreateClientsDTO dto)
        {
            var command = new CreateClientCommand(
                dto.Username!,
                dto.Email,
                dto.FirstName!,
                dto.LastName!,
                dto.Address!,
                dto.City!, 
                dto.Password,
                dto.NationalId,
                dto.PhoneNumber,
                dto.Deposit);

            var id = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetClient), new { id }, id);
        }
        // DELETE: api/Clients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var result = await _mediator.Send(
                new DeleteClientCommand(id));


            if (!result)
            {
                return NotFound();
            }


            return NoContent();
        }
        ////Get Client's Orders
        //[HttpGet("{clientId}/orders")]
        //public async Task<IActionResult> GetClientOrders(int clientId)
        //{
        //    var result = await _mediator.Send(
        //        new GetClientOrdersQuery(clientId)
        //    );

        //    return Ok(result);
        //}
    }
}

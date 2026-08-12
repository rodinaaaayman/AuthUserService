using AuthUserServiceApplication.DTOs;
using AuthUserServiceApplication.Services.clients.Commands.CreateClient;
using AuthUserServiceApplication.Services.clients.Commands.DeleteClient;
using AuthUserServiceApplication.Services.clients.Commands.UpdateClient;
using AuthUserServiceApplication.Services.clients.Queries.GetClientById;
using AuthUserServiceApplication.Services.clients.Queries.GetClients;
using MediatR;
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
        public async Task<ActionResult<ClientResponse>> GetClientById(int id)
        {
            var result = await _mediator.Send(new GetClientByIdQuery(id));
            if (result == null) return NotFound();
            return Ok(result);
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
        [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ClientResponse>> CreateClient(CreateClientCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetClientById), new { id = result.Id }, result);
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
        //[HttpGet("{Id}/orders")]
        //public async Task<IActionResult> GetClientOrders(int Id)
        //{
        //    var result = await _mediator.Send(
        //        new GetClientOrdersQuery(Id)
        //    );

        //    return Ok(result);
        //}
    }
}

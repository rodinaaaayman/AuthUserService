using AuthUserServiceInfrastructure.Data;
using AuthUserServiceApplication.DTOs;
using AuthUserServiceDomain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AuthUserServiceDomain.Enums;

namespace AuthUserServiceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrokersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BrokersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Brokers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Brokers>>> GetBrokers()
        {
            return await _context.Brokers.ToListAsync();
        }

        // GET: api/Brokers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Brokers>> GetBrokers(int id)
        {
            var brokers = await _context.Brokers.FindAsync(id);

            if (brokers == null)
            {
                return NotFound();
            }

            return brokers;
        }

        // PUT: api/Brokers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBrokers(int id, Brokers brokers)
        {
            if (id != brokers.Id)
            {
                return BadRequest();
            }

            _context.Entry(brokers).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BrokersExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Brokers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Brokers>> PostBrokers(CreateBrokerDTO dto)
        {
            var Broker = new Brokers
            {
                Username = dto.Username,
                Email = dto.Email,
                Password = dto.Password,
                Role = Roles.Broker
            };
            _context.Brokers.Add(Broker);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAdmins", new { id = Broker.Id }, Broker);
        }


        // DELETE: api/Brokers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrokers(int id)
        {
            var brokers = await _context.Brokers.FindAsync(id);
            if (brokers == null)
            {
                return NotFound();
            }

            _context.Brokers.Remove(brokers);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BrokersExists(int id)
        {
            return _context.Brokers.Any(e => e.Id == id);
        }
    }
}

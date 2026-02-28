using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ticket_Management.Entity;
using Ticket_Management.Entity.Model;
using Ticket_Management.Repositories;

namespace Ticket_Management.Controllers
{
    [Authorize] // 🔐 All APIs require JWT by default
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketRepository _repository;
        public TicketsController(ITicketRepository repository)
        {
            _repository = repository;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TicketRequest ticket)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            string created = await _repository.AddAsync(ticket);
            return Ok(created);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] TicketRequest ticketRequest)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            string updated = await _repository.UpdateAsync(ticketRequest);
            return Ok(updated);
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string status = null)
        {
            IEnumerable<Ticket> tickets = await _repository.GetAllAsync(status);
            return Ok(tickets);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Ticket? ticket = await _repository.GetByIdAsync(id);
            if (ticket == null) return NotFound();
            return Ok(ticket);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Ticket? existing = await _repository.GetByIdAsync(id);
            if (existing == null) return NotFound();
            await _repository.DeleteAsync(id);
            return NoContent();
        }
        [HttpGet("status/{statusCode:int}")]
        public async Task<IActionResult> GetByStatus(int statusCode)
        {
            IEnumerable<Ticket> tickets = await _repository.GetByStatusCodeAsync(statusCode);

            if (!tickets.Any())
                return NotFound("No tickets found for given status code.");
            return Ok(tickets);
        }
        [HttpGet("search")]
        public async Task<IActionResult> Search(string? assignTo, int? status)
        {
            var tickets = await _repository.GetTicketsAsync(assignTo, status);
            return Ok(tickets);
        }
    }
}

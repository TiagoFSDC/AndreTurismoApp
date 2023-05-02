using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AndreTurismoApp.Models;
using AndreTurismoApp.TicketService.Data;
using AndreTurismoApp.AddressService.Services;
using AndreTurismoApp.Models.DTO;
using System.Net;

namespace AndreTurismoApp.TicketService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly AndreTurismoAppTicketServiceContext _context;

        public TicketsController(AndreTurismoAppTicketServiceContext context)
        {
            _context = context;
        }

        // GET: api/Tickets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTicket()
        {
            if (_context.Ticket == null)
            {
                return NotFound();
            }
            return await _context.Ticket.Include(t=> t.client.address.city).Include(t=> t.Start.city).Include(t=>t.Destination.city).ToListAsync();
        }

        // GET: api/Tickets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ticket>> GetTicket(int id)
        {
            if (_context.Ticket == null)
            {
                return NotFound();
            }
            var ticket = await _context.Ticket.FindAsync(id);

            if (ticket == null)
            {
                return NotFound();
            }

            return ticket;
        }

        // PUT: api/Tickets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTicket(int id, Ticket ticket)
        {
            if (id != ticket.Id)
            {
                return BadRequest();
            }

            _context.Entry(ticket).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketExists(id))
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

        // POST: api/Tickets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Ticket>> PostTicket(Ticket ticket)
        {
            if (_context.Ticket == null)
            {
                return Problem("Entity set 'AndreTurismoAppTicketServiceContext.Ticket'  is null.");
            }
            AddressDTO addreesDto = PostOfficeServices.GetAddress(ticket.Start.ZipCode).Result;
            Address addressComplete = new Address(addreesDto);
            addressComplete.Number = ticket.Start.Number;

            AddressDTO addreesDto2 = PostOfficeServices.GetAddress(ticket.Destination.ZipCode).Result;
            Address addressComplete2 = new Address(addreesDto2);
            addressComplete2.Number = ticket.Destination.Number;

            AddressDTO addreesDto3 = PostOfficeServices.GetAddress(ticket.client.address.ZipCode).Result;
            Address addressComplete3 = new Address(addreesDto3);
            addressComplete3.Number = ticket.client.address.Number;

            ticket.client.address = addressComplete3;
            ticket.Destination = addressComplete2;
            ticket.Start = addressComplete;
            

            _context.Ticket.Add(ticket);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTicket", new { id = ticket.Id }, ticket);
        }

        // DELETE: api/Tickets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            if (_context.Ticket == null)
            {
                return NotFound();
            }
            var ticket = await _context.Ticket.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }

            _context.Ticket.Remove(ticket);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TicketExists(int id)
        {
            return (_context.Ticket?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

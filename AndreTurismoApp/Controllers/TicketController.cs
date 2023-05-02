using AndreTurismoApp.Models.DTO;
using AndreTurismoApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AndreTurismoApp.Services;
using System.Net;

namespace AndreTurismoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly TicketService _ticketService;
        public TicketController(TicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpGet]
        public async Task<List<Ticket>> GetTicket()
        {
            return await _ticketService.GetTicket();
        }

        [HttpGet("{id}")]
        public async Task<Ticket> GetTicketById(int id)
        {
            return await _ticketService.GetTicketById(id);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTicket(int id)
        {
            HttpStatusCode code = await _ticketService.DeleteTicket(id);
            return StatusCode((int)code);
        }

        [HttpPost]
        public async Task<Ticket> PostTicket(Ticket Ticket)
        {
            return await _ticketService.PostTicket(Ticket);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutTicket(int id, Ticket a)
        {
            HttpStatusCode code = await _ticketService.PutTicket(id, a);
            return StatusCode((int)code);
        }
    }
}


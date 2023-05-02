using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AndreTurismoApp.Models;
using AndreTurismoApp.PacketService.Data;
using AndreTurismoApp.Models.DTO;
using System.Net.Sockets;
using AndreTurismoApp.AddressService.Services;

namespace AndreTurismoApp.PacketService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacketsController : ControllerBase
    {
        private readonly AndreTurismoAppPacketServiceContext _context;

        public PacketsController(AndreTurismoAppPacketServiceContext context)
        {
            _context = context;
        }

        // GET: api/Packets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Packet>>> GetPacket()
        {
            if (_context.Packet == null)
            {
                return NotFound();
            }
            return await _context.Packet.Include(p => p.client.address.city).Include(p => p.ticket.Start.city).Include(p => p.ticket.Destination.city).Include(p => p.hotel.address.city).Include(p => p.ticket.client.address.city).Include(p => p.hotel.client.address.city).ToListAsync();
        }

        // GET: api/Packets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Packet>> GetPacket(int id)
        {
            if (_context.Packet == null)
            {
                return NotFound();
            }
            var packet = await _context.Packet.FindAsync(id);

            if (packet == null)
            {
                return NotFound();
            }
            return packet;
        }

        // PUT: api/Packets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPacket(int id, Packet packet)
        {
            if (id != packet.Id)
            {
                return BadRequest();
            }

            _context.Entry(packet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PacketExists(id))
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

        // POST: api/Packets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Packet>> PostPacket(Packet packet)
        {
            if (_context.Packet == null)
            {
                return Problem("Entity set 'AndreTurismoAppPacketServiceContext.Packet'  is null.");
            }

            AddressDTO addreesDto = PostOfficeServices.GetAddress(packet.ticket.Start.ZipCode).Result;
            Address addressComplete = new Address(addreesDto);
            addressComplete.Number = packet.ticket.Start.Number;

            AddressDTO addreesDto2 = PostOfficeServices.GetAddress(packet.ticket.Destination.ZipCode).Result;
            Address addressComplete2 = new Address(addreesDto2);
            addressComplete2.Number = packet.ticket.Destination.Number;

            AddressDTO addreesDto3 = PostOfficeServices.GetAddress(packet.ticket.client.address.ZipCode).Result;
            Address addressComplete3 = new Address(addreesDto3);
            addressComplete3.Number = packet.ticket.client.address.Number;

            AddressDTO addreesDto4 = PostOfficeServices.GetAddress(packet.hotel.address.ZipCode).Result;
            Address addressComplete4 = new Address(addreesDto4);
            addressComplete4.Number = packet.hotel.address.Number;

            AddressDTO addreesDto5 = PostOfficeServices.GetAddress(packet.hotel.client.address.ZipCode).Result;
            Address addressComplete5 = new Address(addreesDto5);
            addressComplete5.Number = packet.hotel.client.address.Number;

            AddressDTO addreesDto6 = PostOfficeServices.GetAddress(packet.client.address.ZipCode).Result;
            Address addressComplete6 = new Address(addreesDto6);
            addressComplete6.Number = packet.hotel.client.address.Number;

            packet.hotel.client.address = addressComplete5;
            packet.hotel.address = addressComplete4;
            packet.ticket.client.address = addressComplete3;
            packet.ticket.Destination = addressComplete2;
            packet.ticket.Start = addressComplete;
            packet.client.address = addressComplete6;

            _context.Packet.Add(packet);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPacket", new { id = packet.Id }, packet);
        }

        // DELETE: api/Packets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePacket(int id)
        {
            if (_context.Packet == null)
            {
                return NotFound();
            }
            var packet = await _context.Packet.FindAsync(id);
            if (packet == null)
            {
                return NotFound();
            }

            _context.Packet.Remove(packet);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PacketExists(int id)
        {
            return (_context.Packet?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

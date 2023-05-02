using AndreTurismoApp.Models;
using AndreTurismoApp.Services;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AndreTurismoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacketController : ControllerBase
    {
        private readonly PacketService _PacketService;
        public PacketController(PacketService packetService)
        {
            _PacketService = packetService;
        }

        [HttpGet]
        public async Task<List<Packet>> GetPacket()
        {
            return await _PacketService.GetPacket();
        }

        [HttpGet("{id}")]
        public async Task<Packet> GetPacketById(int id)
        {
            return await _PacketService.GetPacketById(id);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePacket(int id)
        {
            HttpStatusCode code = await _PacketService.DeletePacket(id);
            return StatusCode((int)code);
        }

        [HttpPost]
        public async Task<Packet> PostPacket(Packet packet)
        {
            return await _PacketService.PostPacket(packet);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutPacket(int id, Packet p)
        {
            HttpStatusCode code = await _PacketService.PutPacket(id, p);
            return StatusCode((int)code);
        }
    }
}

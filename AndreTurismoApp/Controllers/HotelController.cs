using AndreTurismoApp.Models;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AndreTurismoApp.Services;

namespace AndreTurismoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly HotelService _HotelService;
        public HotelController(HotelService HotelService)
        {
            _HotelService = HotelService;
        }

        [HttpGet]
        public async Task<List<Hotel>> GetHotel()
        {
            return await _HotelService.GetHotel();
        }

        [HttpGet("{id}")]
        public async Task<Hotel> GetHotelById(int id)
        {
            return await _HotelService.GetHotelById(id);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteHotel(int id)
        {
            HttpStatusCode code = await _HotelService.DeleteHotel(id);
            return StatusCode((int)code);
        }

        [HttpPost]
        public async Task<Hotel> PostHotel(Hotel hotel)
        {
            return await _HotelService.PostHotel(hotel);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutHotel(int id, Hotel a)
        {
            HttpStatusCode code = await _HotelService.PutHotel(id, a);
            return StatusCode((int)code);
        }
    }
}

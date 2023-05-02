using System.Net;
using AndreTurismoApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AndreTurismoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly Services.AddressService _addressService;
        public AddressController(Services.AddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpGet]
        public async Task<List<Address>> GetAddress()
        {
             return await _addressService.GetAddress();
        }

        [HttpGet("{id}")]
        public async Task<Address> GetAddressById(int id)
        {
            return await _addressService.GetAddressById(id);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAddress(int id) 
        {
            HttpStatusCode code = await _addressService.DeleteAddress(id);
            return StatusCode((int)code);
        }

        [HttpPost]
        public async Task<Address> PostAddress(Address a)
        {
            return await _addressService.PostAddress(a);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutAddress(int id, Address a)
        {
            HttpStatusCode code = await _addressService.PutAddress(id, a);
            return StatusCode((int)code);
        }
    }
}

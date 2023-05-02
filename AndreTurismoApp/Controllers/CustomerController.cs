using AndreTurismoApp.Models;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AndreTurismoApp.Services;

namespace AndreTurismoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _clientService;
        public CustomerController(CustomerService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public async Task<List<Client>> GetClient()
        {
            return await _clientService.GetClient();
        }

        [HttpGet("{id}")]
        public async Task<Client> GetClientById(int id)
        {
            return await _clientService.GetClientById(id);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteClient(int id)
        {
            HttpStatusCode code = await _clientService.DeleteClient(id);
            return StatusCode((int)code);
        }

        [HttpPost]
        public async Task<Client> PostClient(Client a)
        {
            return await _clientService.PostClient(a);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutClient(int id, Client a)
        {
            HttpStatusCode code = await _clientService.PutClient(id, a);
            return StatusCode((int)code);
        }
    }
}

using AndreTurismoApp.Models;
using AndreTurismoApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace AndreTurismoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private AddressServices _addressService;

        public AddressController(AddressServices addressService)
        {
            _addressService = new AddressServices();
        }

        [HttpPost]
        public bool Add(Address address)
        {
            return _addressService.InsertDapper(address);
        }

        [HttpGet]
        public List<Address> GetAll()
        {
            return _addressService.GetAllDapper();
        }

        [HttpPut]
        public bool Update(Address address)
        {

            return _addressService.UpdateDapper(address);
        }

        [HttpDelete]
        public bool Delete(int id)
        {
            return _addressService.DeleteDapper(id);
        }
    }
}


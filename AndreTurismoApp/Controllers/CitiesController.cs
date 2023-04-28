using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AndreTurismoApp.Data;
using AndreTurismoApp.Models;
using AndreTurismoApp.Services;

namespace AndreTurismoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        [Route("api/[controller]")]
        [ApiController]
        public class CityController : ControllerBase
        {
            private CityServices _cityService;
            public CityController()
            {
                _cityService = new CityServices();
            }

            [HttpPost(Name = "InsertCity")]
            public bool Add(City city)
            {
                return _cityService.InsertDapper(city);
            }

            [HttpGet(Name = "GetAllCity")]
            public List<City> GetAll()
            {
                return _cityService.GetAllDapper();
            }

            [HttpPut(Name = "UpdateCity")]
            public bool Update(City city)
            {
                return _cityService.UpdateDapper(city);
            }

            [HttpDelete(Name = "DeleteCity")]
            public bool Delete(int id)
            {
                return _cityService.DeleteDapper(id);
            }
        }
    }
}

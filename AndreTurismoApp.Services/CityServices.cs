using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndreTurismoApp.Models;
using AndreTurismoApp.Repositories;
using static System.Formats.Asn1.AsnWriter;

namespace AndreTurismoApp.Services
{
    internal class CityServices
    {
        readonly string strconn = @"Server=(localdb)\MSSQLLocalDB;Integrated Security=true;AttachDbFileName=C:\dev-aula\AgenciaTurismo\AgenciaTurismo\docs\Scripts\AgenciaTurismo.mdf";
        readonly SqlConnection Conn;

        private ICityRepository touragecyRepository;

        public CityServices()
        {
            touragecyRepository = new CityRepository();
        }

        public bool InsertDapper(City city)
        {
            return touragecyRepository.InsertDapper(city);
        }

        public List<City> GetAllDapper()
        {
            return touragecyRepository.GetAllDapper();
        }

        public bool UpdateDapper(City city)
        {
            return touragecyRepository.UpdateDapper(city);
        }

        public bool DeleteDapper(City city)
        {
            return touragecyRepository.DeleteDapper(city);
        }
    }
}

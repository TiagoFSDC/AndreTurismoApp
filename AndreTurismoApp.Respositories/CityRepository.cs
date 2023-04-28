using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndreTurismoApp.Models;
using Dapper;
using static System.Formats.Asn1.AsnWriter;

namespace AndreTurismoApp.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly string _strConn = @"Server=(localdb)\MSSQLLocalDB;Integrated Security=true;AttachDbFileName=C:\dev-aula\AgenciaTurismo\AgenciaTurismo\docs\Scripts\AgenciaTurismo.mdf";

        public bool DeleteDapper(int Id)
        {
            var status = false;
            using (var db = new SqlConnection(_strConn))
            {
                db.Open();
                db.Execute(City.DELETE, new { @Id = Id});
                status = true;
            }

            return status;
        }

        public List<City> GetAllDapper()
        {
            List<City> citylist = new();

            using (var db = new SqlConnection(_strConn))
            {
                var cities = db.Query<City>(City.GETALL);
                citylist = (List<City>)cities;
            }
            return citylist;
        }

        public bool InsertDapper(City city)
        {
            var status = false;
            using (var db = new SqlConnection(_strConn))
            {
                db.Open();
                db.Execute(City.INSERT, city);
                status = true;
            }
            return status;
        }

        public bool UpdateDapper(City city)
        {
            var status = false;
            using (var db = new SqlConnection(_strConn))
            {
                db.Open();
                db.Execute(City.UPDATE, city);
                status = true;
            }

            return status;
        }
    }
}

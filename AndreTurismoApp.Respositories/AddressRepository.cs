using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndreTurismoApp.Models;
using System.Data.SqlClient;
using Dapper;

namespace AndreTurismoApp.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly string _strConn = @"Server=(localdb)\MSSQLLocalDB;Integrated Security=true;AttachDbFileName=C:\dev-aula\AgenciaTurismo\AgenciaTurismo\docs\Scripts\AgenciaTurismo.mdf";

        public bool DeleteDapper(Address address)
        {
            var status = false;
            using (var db = new SqlConnection(_strConn))
            {
                db.Open();
                db.Execute(Address.DELETE, address);
                status = true;
            }
            return status;
        }

        public List<Address> GetAllDapper()
        {
            List<Address> addresslist = new();

            using (var db = new SqlConnection(_strConn))
            {
                var address = db.Query<Address, City, Address>(Address.GETALL,(a,c) =>
                {
                    a.city = c;

                    return a;
                });
                addresslist = (List<Address>)address;
            }
            return addresslist;
        }

        public bool InsertDapper(Address address)
        {
            var status = false;
            using (var db = new SqlConnection(_strConn))
            {
                db.Open();
                db.ExecuteScalar(Address.INSERT, new { @Street = address.Street, 
                                        @Number = address.Number, @District = address.District, @ZipCode = address.ZipCode, 
                                        @Complement = address.Complement, @IdCidade = address.city.Id});
                status = true;
            }
            return status;
        }

        public bool UpdateDapper(Address address)
        {
            var status = false;
            using (var db = new SqlConnection(_strConn))
            {
                db.Open();
                db.Execute(Address.UPDATE, address);
                status = true;
            }
            return status;
        }
    }
}

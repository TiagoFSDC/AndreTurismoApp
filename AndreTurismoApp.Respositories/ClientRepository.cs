using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AndreTurismoApp.Models;
using Dapper;

namespace AndreTurismoApp.Repositories
{
    public class ClientRepository : IClientRepository
    {

        private readonly string _strConn = @"Server=(localdb)\MSSQLLocalDB;Integrated Security=true;AttachDbFileName=C:\dev-aula\AgenciaTurismo\AgenciaTurismo\docs\Scripts\AgenciaTurismo.mdf";
        
        public bool DeleteDapper(int Id)
        {
            var status = false;
            using (var db = new SqlConnection(_strConn))
            {
                db.Open();
                db.Execute(Client.DELETE, new { Id = Id});
                status = true;
            }
            return status;
        }

        public List<Client> GetAllDapper()
        {
            List<Client> clientlist = new();

            using (var db = new SqlConnection(_strConn))
            {
                var address = db.Query<Client ,Address, City, Client>(Client.GETALL, (client,address, city) =>
                {
                    address.city = city;
                    client.address = address;

                    return client;
                });
                clientlist = (List<Client>)address;
            }
            return clientlist; 
        }

        public bool InsertDapper(Client client)
        {
            var status = false;
            using (var db = new SqlConnection(_strConn))
            {
                db.Open();
                db.ExecuteScalar(Address.INSERT, new
                {
                    @Name = client.Name,
                    @Phone = client.Phone,
                    @IdEndereco = client.address.Id
                });
                status = true;
            }
            return status; 
        }

        public bool UpdateDapper(Client client)
        {
            var status = false;
            using (var db = new SqlConnection(_strConn))
            {
                db.Open();
                db.Execute(Client.UPDATE, client);
            };
            return status;
        }
    }
}

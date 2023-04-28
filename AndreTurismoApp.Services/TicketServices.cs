using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AndreTurismoApp.Models;

namespace AndreTurismoApp.Services
{
    public class TicketServices
    {
        readonly string strconn = @"Server=(localdb)\MSSQLLocalDB;Integrated Security=true;AttachDbFileName=C:\dev-aula\AgenciaTurismo\AgenciaTurismo\docs\Scripts\AgenciaTurismo.mdf";
        readonly SqlConnection Conn;

        public TicketServices()
        {
            Conn = new SqlConnection(strconn);
            Conn.Open();
        }

        public bool Insert(Ticket ticket)
        {
            bool status = false;

            try
            {
                string strInsert = "insert into Ticket (StartId, DestinationId,ClientId, Price) " +
                    "values (@StartId, @DestinationId, @ClientId, @Price)";

                SqlCommand commandInsert = new SqlCommand(strInsert, Conn);

                commandInsert.Parameters.Add(new SqlParameter("@StartId", InsertAddressS(ticket)));
                commandInsert.Parameters.Add(new SqlParameter("@DestinationId", InsertAddressD(ticket)));
                commandInsert.Parameters.Add(new SqlParameter("@ClientId", InsertClient(ticket)));
                commandInsert.Parameters.Add(new SqlParameter("@Price", ticket.Price));

                commandInsert.ExecuteNonQuery();
                status = true;
            }
            catch
            {
                status = false;
                throw;
            }
            finally
            {
                Conn.Close();
            }
            return status;
        }

        private int InsertClient(Ticket ticket)
        {
            string strInsert = "insert into Client (Name, Phone, IdEndereco) "
                + "values (@Name, @Phone, @IdEndereco); select cast(scope_identity() as int)";

            SqlCommand commandInsert = new SqlCommand(strInsert, Conn);

            commandInsert.Parameters.Add(new SqlParameter("@Name", ticket.client.Name));
            commandInsert.Parameters.Add(new SqlParameter("@Phone", ticket.client.Phone));
            commandInsert.Parameters.Add(new SqlParameter("@IdEndereco", InsertAddressS(ticket)));

            return (int)commandInsert.ExecuteScalar();
        }

        private int InsertAddressS(Ticket ticket)
        {
            string strInsert = "insert into Endereco (Logradouro, Numero, Bairro, CEP, Complemento, IdCidade) " +
            "values (@Logradouro, @Numero, @Bairro, @CEP, @Complemento, @IdCidade); select cast(scope_identity() as int)";

            SqlCommand commandInsert = new SqlCommand(strInsert, Conn);

            commandInsert.Parameters.Add(new SqlParameter("@Logradouro", ticket.Start.Street));
            commandInsert.Parameters.Add(new SqlParameter("@Numero", ticket.Start.Number));
            commandInsert.Parameters.Add(new SqlParameter("@Bairro", ticket.Start.District));
            commandInsert.Parameters.Add(new SqlParameter("@CEP", ticket.Start.ZipCode));
            commandInsert.Parameters.Add(new SqlParameter("@Complemento", ticket.Start.Complement));
            commandInsert.Parameters.Add(new SqlParameter("@IdCidade", InsertCity(ticket.Start)));

            return (int)commandInsert.ExecuteScalar();
        }

        private int InsertAddressD(Ticket ticket)
        {
            string strInsert = "insert into Endereco (Logradouro, Numero, Bairro, CEP, Complemento, IdCidade) " +
            "values (@Logradouro, @Numero, @Bairro, @CEP, @Complemento, @IdCidade); select cast(scope_identity() as int)";

            SqlCommand commandInsert = new SqlCommand(strInsert, Conn);

            commandInsert.Parameters.Add(new SqlParameter("@Logradouro", ticket.Destination.Street));
            commandInsert.Parameters.Add(new SqlParameter("@Numero", ticket.Destination.Number));
            commandInsert.Parameters.Add(new SqlParameter("@Bairro", ticket.Destination.District));
            commandInsert.Parameters.Add(new SqlParameter("@CEP", ticket.Destination.ZipCode));
            commandInsert.Parameters.Add(new SqlParameter("@Complemento", ticket.Destination.Complement));
            commandInsert.Parameters.Add(new SqlParameter("@IdCidade", InsertCity(ticket.Destination)));

            return (int)commandInsert.ExecuteScalar();
        }
        private int InsertCity(Address address)
        {
            string strInsert = "insert into Cidade (Descricao) values (@Descricao); select cast(scope_identity() as int)";
            SqlCommand commandInsert = new SqlCommand(strInsert, Conn);
            commandInsert.Parameters.Add(new SqlParameter("@Descricao", address.city.Description));

            return (int)commandInsert.ExecuteScalar();
        }

        public List<Ticket> FindAll()
        {
            List<Ticket> ticketlist = new();
            StringBuilder sb = new StringBuilder();

            sb.Append(@"select t.Id as idticket, t.Price, t.RegisterDate, c.Id as idclient,  
                 c.Name, c.Phone, c.RegisterDate, e.Id as idendereco, e.Logradouro as cliente,
                 e.Numero, e.Bairro, e.CEP, e.Complemento, e.DtCadastro, cAd.Id as idcity3, cAd.Descricao,
                 cAd.DtCadastro as data,
                 tST.Id as idstart, tST.Logradouro as startE,
                 tST.Numero as numberstart, tST.Bairro, tST.CEP, tST.Complemento, tST.DtCadastro as datatst, tDT.Id as iddestination, tDT.Logradouro as destination,
                 tDT.Numero, tDT.Bairro, tDT.CEP, tDT.Complemento, tDT.DtCadastro as datatdt,
                 cid.Id as idcity1, cid.Descricao, cid.DtCadastro as data1,
                 cid2.Id as idcity2, cid2.Descricao, cid2.DtCadastro as data2
                 from Ticket t
                 Join Endereco tST ON t.StartId = tST.Id
                 Join Cidade as cid ON tst.IdCidade = cid.Id
                 Join Endereco tDT ON t.DestinationId = tDT.Id
                 Join Cidade as cid2 ON t.DestinationId = cid2.Id
                 Join Client as c ON t.ClientId = c.Id
                 Join Endereco e On c.IdEndereco = e.Id
                 Join Cidade as cAd ON cAd.Id = e.IdCidade");

            SqlCommand commandSelect = new(sb.ToString(), Conn);
            SqlDataReader dr = commandSelect.ExecuteReader();

            while (dr.Read())
            {
                Ticket ticket = new();

                ticket.Id = (int)dr["Idticket"];
                ticket.Price = (decimal)dr["Price"];
                ticket.Start = new Address()
                {
                    Id = (int)dr["Idstart"],
                    Street = (string)dr["startE"],
                    Number = (int)dr["numberstart"],
                    District = (string)dr["Bairro"],
                    ZipCode = (string)dr["CEP"],
                    Complement = (string)dr["Complemento"],
                    city = new City()
                    {
                        Id = (int)dr["Idcity1"],
                        Description = (string)dr["Descricao"],
                        RegisterDate = (DateTime)dr["data1"]
                    },
                    RegisterDate = (DateTime)dr["datatst"]
                };

                ticket.Destination = new Address()
                {
                    Id = (int)dr["Iddestination"],
                    Street = (string)dr["destination"],
                    Number = (int)dr["Numero"],
                    District = (string)dr["Bairro"],
                    ZipCode = (string)dr["CEP"],
                    Complement = (string)dr["Complemento"],
                    city = new City()
                    {
                        Id = (int)dr["Idcity2"],
                        Description = (string)dr["Descricao"],
                        RegisterDate = (DateTime)dr["data2"]
                    },
                    RegisterDate = (DateTime)dr["datatdt"]
                };

                ticket.client = new Client()
                {
                    Id = (int)dr["Idclient"],
                    Name = (string)dr["Name"],
                    Phone = (string)dr["Phone"],
                    address = new Address()
                    {
                        Id = (int)dr["Idendereco"],
                        Street = (string)dr["cliente"],
                        Number = (int)dr["Numero"],
                        District = (string)dr["Bairro"],
                        ZipCode = (string)dr["CEP"],
                        Complement = (string)dr["Complemento"],
                        city = new City()
                        {
                            Id = (int)dr["Idcity3"],
                            Description = (string)dr["Descricao"],
                            RegisterDate = (DateTime)dr["data"]
                        },
                        RegisterDate = (DateTime)dr["DTCadastro"]
                    }
                };
                ticket.Date = (DateTime)dr["RegisterDate"];
                ticketlist.Add(ticket);
            }
            return ticketlist;
        }

        public bool Update(int id, double price)
        {
            bool status = false;

            try
            {
                string strUpdate = "update Ticket Set Price = " + "'" + price + "' where Id = " + id;

                SqlCommand commandUpdate = new SqlCommand(strUpdate, Conn);

                commandUpdate.ExecuteNonQuery();
                status = true;
            }
            catch
            {
                status = false;
                throw;
            }
            finally
            {
                Conn.Close();
            }
            return status;
        }

        public bool Delete(int id)
        {
            bool status = false;

            try
            {
                string strDelete = $"Delete from Ticket where Id = {id}";

                SqlCommand commandDelete = new SqlCommand(strDelete, Conn);

                commandDelete.ExecuteNonQuery();
                status = true;
            }
            catch
            {
                status = false;
                throw;
            }
            finally
            {
                Conn.Close();
            }
            return status;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using AndreTurismoApp.Models;

namespace AndreTurismoApp.Services
{
    public class PacketServices
    {
        readonly string strconn = @"Server=(localdb)\MSSQLLocalDB;Integrated Security=true;AttachDbFileName=C:\dev-aula\AgenciaTurismo\AgenciaTurismo\docs\Scripts\AgenciaTurismo.mdf";
        readonly SqlConnection Conn;

        public PacketServices()
        {
            Conn = new SqlConnection(strconn);
            Conn.Open();
        }

        public bool Insert(Packet packet)
        {
            bool status = false;

            try
            {
                string strInsert = "insert into Packet (HotelId,TicketId,Price,ClientId) " +
                    "values (@HotelId, @TicketId,@Price,@ClientId)";

                SqlCommand commandInsert = new SqlCommand(strInsert, Conn);

                commandInsert.Parameters.Add(new SqlParameter("@HotelId", InsertHotel(packet)));
                commandInsert.Parameters.Add(new SqlParameter("@TicketId", InsertTicket(packet)));
                commandInsert.Parameters.Add(new SqlParameter("@Price", packet.Price));
                commandInsert.Parameters.Add(new SqlParameter("@ClientId", InsertClient(packet)));

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

        private int InsertHotel(Packet packet)
        {
            string strInsert = "insert into Hotel (Name, IdAddress, Price) " +
                "values (@Name, @IdAddress, @Price); select cast(scope_identity() as int)";

            SqlCommand commandInsert = new SqlCommand(strInsert, Conn);

            commandInsert.Parameters.Add(new SqlParameter("@Name", packet.hotel.Name));
            commandInsert.Parameters.Add(new SqlParameter("@IdAddress", InsertAddress(packet)));
            commandInsert.Parameters.Add(new SqlParameter("@Price", packet.hotel.Price));

            return (int)commandInsert.ExecuteScalar();
        }

        private int InsertAddress(Packet packet)
        {
            string strInsert = "insert into Endereco (Logradouro, Numero, Bairro, CEP, Complemento, IdCidade) " +
                "values (@Logradouro, @Numero, @Bairro, @CEP, @Complemento, @IdCidade); select cast(scope_identity() as int)";

            SqlCommand commandInsert = new SqlCommand(strInsert, Conn);

            commandInsert.Parameters.Add(new SqlParameter("@Logradouro", packet.hotel.address.Street));
            commandInsert.Parameters.Add(new SqlParameter("@Numero", packet.hotel.address.Number));
            commandInsert.Parameters.Add(new SqlParameter("@Bairro", packet.hotel.address.District));
            commandInsert.Parameters.Add(new SqlParameter("@CEP", packet.hotel.address.ZipCode));
            commandInsert.Parameters.Add(new SqlParameter("@Complemento", packet.hotel.address.Complement));
            commandInsert.Parameters.Add(new SqlParameter("@IdCidade", InsertCity(packet.hotel.address)));

            var x = (int)commandInsert.ExecuteScalar();
            return x;
        }

        private int InsertTicket(Packet packet)
        {
            string strInsert = "insert into Ticket (StartId, DestinationId,ClientId, Price) " +
                                "values (@StartId, @DestinationId, @ClientId, @Price); select cast(scope_identity() as int)";

            SqlCommand commandInsert = new SqlCommand(strInsert, Conn);

            commandInsert.Parameters.Add(new SqlParameter("@StartId", InsertAddressS(packet.ticket)));
            commandInsert.Parameters.Add(new SqlParameter("@DestinationId", InsertAddressD(packet.ticket)));
            commandInsert.Parameters.Add(new SqlParameter("@ClientId", InsertClient(packet)));
            commandInsert.Parameters.Add(new SqlParameter("@Price", packet.ticket.Price));

            return (int)commandInsert.ExecuteScalar();
        }

        private int InsertClient(Packet packet)
        {
            string strInsert = "insert into Client (Name, Phone, IdEndereco) "
                + "values (@Name, @Phone, @IdEndereco); select cast(scope_identity() as int)";

            SqlCommand commandInsert = new SqlCommand(strInsert, Conn);

            commandInsert.Parameters.Add(new SqlParameter("@Name", packet.client.Name));
            commandInsert.Parameters.Add(new SqlParameter("@Phone", packet.client.Phone));
            commandInsert.Parameters.Add(new SqlParameter("@IdEndereco", InsertAddressS(packet.ticket)));

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


        //public List<Packet> FindAll()
        //{
        //    List<Packet> packetlist = new();
        //    StringBuilder sb = new StringBuilder();

        //    sb.Append(@"select 
        //                      p.Id, 
        //                      p.Price, 
        //                      t.Id as idticket, 
        //                      t.Price as preçoticket, 
        //                      t.RegisterDate as dataticket, 
        //                      c.Id as idclientT, 
        //                      c.Name as nameclientT, 
        //                      c.Phone as phoneclietT, 
        //                      c.RegisterDate as datacliente, 
        //                      eclient.Id as idendereco, 
        //                      eclient.Logradouro as logradourocliente, 
        //                      eclient.Numero, 
        //                      eclient.Bairro, 
        //                      eclient.CEP, 
        //                      eclient.Complemento, 
        //                      eclient.DtCadastro, 
        //                      cAd.Id as idcity3, 
        //                      cAd.Descricao, 
        //                      cAd.DtCadastro as data, 
        //                      tST.Id as idstart, 
        //                      tST.Logradouro as startE, 
        //                      tST.Numero as numberstart, 
        //                      tST.Bairro, 
        //                      tST.CEP, 
        //                      tST.Complemento, 
        //                      tST.DtCadastro as datatst, 
        //                      tDT.Id as iddestination, 
        //                      tDT.Logradouro as destination, 
        //                      tDT.Numero, 
        //                      tDT.Bairro, 
        //                      tDT.CEP, 
        //                      tDT.Complemento, 
        //                      tDT.DtCadastro as datatdt, 
        //                      cid.Id as idcity1, 
        //                      cid.Descricao, 
        //                      cid.DtCadastro as data1, 
        //                      cid2.Id as idcity2, 
        //                      cid2.Descricao, 
        //                      cid2.DtCadastro as data2, 
        //                      h.Id, 
        //                      h.Name as Namehotel, 
        //                      h.IdAddress, 
        //                      h.Price, 
        //                      h.RegisterDate, 
        //                      e.Id, 
        //                      e.Logradouro, 
        //                      e.Numero, 
        //                      e.Bairro, 
        //                      e.CEP, 
        //                      e.Complemento, 
        //                      e.DtCadastro, 
        //                      cidh.Id, 
        //                      cidh.Descricao, 
        //                      cidh.DtCadastro as data ,
        //                      c.Name,
        //                      c.Phone,
        //                      c.RegisterDate, 
        //                      e.Id,
        //                      e.Logradouro,
        //                      e.Numero, 
        //                      e.Bairro, 
        //                      e.CEP, 
        //                      e.Complemento,
        //                      e.DtCadastro , 
        //                      cid.Id, 
        //                      cid.Descricao, 
        //                      cid.DtCadastro as data
        //                    from 
        //                      Packet p 
        //                      Join Ticket t ON t.Id = p.TicketId 
        //                      Join Endereco tST ON t.StartId = tST.Id 
        //                      Join Cidade as cid ON tst.IdCidade = cid.Id 
        //                      Join Endereco tDT ON t.DestinationId = tDT.Id 
        //                      Join Cidade as cid2 ON t.DestinationId = cid2.Id 
        //                      Join Client as c ON t.ClientId = c.Id 
        //                      Join Endereco eclient On c.IdEndereco = eclient.Id 
        //                      Join Cidade as cAd ON cAd.Id = eclient.IdCidade 
        //                      Join Hotel as h ON p.HotelId = h.Id 
        //                      Join Endereco e ON h.IdAddress = e.Id 
        //                      Join Cidade as cidh ON cidh.Id = e.IdCidade
        //                      Join Client as cliente ON cliente.Id = p.ClientId
        //                      Join Endereco as endcli ON endcli.Id = cliente.Idendereco
        //                      Join Cidade as cidcli ON cidcli.Id = endcli.IdCidade
        //                      ");

        //    SqlCommand commandSelect = new(sb.ToString(), Conn);
        //    SqlDataReader dr = commandSelect.ExecuteReader();

        //    while (dr.Read())
        //    {
        //        Packet packet = new Packet();

        //        packet.Id = (int)dr["Id"];

        //        packet.hotel = new Hotel()
        //        {
        //            Id = (int)dr["Id"],
        //            Name = (string)dr["Namehotel"],
        //            address = new Address()
        //            {
        //                Id = (int)dr["Id"],
        //                Street = (string)dr["Logradouro"],
        //                Number = (int)dr["Numero"],
        //                District = (string)dr["Bairro"],
        //                ZipCode = (string)dr["CEP"],
        //                Complement = (string)dr["Complemento"],
        //                city = new City()
        //                {
        //                    Id = (int)dr["Id"],
        //                    Description = (string)dr["Descricao"],
        //                    RegisterDate = (DateTime)dr["data"]
        //                },
        //                RegisterDate = (DateTime)dr["Dtcadastro"]
        //            },
        //            RegisterDate = (DateTime)dr["RegisterDate"],
        //            Price = (decimal)dr["Price"]
        //        };
        //        packet.ticket = new Ticket()
        //        {
        //            Id = (int)dr["Idticket"],
        //            Price = (decimal)dr["preçoticket"],
        //            Start = new Address()
        //            {
        //                Id = (int)dr["Idstart"],
        //                Street = (string)dr["startE"],
        //                Number = (int)dr["numberstart"],
        //                District = (string)dr["Bairro"],
        //                ZipCode = (string)dr["CEP"],
        //                Complement = (string)dr["Complemento"],
        //                city = new City()
        //                {
        //                    Id = (int)dr["Idcity1"],
        //                    Description = (string)dr["Descricao"],
        //                    RegisterDate = (DateTime)dr["data1"]
        //                },
        //                RegisterDate = (DateTime)dr["datatst"]
        //            },

        //            Destination = new Address()
        //            {
        //                Id = (int)dr["Iddestination"],
        //                Street = (string)dr["destination"],
        //                Number = (int)dr["Numero"],
        //                District = (string)dr["Bairro"],
        //                ZipCode = (string)dr["CEP"],
        //                Complement = (string)dr["Complemento"],
        //                city = new City()
        //                {
        //                    Id = (int)dr["Idcity2"],
        //                    Description = (string)dr["Descricao"],
        //                    RegisterDate = (DateTime)dr["data2"]
        //                },
        //                RegisterDate = (DateTime)dr["datatdt"]
        //            },

        //            client = new Client()
        //            {
        //                Id = (int)dr["IdclientT"],
        //                Name = (string)dr["nameclientT"],
        //                Phone = (string)dr["Phone"],
        //                address = new Address()
        //                {
        //                    Id = (int)dr["Idendereco"],
        //                    Street = (string)dr["logradourocliente"],
        //                    Number = (int)dr["Numero"],
        //                    District = (string)dr["Bairro"],
        //                    ZipCode = (string)dr["CEP"],
        //                    Complement = (string)dr["Complemento"],
        //                    city = new City()
        //                    {
        //                        Id = (int)dr["Idcity3"],
        //                        Description = (string)dr["Descricao"],
        //                        RegisterDate = (DateTime)dr["data"]
        //                    },
        //                    RegisterDate = (DateTime)dr["datacliente"]
        //                }
        //            },
        //            Date = (DateTime)dr["dataticket"]
        //        };
        //        packet.client = new Client()
        //        {
        //            Id = (int)dr["Id"],
        //            Name = (string)dr["Name"],
        //            Phone = (string)dr["Phone"],
        //            address = new Address()
        //            {
        //                Id = (int)dr["Id"],
        //                Street = (string)dr["Logradouro"],
        //                Number = (int)dr["Numero"],
        //                District = (string)dr["Bairro"],
        //                ZipCode = (string)dr["CEP"],
        //                Complement = (string)dr["Complemento"],
        //                city = new City()
        //                {
        //                    Id = (int)dr["Id"],
        //                    Description = (string)dr["Descricao"],
        //                    RegisterDate = (DateTime)dr["data"]
        //                },
        //                RegisterDate = (DateTime)dr["DTCadastro"]
        //            },
        //            RegisterDate = (DateTime)dr["RegisterDate"]
        //        };
        //        packetlist.Add(packet);
        //    }
        //    return packetlist;
        //}

        public bool Update(int id, double price)
        {
            bool status = false;

            try
            {
                string strUpdate = "update Packet Set Price = " + "'" + price + "' where Id = " + id;

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
                string strDelete = $"Delete from Packet where Id = {id}";

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
